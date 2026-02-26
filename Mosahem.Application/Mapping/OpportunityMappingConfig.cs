using Mapster;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Questions;
using mosahem.Domain.Enums;
using Mosahem.Application.Features.Opportunities.Commands.CreateOpportunity;
using System.Text.Json;

namespace Mosahem.Application.Mapping
{
    public class OpportunityMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // 1. Opportunity Mapping
            config.NewConfig<CreateOpportunityCommand, Opportunity>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.Descripition, src => src.Description)
                .Map(dest => dest.Vacancies, src => src.NumberOfVolunteers)
                .Map(dest => dest.ApplicantsCount, src => 0)
                .Map(dest => dest.Status, src => OpportunityStatus.Open)
                .Map(dest => dest.VerificationStatus, src => VerficationStatus.Pending)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Address)
                .Ignore(dest => dest.OpportunitySkills)
                .Ignore(dest => dest.Questions)
                .Ignore(dest => dest.OpportunityFields);

            // 2. Address Mapping
            config.NewConfig<CreateOpportunityAddressDto, Address>()
                .Map(dest => dest.Id, src => Guid.NewGuid());

            // 3. Skills Mapping (Handling Guid to Entity)
            config.NewConfig<Guid, OpportunityProvideSkill>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.SkillId, src => src);

            config.NewConfig<Guid, OpportunityRequireSkill>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.SkillId, src => src);

            // 4. Fields Mapping
            config.NewConfig<Guid, OpportunityField>()
                .Map(dest => dest.FieldId, src => src);

            // 5. Questions Mapping 
            config.NewConfig<CreateOpportunityQuestionDto, Question>()
                 .Map(dest => dest.Id, src => Guid.NewGuid())
                 .Map(dest => dest.Description, src => src.Description)
                 .Map(dest => dest.AnswerType, src => src.AnswerType)
                 .Map(dest => dest.IsRequired, src => src.IsRequired)
                 .Ignore(dest => dest.Options)
                 .Ignore(dest => dest.OpportunityId)
                 .AfterMapping((src, dest) => dest.Options = BuildQuestionOptions(src.Options));

        }

        private static JsonDocument? BuildQuestionOptions(List<string>? options)
        {
            if (options is null || options.Count == 0)
            {
                return null;
            }

            return JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes(options));
        }
    }
}