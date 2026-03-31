using Mapster;
using mosahem.Domain.Entities.Location;
using Mosahem.Application.Features.Cities.Commands.AddCity;
using Mosahem.Application.Features.Cities.Queries.GetCitiesByGovernate;

namespace Mosahem.Application.Mapping
{
    public class CityMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            #region Commands Mapping
            //Add City Mapping
            config.NewConfig<AddCityCommand, City>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.GovernorateId, src => src.GovernorateId)
                .Map(dest => dest.NameAr, src => src.NameAr)
                .Map(dest => dest.NameEn, src => src.NameEn)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow);
            #endregion

            #region Queries Mapping
            //Get Cities By Governate Mapping
            config.NewConfig<City, GetCitiesByGovernateResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Localize(src.NameAr, src.NameEn));
            #endregion
        }
    }
}
