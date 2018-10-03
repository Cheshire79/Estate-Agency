using System;
using System.Web.Mvc;
using System.Web.Routing;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date.Client;
using EstateAgency.BLL.Interface.Date.Realtor;
using EstateAgency.BLL.Mapper;
using EstateAgency.BLL.Services;
using EstateAgency.DAL.EF;
using EstateAgency.DAL.Infrastructure;
using EstateAgency.DAL.Interface;
using EstateAgency.DAL.Interface.Date;
using EstateAgency.DAL.Repository;
using Ninject;
using Ninject.Web.Common;
using WebUI.Mapper;
using EstateAgency.BLL.Services.RealeEstateOrdering;
using EstateAgency.BLL.Services.RealEstateForClientFiltering;
using EstateAgency.BLL.Services.RealEstatetFiltering;
using EstateAgency.DAL.Interface.Identity;
using EstateAgency.DAL.Identity.Repository;
using EstateAgency.DAL.Identity.Infrastructure;
using EstateAgency.BLL.Identity.Services;
using EstateAgency.BLL.Identity.Interface;
using Identity.BLL.Mapper;

namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _ninjectKernel;
        private string _connectionString = "DefaultConnection";
        public NinjectControllerFactory(IKernel ninjectKernel1)
        {
            _ninjectKernel = ninjectKernel1;
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {            
            AddBindingsForEstateAgencyDAL();
            AddBindingsForEstateAgencyBLL();
            _ninjectKernel.Bind<IMapperFactoryWEB>().To<MapperFactoryWEB>().InSingletonScope();
        }

        private void AddBindingsForEstateAgencyDAL()
        {
			
			_ninjectKernel.Bind<DataContext>().To<DataContext>().InRequestScope()
				.WithConstructorArgument(_connectionString);



			_ninjectKernel.Bind<IEstateAgencyUnitOfWork>().To<EstateAgencyUnitOfWork>().InRequestScope();
			_ninjectKernel.Bind<IIdentityUnitOfWork>().To<IdentityUnitOfWork>().InRequestScope();

			_ninjectKernel.Bind<IEstateAgencyFactoryRepository>().To<EstateAgencyFactoryRepositor>();
//
			_ninjectKernel.Bind<IRepository<RealEstate>>().To<RealEstateRepository>();
            _ninjectKernel.Bind<IReadOnlyRepository<City>>().To<CityReadOnlyRepository>();
            _ninjectKernel.Bind<IReadOnlyRepository<CityDistrict>>().To<CityDistrictReadOnlyRepository>();
            _ninjectKernel.Bind<IReadOnlyRepository<Street>>().To<StreetReadOnlyRepository>();
			_ninjectKernel.Bind<IUserReadOnlyRepository>().To<UserReadOnlyRepository>();
			


			_ninjectKernel.Bind<IRealEstatesDataMapper>().To<RealEstatesDataMapper>();            	

			_ninjectKernel.Bind<IIdentityFactoryRepository>().To<IdentityFactoryRepository>();
			
			_ninjectKernel.Bind<IRoleRepository>().To<RoleRepository>();
			_ninjectKernel.Bind<IUserRepository>().To<UserRepository>();

		}

        private void AddBindingsForEstateAgencyBLL()
        {
            _ninjectKernel.Bind<IRealtorService>().To<RealtorService>();
            _ninjectKernel.Bind<IClientService>().To<ClientService>();
            
            _ninjectKernel.Bind<IRealeEstateSort<RealEstateForRealtorDTO>>().To<RealeEstateSort<RealEstateForRealtorDTO>>();
            _ninjectKernel.Bind<IRealeEstateSort<RealEstateForClientDTO>>().To<RealeEstateSort<RealEstateForClientDTO>>();
            _ninjectKernel.Bind<IFilterForRealtor>().To<FilterForRealtor>();
            _ninjectKernel.Bind<IFilterForClient>().To<FilterForClient>();
            


            _ninjectKernel.Bind<IBLLMapper>().To<BLLMapper>().InSingletonScope();

			_ninjectKernel.Bind<IIdentityService>().To<IdentityService>();
			_ninjectKernel.Bind<IIdentityBLLMapper>().To<IdentityBLLMapper>().InSingletonScope();


		}

    }
}