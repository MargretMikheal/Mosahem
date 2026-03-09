using Mapster;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Questions;
using mosahem.Domain.Enums;
using Mosahem.Application.Features.Opportunities.Commands.CreateOpportunity;
using Mosahem.Application.Features.Opportunities.Queries.GetAllPendingOpportunities;
using Mosahem.Application.Features.Opportunities.Queries.GetOpportunityById;
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
            config.NewConfig<Opportunity, PendingOpportunityResponse>()
                .Map(dest => dest.OpportunityId, src => src.Id)
                .Map(dest => dest.OpportunityName, src => src.Title)
                .Map(dest => dest.OrganizationName, src => src.Organization.User != null ? src.Organization.User.FullName : string.Empty)
                .Map(dest => dest.OrganizationLogoUrl, src => src.Organization.LogoKey);

            config.NewConfig<Opportunity, OpportunityDetailsResponse>()
                .Map(dest => dest.OpportunityId, src => src.Id)
                .Map(dest => dest.OpportunityName, src => src.Title)
                .Map(dest => dest.OpportunityDescription, src => src.Descripition)
                .Map(dest => dest.OpportunityStatus, src => BuildOpportunityStatuses(src))
                .Map(dest => dest.VerificationStatus, src => src.VerificationStatus.ToString())
                .Map(dest => dest.WorkType, src => src.WorkType.ToString())
                .Map(dest => dest.LocationType, src => src.LocationType.ToString())
                .Map(dest => dest.NumberOfVolunteers, src => src.Vacancies)
                .Map(dest => dest.Organization, src => src.Organization)
                .Map(dest => dest.Locations, src => src.Address ?? new List<Address>())
                .Map(dest => dest.RequiredSkills,
                    src => (src.OpportunitySkills ?? new List<OpportunitySkill>())
                        .Where(skill => skill.SkillType == OpportunitySkillType.Require)
                        .ToList())
                .Map(dest => dest.ProvidedSkills,
                    src => (src.OpportunitySkills ?? new List<OpportunitySkill>())
                        .Where(skill => skill.SkillType == OpportunitySkillType.Provide)
                        .ToList())
                .Map(dest => dest.Fields,
                    src => (src.OpportunityFields ?? new List<OpportunityField>()).ToList())
                .Map(dest => dest.LikesCount, src => src.OpportunityLikes != null ? src.OpportunityLikes.Count : 0)
                .Map(dest => dest.CommentsCount, src => src.OpportunityComments != null ? src.OpportunityComments.Count : 0)
                .Map(dest => dest.SavesCount, src => src.OpportunitySaves != null ? src.OpportunitySaves.Count : 0)
                .Map(dest => dest.Questions,
                    src => (src.Questions ?? new List<Question>())
                        .OrderBy(question => question.Order)
                        .ToList());

            config.NewConfig<mosahem.Domain.Entities.Profiles.Organization, OpportunityOrganizationResponse>()
                .Map(dest => dest.OrganizationId, src => src.Id)
                .Map(dest => dest.OrganizationName, src => src.User != null ? src.User.FullName : string.Empty);

            config.NewConfig<Address, OpportunityLocationResponse>()
                .Map(dest => dest.CityName, src => src.City.Localize(src.City.NameAr, src.City.NameEn))
                .Map(dest => dest.GovernorateId, src => src.City.GovernorateId)
                .Map(dest => dest.GovernorateName,
                    src => src.City.Governorate.Localize(src.City.Governorate.NameAr, src.City.Governorate.NameEn));

            config.NewConfig<OpportunityField, OpportunityFieldResponse>()
                .Map(dest => dest.FieldName, src => src.Field.Localize(src.Field.NameAr, src.Field.NameEn));

            config.NewConfig<OpportunitySkill, OpportunitySkillResponse>()
                .Map(dest => dest.SkillName, src => src.Skill.Localize(src.Skill.NameAr, src.Skill.NameEn));

            config.NewConfig<Question, OpportunityQuestionResponse>()
                .Map(dest => dest.QuestionId, src => src.Id)
                .Map(dest => dest.AnswerType, src => src.AnswerType.ToString())
                .Map(dest => dest.Options, src => ParseQuestionOptions(src.Options));

        }

        private static List<string> BuildOpportunityStatuses(Opportunity opportunity)
        {
            var statuses = Enum.GetValues<OpportunityStatus>()
                .Where(status => opportunity.Status.HasFlag(status))
                .Select(status => status.ToString())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var now = DateTime.UtcNow;
            var hasVacancy = opportunity.ApplicantsCount < opportunity.Vacancies;

            if (now < opportunity.StartDate)
            {
                statuses.Add(OpportunityStatus.Open.ToString());
                statuses.Remove(OpportunityStatus.Active.ToString());
                statuses.Remove(OpportunityStatus.Ended.ToString());
            }
            else if (now >= opportunity.StartDate && now < opportunity.EndDate)
            {
                statuses.Add(OpportunityStatus.Active.ToString());
                if (hasVacancy)
                {
                    statuses.Add(OpportunityStatus.Open.ToString());
                    statuses.Remove(OpportunityStatus.Closed.ToString());
                }
                else
                {
                    statuses.Add(OpportunityStatus.Closed.ToString());
                    statuses.Remove(OpportunityStatus.Open.ToString());
                }
            }
            else if (now >= opportunity.EndDate)
            {
                statuses.Add(OpportunityStatus.Closed.ToString());
                statuses.Add(OpportunityStatus.Ended.ToString());
                statuses.Remove(OpportunityStatus.Open.ToString());
                statuses.Remove(OpportunityStatus.Active.ToString());
            }

            return statuses.ToList();
        }

        private static JsonDocument? BuildQuestionOptions(List<string>? options)
        {
            if (options is null || options.Count == 0)
            {
                return null;
            }

            return JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes(options));
        }
        private static List<string> ParseQuestionOptions(JsonDocument? options)
        {
            if (options is null || options.RootElement.ValueKind != JsonValueKind.Array)
            {
                return new List<string>();
            }

            return options.RootElement
                .EnumerateArray()
                .Where(option => option.ValueKind == JsonValueKind.String)
                .Select(option => option.GetString())
                .Where(option => !string.IsNullOrWhiteSpace(option))
                .Select(option => option!)
                .ToList();
        }
    }
}