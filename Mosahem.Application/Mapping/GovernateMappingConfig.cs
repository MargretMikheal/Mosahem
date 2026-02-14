using Mapster;
using mosahem.Domain.Entities.Location;
using Mosahem.Application.Features.Governates.GetAllGovernates;

namespace Mosahem.Application.Mapping
{
    public class GovernateMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Get All Governates Query Mapping
            config.NewConfig<Governorate, GetAllGovernatesQueryResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Localize(src.NameAr, src.NameEn));
        }
    }
}
