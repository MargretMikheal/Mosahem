using Mapster;
using mosahem.Domain.Entities.MasterData;
using Mosahem.Application.Features.Skills.Commands.AddSkill;
using Mosahem.Application.Features.Skills.Commands.EditSkill;
using Mosahem.Application.Features.Skills.Queries.GetAllSkills;
using Mosahem.Application.Features.Skills.Queries.GetAllSkillsGrouped;

namespace Mosahem.Application.Mapping
{
    public class SkillMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            #region Command Mapping
            config.NewConfig<AddSkillCommand, Skill>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.NameAr, src => src.NameAr)
                .Map(dest => dest.NameEn, src => src.NameEn)
                .Map(dest => dest.FieldId, src => src.FieldId).
                Map(dest => dest.CreatedAt, src => DateTime.UtcNow);

            config.NewConfig<EditSkillCommand, Skill>()
                .Map(dest => dest.NameAr, src => src.NameAr)
                .Map(dest => dest.NameEn, src => src.NameEn)
                .Map(dest => dest.FieldId, src => src.FieldId)
                .IgnoreNullValues(true);
            #endregion

            #region Queries Mapping
            config.NewConfig<Skill, GetAllSkillsGroupedSkillResponse>()
               .Map(dest => dest.Id, src => src.Id)
               .Map(dest => dest.Name, src => src.Localize(src.NameAr, src.NameEn));

            config.NewConfig<Skill, GetAllSkillsQueryResponse>()
                .Map(dest => dest.SkillId, src => src.Id)
                .Map(dest => dest.Name, src => src.Localize(src.NameAr, src.NameEn))
                .Map(dest => dest.FieldName, src => src.Field.Localize(src.Field.NameAr, src.Field.NameEn));
            #endregion
        }
    }
}
