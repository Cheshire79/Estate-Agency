using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EstateAgency.BLL.Identity.Interface;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date.Client;
using Microsoft.AspNet.Identity;
using WebUI.Mapper;
using WebUI.Models;
using WebUI.Models.EstateAgency.Client;
using WebUI.Models.UsersAndRoles;

namespace WebUI.Controllers
{
    public class ClientController : Controller
    {
        private IClientService _clientService;
        private IIdentityService _identityService;
        private IMapper _mapper;
        private int _pageSize = 8;

        public ClientController(IClientService clientService, IIdentityService identityService, IMapperFactoryWEB mapperFactory)
        {
            _clientService = clientService;
            _identityService = identityService;
            _mapper = mapperFactory.CreateMapperWEB();
        }

        [Authorize(Roles = "client")]
        public async Task<ActionResult> RealEstates()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            //   await _clientService.SetInitialData(userId);

            ChoosenSearchParametrsForClientView searchParameters = new ChoosenSearchParametrsForClientView();
            DataAboutRealEstatesForClientView dataForRealtor = await PreparedDataAboutRealEstates(searchParameters);
            return View(dataForRealtor);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<ActionResult> RealEstates(ChoosenSearchParametrsForClientView searchParametersForRealtor)
        {
            DataAboutRealEstatesForClientView dataForRealtor;
            if (ModelState.IsValid)
            {
                dataForRealtor = await PreparedDataAboutRealEstates(searchParametersForRealtor);
                return View(dataForRealtor);
            }
            searchParametersForRealtor = new ChoosenSearchParametrsForClientView();
            dataForRealtor = await PreparedDataAboutRealEstates(searchParametersForRealtor);
            return View(dataForRealtor);
        }

        private async Task<DataAboutRealEstatesForClientView> PreparedDataAboutRealEstates(ChoosenSearchParametrsForClientView choosenSearchParameters)
        {
            ChoosenSearchParametersForClientDTO  choosenSearchParametersDTO = _mapper.Map<ChoosenSearchParametrsForClientView, ChoosenSearchParametersForClientDTO>
                       (choosenSearchParameters);
            var users = await _identityService.GetUsers().ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToListAsync();

            List<RealEstateForClientDTO> realEstatesDTO = await _clientService.FormRealEstates(choosenSearchParametersDTO)
                .Skip((choosenSearchParameters.Page - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync();

            List<RealEstateForClientView> realEstates =
                _mapper.Map<List<RealEstateForClientDTO>, List<RealEstateForClientView>>(realEstatesDTO);


            DataAboutRealEstatesForClientView dataForRealtor = new DataAboutRealEstatesForClientView
            {
                ChoosenSearchParametersForRealtor = choosenSearchParameters,
                RealEstates = realEstates,
                SearchParameters = _mapper.Map<DataForSearchParametersClientDTO, DataForSearchParametersClientView>(await _clientService.InitiateSearchParameters()),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = choosenSearchParameters.Page,
                    ItemsPerPage = _pageSize,
                    TotalItems = await _clientService.FormRealEstates(choosenSearchParametersDTO).CountAsync()
                }
            };
            return dataForRealtor;
        }

        protected override void Dispose(bool disposing)
        {
            _clientService.Dispose();
            _identityService.Dispose();
            base.Dispose(disposing);
        }
    }
}