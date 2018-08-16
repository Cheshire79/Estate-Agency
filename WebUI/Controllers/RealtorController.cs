using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;
using Identity.BLL.Interface;
using Microsoft.AspNet.Identity;
using WebUI.Mapper;
using WebUI.Models;
using WebUI.Models.Realtor;
using WebUI.Models.Realtor.ForManipulate;
using WebUI.Models.UsersAndRoles;

namespace WebUI.Controllers
{
    public class RealtorController : Controller
    {
        private IRealtorService _realtorService;
        private IIdentityService _identityService;
        private IMapper _mapper;
        private int _pageSize = 8;

        public RealtorController(IRealtorService realtorService, IIdentityService identityService, IMapperFactoryWEB mapperFactory)
        {
            _realtorService = realtorService;
            _identityService = identityService;
            _mapper = mapperFactory.CreateMapperWEB();
        }

        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> RealEstates()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            await _realtorService.SetInitialData(userId);

            ChoosenSearchParametrsForRealtorView searchParameters = new ChoosenSearchParametrsForRealtorView();
            DataAboutRealEstatesForRealtorView dataForRealtor = await PreparedRealEstates(searchParameters);
            return View(dataForRealtor);
        }

        [HttpPost]
        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> RealEstates(ChoosenSearchParametrsForRealtorView searchParametersForRealtor)
        {
            DataAboutRealEstatesForRealtorView dataForRealtor;
            if (ModelState.IsValid)
            {
                dataForRealtor = await PreparedRealEstates(searchParametersForRealtor);
                return View(dataForRealtor);
            }
            searchParametersForRealtor = new ChoosenSearchParametrsForRealtorView();
            dataForRealtor = await PreparedRealEstates(searchParametersForRealtor);
            return View(dataForRealtor);
        }

        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> CreateRealEstate(string returnUrl)
        {
            DataForManipulateRealEstateView dataForManipulateRealEstate =
                _mapper.Map<DataForManipulateRealEstateDTO, DataForManipulateRealEstateView>(await _realtorService.GetDataForRealEstateManipulate());
            dataForManipulateRealEstate.ReturnUrl =
                string.IsNullOrWhiteSpace(returnUrl) ? Url.Action("RealEstates") : returnUrl;
            return View(dataForManipulateRealEstate);
        }

        [HttpPost]
        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> CreateRealEstate(RealEstateToSaveView realEstate, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    realEstate.Image = imageData;
                }
                var realEstatelDTO = _mapper.Map<RealEstateToSaveView, RealEstateDTO>(realEstate);
                string realtorId = HttpContext.User.Identity.GetUserId();
                await _realtorService.Create(realEstatelDTO, realtorId);
            }
            return RedirectToAction("RealEstates");
        }

        [HttpPost]
        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> MarkAsSold(int? id)
        {
            if (id != null)
                await _realtorService.MarkRealEstateAsSold(id.Value);
            else
            {
                throw new HttpException(400, "Invalid value of reale estate Id");
            }
            return RedirectToAction("RealEstates");
        }

        [HttpPost]
        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> DeleteRealEstate(int? id)
        {
            if (id != null)
                await _realtorService.DeleteRealEstate(id.Value);
            else
            {
                throw new HttpException(400, "Invalid value of reale estate Id");
            }
            return RedirectToAction("RealEstates");
        }

        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> FillStreet(int? districtId)
        {
            List<StreetDropItemView> cities;
            if (districtId != null)
            {
                cities = _mapper.Map<List<StreetDropDownItemDTO>, List<StreetDropItemView>>(
                    await _realtorService.GetStreetsForDropDownByDistrctId(districtId.Value));
                return Json(cities, JsonRequestBehavior.AllowGet);
            }
            throw new HttpException(400, "Invalid value of district Id");
        }

        private async Task<DataAboutRealEstatesForRealtorView> PreparedRealEstates(ChoosenSearchParametrsForRealtorView choosenSearchParameters)
        {
            ChoosenSearchParametersForRealtorDTO choosenSearchParametersDTO = _mapper.Map<ChoosenSearchParametrsForRealtorView, ChoosenSearchParametersForRealtorDTO>
                       (choosenSearchParameters);
            string userId = HttpContext.User.Identity.GetUserId();
            var users = await _identityService.GetUsers().ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToListAsync();

            List<RealEstateForRealtorDTO> realEstatesDTO = await _realtorService.GetRealEstatesForRealtor(userId, choosenSearchParametersDTO)
                .Skip((choosenSearchParameters.Page - 1) * _pageSize)
                .Take(_pageSize).ToListAsync();

            List<RealEstateForRealtorView> realEstates =
                _mapper.Map<List<RealEstateForRealtorDTO>, List<RealEstateForRealtorView>>(realEstatesDTO);

            realEstates = realEstates.Join(users, r => r.RealtorId, u => u.Id, (r, u) =>
             {
                 r.RealtorName = u.Name;
                 r.RealtorEmail = u.Email;
                 return r;
             }).ToList();

            DataAboutRealEstatesForRealtorView dataForRealtor = new DataAboutRealEstatesForRealtorView
            {
                ChoosenSearchParametersForRealtor = choosenSearchParameters,
                RealEstates = realEstates,
                SearchParameters = _mapper.Map<DataForSearchParametersDTO, DataForSearchParametersRealtorView>(await _realtorService.InitiateSearchParameters()),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = choosenSearchParameters.Page,
                    ItemsPerPage = _pageSize,
                    TotalItems = await _realtorService.GetRealEstatesForRealtor(userId, choosenSearchParametersDTO).CountAsync()
                }
            };
            return dataForRealtor;
        }

        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> EditRealEstate(int? id, string returnUrl)
        {
            if (id != null)
            {
                string realtorId = HttpContext.User.Identity.GetUserId();
                EditRealEstateView editRealEstate =
                    _mapper.Map<EditRealEstateDTO, EditRealEstateView>(await _realtorService.GetDataForRealEstateEditing(id.Value, realtorId));
                editRealEstate.DataForManipulateRealEstate.ReturnUrl =
                    string.IsNullOrWhiteSpace(returnUrl) ? Url.Action("RealEstates") : returnUrl;
                return View(editRealEstate);
            }
            throw new HttpException(400, "Invalid value of reale estate Id");
        }

        [HttpPost]
        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> EditRealEstate(RealEstateToSaveView realEstate, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    realEstate.Image = imageData;
                }
                var realEstatelDTO = _mapper.Map<RealEstateToSaveView, RealEstateDTO>(realEstate);
                await _realtorService.Save(realEstatelDTO);
            }
            return RedirectToAction("RealEstates"); 
        }

        protected override void Dispose(bool disposing)
        {
            _realtorService.Dispose();
            _identityService.Dispose();
            base.Dispose(disposing);
        }
    }
}
