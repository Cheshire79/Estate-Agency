using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EstateAgency.BLL.Identity.Interface;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;
using EstateAgency.BLL.Interface.Date.Realtor;
using Microsoft.AspNet.Identity;
using WebUI.Mapper;
using WebUI.Models.EstateAgency.ForManipulate;
using WebUI.Models.EstateAgency.Realtor;
using WebUI.Models.Realtor;

namespace WebUI.Controllers
{
    public class RealtorController : Controller
    {
        private IRealtorService _realtorService;
        private IIdentityService _identityService;
        private IMapper _mapper;
       

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

            ChoosenSearchParametersForRealtorView searchParameters = new ChoosenSearchParametersForRealtorView();
            DataAboutRealEstatesForRealtorView dataForRealtor = await PreparedDataAboutRealEstates(searchParameters);
            return View(dataForRealtor);
        }

        [HttpPost]
        [Authorize(Roles = "realtor")]
        public async Task<ActionResult> RealEstates(ChoosenSearchParametersForRealtorView searchParametersForRealtor)
        {
            DataAboutRealEstatesForRealtorView dataForRealtor;
            if (ModelState.IsValid)
            {
                dataForRealtor = await PreparedDataAboutRealEstates(searchParametersForRealtor);
                return View(dataForRealtor);
            }
            searchParametersForRealtor = new ChoosenSearchParametersForRealtorView();
            dataForRealtor = await PreparedDataAboutRealEstates(searchParametersForRealtor);
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

        private async Task<DataAboutRealEstatesForRealtorView> PreparedDataAboutRealEstates(ChoosenSearchParametersForRealtorView choosenSearchParameters)
        {
            ChoosenSearchParametersForRealtorDTO choosenSearchParametersDTO = _mapper.Map<ChoosenSearchParametersForRealtorView, ChoosenSearchParametersForRealtorDTO>
                       (choosenSearchParameters);
            string userId = HttpContext.User.Identity.GetUserId();
			DataAboutRealEstatesForRealtorDTO dataForRealtor = await _realtorService.FormRealEstates(userId, choosenSearchParametersDTO);
		 
			return _mapper.Map<DataAboutRealEstatesForRealtorDTO, DataAboutRealEstatesForRealtorView>(dataForRealtor);
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
