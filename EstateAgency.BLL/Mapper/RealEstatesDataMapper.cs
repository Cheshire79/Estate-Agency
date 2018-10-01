using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;
using EstateAgency.DAL.Interface;

namespace EstateAgency.BLL.Mapper
{
    public class RealEstatesDataMapper: IRealEstatesDataMapper
    {
        private IMapper _mapper;
        private int _cityKievId;
        private IEstateAgencyUnitOfWork _unitOfWork;

        public RealEstatesDataMapper(IEstateAgencyUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapperFactory.CreateMapper();
            var cityKiev = _unitOfWork.Cities.GetAll().FirstOrDefault(x => x.Name == "Киев");
            if (cityKiev != null)
                _cityKievId = cityKiev.Id;
            else
            {
                throw new HttpException(404, "Cannot find Kiev. Working just for area of Kiev city.");
            }
        }

        public IQueryable<CityDistrictDTO> KievDistricts()// just for Kiev city
        {
            return _unitOfWork.CityDistricts.GetAll().Where(x => x.CityId == _cityKievId).ProjectTo<CityDistrictDTO>(_mapper.ConfigurationProvider);
        }

        public IQueryable<RealEstateDTO> RealEstates()
        {
            return _unitOfWork.RealEstates.GetAll().ProjectTo<RealEstateDTO>(_mapper.ConfigurationProvider);
        }

        public IQueryable<StreetDTO> Streets()
        {
            return _unitOfWork.Streets.GetAll().ProjectTo<StreetDTO>(_mapper.ConfigurationProvider);
        }

        public List<RoomNumberDownItemDTO> Rooms()
        {
            return new List<RoomNumberDownItemDTO>()
            {
                new RoomNumberDownItemDTO(){Id=1,Name = "1"},
                new RoomNumberDownItemDTO(){Id=2,Name = "2"},
                new RoomNumberDownItemDTO(){Id=3,Name = "3"},
                new RoomNumberDownItemDTO(){Id=4,Name = "4"},
                new RoomNumberDownItemDTO(){Id=5,Name = "5"}
            };
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
