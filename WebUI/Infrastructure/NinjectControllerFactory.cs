using System;
using System.Web.Mvc;
using System.Web.Routing;
using Identity.BLL.Interface;
using Identity.DAL.EF;
using Identity.DAL.Infrastructure;
using Identity.DAL.Repositories;
using Identity.BLL.Services;
using Identity.DAL.Interface;
using EstateAgency.BLL.Interface;
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
            AddBindingsForIdentityDAL();
            AddBindingsForIdentityBLL();
            AddBindingsForKnowledgeManagementDAL();
            AddBindingsForKnowledgeManagementBLL();
            _ninjectKernel.Bind<IMapperFactoryWEB>().To<MapperFactoryWEB>().InSingletonScope();
        }

        private void AddBindingsForKnowledgeManagementDAL()
        {
            _ninjectKernel.Bind<IDataContext>().To<DataContext>().WithConstructorArgument("connection", _connectionString);

            _ninjectKernel.Bind<IRepository<RealEstate>>().To<RealEstateRepository>();
            _ninjectKernel.Bind<IReadOnlyRepository<City>>().To<CityReadOnlyRepository>();
            _ninjectKernel.Bind<IReadOnlyRepository<CityDistrict>>().To<CityDistrictReadOnlyRepository>();
            _ninjectKernel.Bind<IReadOnlyRepository<Street>>().To<StreetReadOnlyRepository>();

            _ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            _ninjectKernel.Bind<IFactoryRepository>().To<FactoryRepositor>();
        }

        private void AddBindingsForKnowledgeManagementBLL()
        {
            _ninjectKernel.Bind<IRealtorService>().To<RealtorService>();
            _ninjectKernel.Bind<IRealeEstateSort>().To<RealeEstateSort>();
            _ninjectKernel.Bind<EstateAgency.BLL.Interface.IMapperFactory>().To<EstateAgency.BLL.Mapper.MapperFactory>().InSingletonScope();
        }

        private void AddBindingsForIdentityDAL()
        {
            _ninjectKernel.Bind<IIdentityUnitOfWork<ApplicationUserManager, ApplicationRoleManager>>().To<IdentityUnitOfWork>();
            _ninjectKernel.Bind<ApplicationContext>().ToSelf()
                .InRequestScope()
                .WithConstructorArgument("connection", _connectionString);
            _ninjectKernel.Bind<IFactoryEntitiesManager<ApplicationUserManager, ApplicationRoleManager, ApplicationContext>>()
                .To<FactoryEntitiesManager>();
        }

        private void AddBindingsForIdentityBLL()
        {
            _ninjectKernel.Bind<IIdentityService>().To<IdentityService>();
            _ninjectKernel.Bind<Identity.BLL.Interface.IMapperFactory>().To<Identity.BLL.Mapper.MapperFactory>().InSingletonScope();
        }
    }
}