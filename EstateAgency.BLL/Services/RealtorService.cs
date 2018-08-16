using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;
using EstateAgency.DAL.Interface;
using EstateAgency.DAL.Interface.Date;

namespace EstateAgency.BLL.Services
{
    public class RealtorService : IRealtorService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRealeEstateSort _realeEstateSort;
        private int _cityKievId;

        public RealtorService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IRealeEstateSort realeEstateSort)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapperFactory.CreateMapper();
            _realeEstateSort = realeEstateSort;
            var cityKiev = _unitOfWork.Cities.GetAll().FirstOrDefault(x => x.Name == "Киев");
            if (cityKiev != null)
                _cityKievId = cityKiev.Id;
            else
            {
                throw new HttpException(404, "Cannot find Kiev. Working just for area of Kiev city.");
            }
        }

        public async Task SetInitialData(string realtorId)
        {
            if (!_unitOfWork.RealEstates.GetAll().Any())
            {
                var streetInKiev = _unitOfWork.Streets.GetAll().FirstOrDefault(x => x.Name == "Чорновола");
                if (streetInKiev != null)
                {
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "33/30",
                        Appartment = 24,
                        Floor = 6,
                        Height = 9,
                        Area = 54,
                        Price = 36.000M,
                        RoomNumber = 2,
                        CreationDate = DateTime.Now,
                        Description = "test",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "3",
                        Appartment = 4,
                        Floor = 2,
                        Height = 19,
                        Area = 154,
                        Price = 56.000M,
                        RoomNumber = 2,
                        CreationDate = DateTime.Now,
                        Description = "test",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                }
                streetInKiev = _unitOfWork.Streets.GetAll().FirstOrDefault(x => x.Name == "Саксаганского");
                if (streetInKiev != null)
                {
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "30",
                        Appartment = 14,
                        Floor = 2,
                        Height = 4,
                        Area = 74,
                        Price = 86.000M,
                        RoomNumber = 3,
                        CreationDate = DateTime.Now,
                        Description = "test 2",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "123",
                        Appartment = 94,
                        Floor = 12,
                        Height = 19,
                        Area = 154,
                        Price = 516.000M,
                        RoomNumber = 4,
                        CreationDate = DateTime.Now,
                        Description = "test",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                }
                streetInKiev = _unitOfWork.Streets.GetAll().FirstOrDefault(x => x.Name == "Ружинская ул.");
                if (streetInKiev != null)
                {
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "26",
                        Appartment = 9,
                        Floor = 4,
                        Height = 5,
                        Area = 30,
                        Price = 32500.000M,
                        RoomNumber = 1,
                        CreationDate = DateTime.Now,
                        Description = "1 комнатная квартира студио на Нивках, 4/5 этажного, 30 кв.м., улица Ружинська 26 (бывшая Вильгельма Пика). Евроремонт, в пешей доступности 3 станции метро – Нивки, Берестейская, Сырецкая. Квартира продается с мебелью и техникой. Комфортный для проживания район – парковая зона, школы, садики, маркеты, рынок, лес. Также отличный вариант под арендный бизнес.",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                }
                streetInKiev = _unitOfWork.Streets.GetAll().FirstOrDefault(x => x.Name == "Победы просп.");
                if (streetInKiev != null)
                {
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "5б",
                        Appartment = 223,
                        Floor = 10,
                        Height = 34,
                        Area = 44,
                        Price = 49500.000M,
                        RoomNumber = 1,
                        CreationDate = DateTime.Now,
                        Description = "Предлагается к продаже 1 к. квартира 45 кв. м на 10-м этаже в доме премиум класса по пр. Победы 5б - ЖК Victory V, застройщик bUd development, запланированная дата ввода в эксплуатацию 3 квартал 2018г. Большой дом на 34 этажа изящной, лаконичной архитектурной формы, расположенный в центральной части Киева",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "26",
                        Appartment = 9,
                        Floor = 18,
                        Height = 32,
                        Area = 44,
                        Price = 88500.000M,
                        RoomNumber = 1,
                        CreationDate = DateTime.Now,
                        Description = "Квартира з ремонтом в новому ЖК 'Smart Plaza Polytech'. ЄВРОРЕМОНТ. Вбудовані меблі, побутова техніка, на підлозі ламінат та кахель, встановлений кондиціонер та бойлер. З вікон відкривається ПАНОРАМА на місто та парк на даху ТРЦ. ",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "67",
                        Appartment = 78,
                        Floor = 2,
                        Height = 16,
                        Area = 48,
                        Price = 32500.000M,
                        RoomNumber = 1,
                        CreationDate = DateTime.Now,
                        Description = "1 комнатная квартира в ЖК ’’Нивки парк’’ с ремонтом (без ремонта 44300$ на 7 этаже 3й дом) 2/16 этажного дома 48/19/13, закрытая охраняемая территория, метро Нивки 5 минут пешком",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "127",
                        Appartment = 23,
                        Floor = 8,
                        Height = 9,
                        Area = 58,
                        Price = 43000.000M,
                        RoomNumber = 3,
                        CreationDate = DateTime.Now,
                        Description = "Продается теплая, светлая квартира на пр.Победы, 127. Метраж: 58/41/7 на 8/9эт.В квартире 3 года назад сделан ремонт, замена всего, установлена встроенная кухня, шкафы, официальный перевод на горячую воду через бойлер, счетчики.",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                }
                streetInKiev = _unitOfWork.Streets.GetAll().FirstOrDefault(x => x.Name == "Жмаченко генерала ул.");
                if (streetInKiev != null)
                {
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "28",
                        Appartment = 123,
                        Floor = 19,
                        Height = 26,
                        Area = 68,
                        Price = 78000.000M,
                        RoomNumber = 2,
                        CreationDate = DateTime.Now,
                        Description = "ВЛАДЕЛЕЦ!!! ЖК Автограф. переуступка права собственности. комплекс победитель 2017г. в наминации Комфорт класс. Здача конец 2018г. Консьерж сервисс, три лифта, своя территория, подземный паркинг",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "28",
                        Appartment = 9,
                        Floor = 19,
                        Height = 26,
                        Area = 115,
                        Price = 127000.000M,
                        RoomNumber = 4,
                        CreationDate = DateTime.Now,
                        Description = "ЖК Автограф. Объединение двух квартир 2+1. переуступка права собственности. Аналога в комплексе нет. комплекс победитель 2017г. в наминации Комфорт класс. Здача конец 2018г.",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );

                }
                streetInKiev = _unitOfWork.Streets.GetAll().FirstOrDefault(x => x.Name == "Строителей ул.");
                if (streetInKiev != null)
                {
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "32/2",
                        Appartment = 83,
                        Floor = 5,
                        Height = 5,
                        Area = 64,
                        Price = 42900.000M,
                        RoomNumber = 3,
                        CreationDate = DateTime.Now,
                        Description =
                                "Продаётся 3х квартира. Сталинка. с/у/р 5/5. Состояние под ремонт. Чистый подъезд. До М.Дарница 500 метров. Рядом школа, торговый центры. Хорошая транспортная развяска. Срочная продажа.Торг.",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "39",
                        Appartment = 19,
                        Floor = 3,
                        Height = 4,
                        Area = 43,
                        Price = 394000.000M,
                        RoomNumber = 2,
                        CreationDate = DateTime.Now,
                        Description =
                                "2х комнатная квартира 45м2, ул.Строителей 39, м. Дарница 3 мин пешком, Днепровский район. Сталинка, h=2.9м, площадь 43/24/9м2. 3 этаж, комнаты раздельные, окна во двор, балкон застеклен. Пластиковые окна, кондиционер, жилое состояние( требуется косметический ремонт).",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                }


                streetInKiev = _unitOfWork.Streets.GetAll().FirstOrDefault(x => x.Name == "Тампере ул.");
                if (streetInKiev != null)
                {
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "12Б",
                        Appartment = 13,
                        Floor = 2,
                        Height = 5,
                        Area = 41,
                        Price = 37000.000M,
                        RoomNumber = 2,
                        CreationDate = DateTime.Now,
                        Description = "смежно – раздельная квартира, 2 этаж 5 - эт.тома, 41 / 25 / 7кв.м., санузел раздельный, поменяна сантехника, балкон застеклен, стеклопакеты, кафель,",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "8А",
                        Appartment = 29,
                        Floor = 1,
                        Height = 5,
                        Area = 49,
                        Price = 360000.000M,
                        RoomNumber = 1,
                        CreationDate = DateTime.Now,
                        Description = "Без комиссии!!! О квартире.Общая площадь 49 м2, большая комната - 18 м2 и просторная кухня – 10 м2.Две лоджии(4м2 и 2, 5м2), в одной из них кладовка.Лоджии опоясывают квартиру, создавая дополнительную воздушную подушку.Благодаря этому зимой в квартире тепло, а летом прохладно.На полу – паркет.Шкаф - купе.В ванной установлен бойлер на 50л. ",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                    _unitOfWork.RealEstates.Create(new RealEstate()
                    {
                        Building = "11",
                        Appartment = 49,
                        Floor = 2,
                        Height = 5,
                        Area = 49,
                        Price = 320000.000M,
                        RoomNumber = 2,
                        CreationDate = DateTime.Now,
                        Description = "Срочно продам свою двухкомнатную квартиру с ремонтом, мебелью и всей бытовой техникой. На полу паркет, интернет кабельное ТВ. Квартира находиться на высоком 1 этаже в доме после капитального ремонта на улице. ",
                        IsSold = false,
                        StreetId = streetInKiev.Id,
                        RealtorId = realtorId
                    }
                    );
                }
                await _unitOfWork.SaveAsync();
            }
        }

        private IQueryable<CityDistrictDTO> GetKievDistricts()// just for Kiev city
        {
            return _unitOfWork.CityDistricts.GetAll().Where(x => x.CityId == _cityKievId).ProjectTo<CityDistrictDTO>(_mapper.ConfigurationProvider);
        }

        private IQueryable<RealEstateDTO> GetRealEstates()
        {
            return _unitOfWork.RealEstates.GetAll().ProjectTo<RealEstateDTO>(_mapper.ConfigurationProvider);
        }

        private IQueryable<StreetDTO> GetStreets()
        {
            return _unitOfWork.Streets.GetAll().ProjectTo<StreetDTO>(_mapper.ConfigurationProvider);
        }

        public async Task<List<StreetDropDownItemDTO>> GetStreetsForDropDownByDistrctId(int districtId)
        {
            List<StreetDropDownItemDTO> streets = (await _unitOfWork.Streets.GetAll().Where(x => x.CityDistrictId == districtId).ProjectTo<StreetDropDownItemDTO>(_mapper.ConfigurationProvider).ToListAsync());
            if (streets.Count == 0)
                streets.Add(new StreetDropDownItemDTO() { Id = null, Name = "Empty List" });
            return streets;
        }

        public async Task Create(RealEstateDTO realEstateDTO, string realtorId)
        {
            var realEstate = _mapper.Map<RealEstateDTO, RealEstate>(realEstateDTO);
            realEstate.Id = new RealEstate().Id;
            realEstate.RealtorId = realtorId;
            realEstate.IsSold = false;
            realEstate.CreationDate = DateTime.Now;
            _unitOfWork.RealEstates.Create(realEstate);
            await _unitOfWork.SaveAsync();
        }

        public IQueryable<RealEstateForRealtorDTO> GetRealEstatesForRealtor(string userId, ChoosenSearchParametersForRealtorDTO parameters)
        {
            IQueryable<RealEstateForRealtorDTO> realEstates =
                 (from realEstate in FilteredRealEstates(GetRealEstates(), parameters, userId)
                  join street in GetStreets() on realEstate.StreetId equals street.Id
                  join district in FilteredDistricts(GetKievDistricts(), parameters) on street.CityDistrictId equals district.Id
                  select new RealEstateForRealtorDTO
                  {
                      Id = realEstate.Id,
                      Building = realEstate.Building,
                      Appartment = realEstate.Appartment,
                      Floor = realEstate.Floor,
                      Height = realEstate.Height,
                      Area = realEstate.Area,
                      Price = realEstate.Price,
                      RoomNumber = realEstate.RoomNumber,
                      CreationDate = realEstate.CreationDate,
                      Description = realEstate.Description,
                      IsSold = realEstate.IsSold,
                      RealtorId = realEstate.RealtorId,
                      StreetName = street.Name,
                      DistrictName = district.Name,
                      IsOwner = (userId == realEstate.RealtorId),
                      Image = realEstate.Image,
                      DistrictId = district.Id,
                      StreetId = street.Id
                  });
            return _realeEstateSort.Sort(parameters.SortOrder)(realEstates);
        }

        private async Task<RealEstateForRealtorDTO> FindRealEstateById(int id, string userId)
        {
            RealEstateForRealtorDTO realEstateForRealtor = await
                (from realEstate in GetRealEstates()
                 join street in GetStreets() on realEstate.StreetId equals street.Id
                 join district in GetKievDistricts() on street.CityDistrictId equals district.Id
                 where realEstate.Id == id
                 select new RealEstateForRealtorDTO
                 {
                     Id = realEstate.Id,
                     Building = realEstate.Building,
                     Appartment = realEstate.Appartment,
                     Floor = realEstate.Floor,
                     Height = realEstate.Height,
                     Area = realEstate.Area,
                     Price = realEstate.Price,
                     RoomNumber = realEstate.RoomNumber,
                     CreationDate = realEstate.CreationDate,
                     Description = realEstate.Description,
                     IsSold = realEstate.IsSold,
                     RealtorId = realEstate.RealtorId,
                     StreetName = street.Name,
                     DistrictName = district.Name,
                     IsOwner = (userId == realEstate.RealtorId),
                     Image = realEstate.Image,
                     DistrictId = district.Id,
                     StreetId = street.Id
                 }).FirstOrDefaultAsync();

            if (realEstateForRealtor == null)
                throw new HttpException(404, "Cannot find real estate with id = " + id);
            return realEstateForRealtor;
        }

        public async Task<EditRealEstateDTO> GetDataForRealEstateEditing(int id, string userId)
        {
            EditRealEstateDTO editRealEstate = new EditRealEstateDTO();
            editRealEstate.RealEstate = await FindRealEstateById(id, userId);
            editRealEstate.DataForManipulateRealEstate = await GetDataForRealEstateManipulate(editRealEstate.RealEstate.DistrictId, editRealEstate.RealEstate.StreetId, editRealEstate.RealEstate.RoomNumber);
            return editRealEstate;
        }

        public async Task MarkRealEstateAsSold(int realEstateId)
        {
            var realEstate = await _unitOfWork.RealEstates.GetByIdAsync(realEstateId);
            if (realEstate == null)
                throw new HttpException(404, "Cannot find real estate with id = " + realEstateId);
            realEstate.IsSold = true;
            _unitOfWork.RealEstates.Update(realEstate);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteRealEstate(int realEstateId)
        {
            var realEstate = await _unitOfWork.RealEstates.GetByIdAsync(realEstateId);
            if (realEstate == null)
                throw new HttpException(404, "Cannot find real estate with id = " + realEstateId);
            _unitOfWork.RealEstates.Delete(realEstate);
            await _unitOfWork.SaveAsync();
        }

        public async Task Save(RealEstateDTO realEstateDTO)
        {
            var realEstate = _mapper.Map<RealEstateDTO, RealEstate>(realEstateDTO);
            var originRealEstate = await _unitOfWork.RealEstates.GetByIdAsync(realEstate.Id);
            if (originRealEstate == null)
                throw new HttpException(404, "Cannot find real estate with id = " + realEstate.Id);
            realEstate.RealtorId = originRealEstate.RealtorId;
            realEstate.CreationDate = originRealEstate.CreationDate;
            _unitOfWork.RealEstates.Update(realEstate);
            await _unitOfWork.SaveAsync();
        }

        public async Task<DataForSearchParametersDTO> InitiateSearchParameters()
        {
            var searchParameters = new DataForSearchParametersDTO();
            searchParameters.Districts = new List<CityDistrictDropDownItemDTO>() { new CityDistrictDropDownItemDTO() { Id = null, Name = "No matter" } };
            searchParameters.Districts.AddRange(await GetKievDistricts().OrderBy(x => x.Name).Select(x => new CityDistrictDropDownItemDTO { Id = x.Id, Name = x.Name }).ToListAsync());
            searchParameters.RoomNumbers = new List<RoomNumberDownItemDTO>()
                { new RoomNumberDownItemDTO() { Id = null, Name = "No matter" }};
            searchParameters.RoomNumbers.AddRange(GetRooms());

            searchParameters.SortOrders = _realeEstateSort.GetSortingOptionsName();
            return searchParameters;
        }

        public async Task<DataForManipulateRealEstateDTO> GetDataForRealEstateManipulate(int? specifiedDistrictId = null, int? specifiedStreetId = null, byte? specifiedRoomNumber = null)
        {
            var searchParameters = new DataForManipulateRealEstateDTO();

            await SetDistrictsDataForRealEstateManipulate(searchParameters, specifiedDistrictId);
            await SetStreetsDataForRealEstateManipulate(searchParameters, specifiedStreetId);
            SetRoomsDataForRealEstateManipulate(searchParameters, specifiedRoomNumber);

            return searchParameters;
        }

        private void SetRoomsDataForRealEstateManipulate(DataForManipulateRealEstateDTO searchParameters, byte? specifiedRoomNumber)
        {
            searchParameters.RoomNumbers = GetRooms();
            if (specifiedRoomNumber.HasValue)
                searchParameters.ChoosenRoomNumber = specifiedRoomNumber.Value;
            else
                searchParameters.ChoosenRoomNumber = searchParameters.RoomNumbers.FirstOrDefault().Id.Value;
        }

        private async Task SetDistrictsDataForRealEstateManipulate(DataForManipulateRealEstateDTO searchParameters, int? specifiedDistrictId)
        {
            searchParameters.Districts = await GetKievDistricts().OrderBy(x => x.Name).Select(x => new CityDistrictDropDownItemDTO { Id = x.Id, Name = x.Name }).ToListAsync();
            if (searchParameters.Districts.Count == 0)
                searchParameters.Districts.Add(new CityDistrictDropDownItemDTO() { Id = null, Name = "Empty List" });

            if (specifiedDistrictId.HasValue)
            {
                if (searchParameters.Districts.Exists(x => x.Id == specifiedDistrictId.Value))
                    searchParameters.ChoosenDistrictId = specifiedDistrictId.Value;
                else
                    throw new HttpException(404, "Cannot find district with id = " + specifiedDistrictId);
            }
            else
                searchParameters.ChoosenDistrictId = searchParameters.Districts.First().Id;
        }

        private async Task SetStreetsDataForRealEstateManipulate(DataForManipulateRealEstateDTO searchParameters, int? specifiedStreetId)
        {
            searchParameters.Streets = await _unitOfWork.Streets.GetAll().Where(x => x.CityDistrictId == searchParameters.ChoosenDistrictId).ProjectTo<StreetDropDownItemDTO>(_mapper.ConfigurationProvider).OrderBy(x => x.Name).ToListAsync();
            if (searchParameters.Streets.Count == 0)
                searchParameters.Streets.Add(new StreetDropDownItemDTO() { Id = null, Name = "Empty List" });
            if (specifiedStreetId.HasValue)
            {
                if (searchParameters.Streets.Exists(x => x.Id == specifiedStreetId))
                    searchParameters.ChoosenStreetId = specifiedStreetId;
                else
                    throw new HttpException(404, "Cannot find street with id = " + specifiedStreetId);
            }
            else
                searchParameters.ChoosenStreetId = searchParameters.Streets.First().Id;

        }

        private List<RoomNumberDownItemDTO> GetRooms()
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
        private IQueryable<CityDistrictDTO> FilteredDistricts(IQueryable<CityDistrictDTO> districts, ChoosenSearchParametersForRealtorDTO parameters)
        {
            var result = districts;
            if (parameters.DistrictId.HasValue)
                result = result.Where(x => x.Id == parameters.DistrictId);
            return result;
        }

        private IQueryable<RealEstateDTO> FilteredRealEstates(IQueryable<RealEstateDTO> realEstates, ChoosenSearchParametersForRealtorDTO parameters, string userId)
        {
            var result = realEstates;
            if (parameters.RoomNumber.HasValue)
                result = result.Where(x => x.RoomNumber == parameters.RoomNumber);
            if (parameters.AreaFrom.HasValue)
                result = result.Where(x => x.Area >= parameters.AreaFrom);
            if (parameters.AreaTo.HasValue)
                result = result.Where(x => x.Area <= parameters.AreaTo);

            if (parameters.PriceFrom.HasValue)
                result = result.Where(x => x.Price >= parameters.PriceFrom);
            if (parameters.PriceTo.HasValue)
                result = result.Where(x => x.Price <= parameters.PriceTo);

            if (parameters.HeightFrom.HasValue)
                result = result.Where(x => x.Height >= parameters.HeightFrom);
            if (parameters.HeightTo.HasValue)
                result = result.Where(x => x.Height <= parameters.HeightTo);

            if (parameters.FloorFrom.HasValue)
                result = result.Where(x => x.Floor >= parameters.FloorFrom);
            if (parameters.FloorTo.HasValue)
                result = result.Where(x => x.Floor <= parameters.FloorTo);

            if (parameters.ShowOnlyMyOwn)
                result = result.Where(x => x.RealtorId == userId);
            return result;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
