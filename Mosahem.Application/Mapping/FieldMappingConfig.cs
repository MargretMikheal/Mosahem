using Mapster;
using mosahem.Domain.Entities.MasterData;
using Mosahem.Application.Features.Fields.Commands.AddField;
using Mosahem.Application.Features.Fields.Commands.EditField;
using Mosahem.Application.Features.Fields.Queries.GetAllFields;

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

            //Edit Field Command Mapping
            config.NewConfig<EditFieldCommand, Field>()
                .Map(dest => dest.NameAr, src => src.NameAr)
                .Map(dest => dest.NameEn, src => src.NameEn)
                .IgnoreNullValues(true);
            #endregion
            //Get All Fields Query Mapping
            #region Queries Mapping
            config.NewConfig<Field, GetAllFieldsQueryResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Localize(src.NameAr, src.NameEn));
            #endregion

        }
    }
}
