using Mapster;
using mosahem.Domain.Entities.MasterData;
using Mosahem.Application.Features.Skills.Commands.AddSkill;
using Mosahem.Application.Features.Skills.Commands.EditSkill;

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
                .Map(dest => dest.Category, src => src.Category)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow);

            config.NewConfig<EditSkillCommand, Skill>()
                .Map(dest => dest.NameAr, src => src.NameAr)
                .Map(dest => dest.NameEn, src => src.NameEn)
                .Map(dest => dest.Category, src => src.Category)
                .IgnoreNullValues(true);
            #endregion
        }
    }
}
