using Mapster;
using mosahem.Domain.Entities.MasterData;
using Mosahem.Application.Features.Fields.Commands.AddField;

namespace Mosahem.Application.Mapping
{
    public class FieldMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            #region Commands Mapping

            //Add Field Command Mapping
            config.NewConfig<AddFieldCommand, Field>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.NameAr, src => src.NameAr)
                .Map(dest => dest.NameEn, src => src.NameEn)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow);

            #endregion

        }
    }
}
