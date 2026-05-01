using Mapster;
using mosahem.Application.Common.Opportunities;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;
using Mosahem.Application.Features.Authentication.Commands.CompleteVolunteerRegistration;
using Mosahem.Application.Features.Opportunities.Queries.GetApplicantsByStatus;
using Mosahem.Application.Features.Organizations.Queries.GetOrganizationVolunteersByVerificationStatus;
using Mosahem.Application.Features.Volunteers.Commands.EditVolunteerBasicInfoCommand;
using Mosahem.Application.Features.Volunteers.Queries.GetAllVolunteers;
using Mosahem.Application.Features.Volunteers.Queries.GetVolunteerFollowedOrganizations;
using Mosahem.Application.Features.Volunteers.Queries.GetVolunteerProfile;

namespace Mosahem.Application.Mapping
{
    public class VolunteerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CompleteVolunteerRegistrationCommand, MosahmUser>()
                .Map(dest => dest.FullName, src => src.FullName)
                .Map(dest => dest.UserName, src => src.Email)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
                .Map(dest => dest.Role, src => UserRole.Volunteer)
                .Map(dest => dest.AuthProvider, src => AuthProvider.Local)
                .Map(dest => dest.IsDeleted, src => false)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow);

            config.NewConfig<CompleteVolunteerRegistrationCommand, Volunteer>()
                .Map(dest => dest.NationalId, src => src.NationalId)
                .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
                .Map(dest => dest.Gender, src => src.Gender)
                .Map(dest => dest.TotalHours, src => 0)
                .Map(dest => dest.CompleteOpportunities, src => 0)
                .Map(dest => dest.CVKey, src => src.CvUrl);

            config.NewConfig<Volunteer, GetAllVolunteersQueryResponse>()
                .Map(dest => dest.ProfileImage, src => src.ProfileImgKey)
                .Map(dest => dest.FullName, src => src.User.FullName)
                .Map(dest => dest.Bio, src => (string?)null);

            config.NewConfig<Guid, VolunteerField>()
                .Map(dest => dest.FieldId, src => src)
                .Ignore(dest => dest.VolunteerId);

            config.NewConfig<Guid, VolunteerSkill>()
                .Map(dest => dest.SkillId, src => src)
                .Ignore(dest => dest.VolunteerId);


            config.NewConfig<OrganizationFollower, GetVolunteerFollowedOrganizationsResponse>()
                .Map(dest => dest.OrganizationId, src => src.OrganizationId)
                .Map(dest => dest.OrganizationName, src => src.Organization.User.FullName)
                .Map(dest => dest.OrganizationDescription, src => src.Organization.Description)
                .Map(dest => dest.OrganizationLogo, src => src.Organization.LogoKey);

            config.NewConfig<Volunteer, GetApplicantsByStatusResponse>()
                .Map(dest => dest.Name, src => src.User.FullName ?? "")
                .Map(dest => dest.Age,
                     src => src.DateOfBirth == null
                         ? (int?)null
                         : CalculateApplicantAge(src.DateOfBirth.Value))
                .Map(dest => dest.TotalHours, src => src.TotalHours)
                .Map(dest => dest.Bio, src => src.Bio)
                .IgnoreNonMapped(true);

            config.NewConfig<Volunteer, GetOrganizationVolunteersByVerificationStatusResponse>()
                .Map(dest => dest.VolunteerId, src => src.Id)
                .Map(dest => dest.Name, src => src.User.FullName ?? "")
                .Map(dest => dest.ProfileImgUrl, src => src.ProfileImgKey)
                .Map(dest => dest.Age,
                     src => src.DateOfBirth == null
                         ? (int?)null
                         : CalculateApplicantAge(src.DateOfBirth.Value))
                .Map(dest => dest.TotalHours, src => src.TotalHours)
                .Map(dest => dest.Bio, src => src.Bio)
                .IgnoreNonMapped(true);

            config.NewConfig<EditVolunteerBasicInfoCommand, Volunteer>()
                .Map(dest => dest.NationalId, src => src.NationalId)
                .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
                .Map(dest => dest.Gender, src => src.Gender)
                .Map(dest => dest.Bio, src => src.Bio)
                .IgnoreNonMapped(true)
                .IgnoreNullValues(true);

            config.NewConfig<VolunteerSkill, VolunteerMasterDataResponse>()
                .Map(dest => dest.Id, src => src.SkillId)
                .Map(dest => dest.Name, src => src.Skill.Localize(src.Skill.NameAr, src.Skill.NameEn));

            config.NewConfig<VolunteerField, VolunteerMasterDataResponse>()
                .Map(dest => dest.Id, src => src.FieldId)
                .Map(dest => dest.Name, src => src.Field.Localize(src.Field.NameAr, src.Field.NameEn));

            config.NewConfig<Opportunity, VolunteerOpportunityResponse>()
                .Map(dest => dest.OpportunityId, src => src.Id)
                .Map(dest => dest.OpportunityName, src => src.Title)
                .Map(dest => dest.OpportunityDescription, src => src.Descripition)
                .Map(dest => dest.OpportunityPhotoUrl, src => src.PhotoKey)
                .Map(dest => dest.WorkType, src => src.WorkType.ToString())
                .Map(dest => dest.LocationType, src => src.LocationType.ToString())
                .Map(dest => dest.OpportunityStatus, src => OpportunityStatusCalculator.ToNames(src.Status))
                .Map(dest => dest.StartDate, src => src.StartDate)
                .Map(dest => dest.EndDate, src => src.EndDate)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.Vacancies, src => src.Vacancies)
                .Map(dest => dest.ApplicantsCount, src => src.ApplicantsCount)
                .Map(dest => dest.Organization, src => src.Organization)
                .Map(dest => dest.Locations, src => src.Address);

            config.NewConfig<OpportunityApplication, VolunteerOpportunityResponse>()
                .Map(dest => dest, src => src.Opportunity);

            config.NewConfig<OpportunitySave, VolunteerOpportunityResponse>()
                .Map(dest => dest, src => src.Opportunity);

            config.NewConfig<Volunteer, GetVolunteerProfileResponse>()
                .Map(dest => dest.VolunteerId, src => src.Id)
                .Map(dest => dest.Name, src => src.User.FullName)
                .Map(dest => dest.ProfilePhoto, src => src.ProfileImgKey)
                .Map(dest => dest.CoverPhoto, src => src.CoverImgKey)
                .Map(dest => dest.Bio, src => src.Bio)
                .Map(dest => dest.PhoneNumber, src => src.User.PhoneNumber)
                .Map(dest => dest.Gender, src => src.Gender != null ? src.Gender.ToString() : null)
                .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
                .Map(dest => dest.TotalHours, src => src.TotalHours)
                .Map(dest => dest.CompletedOpportunitiesCount, src => src.CompleteOpportunities)
                .Map(dest => dest.Skills, src => src.VolunteerSkills)
                .Map(dest => dest.Fields, src => src.VolunteerFields)
                .Map(dest => dest.CompletedOpportunities, src => src.OpportunityApplications == null ? new List<OpportunityApplication>() : src.OpportunityApplications.Where(x => x.ApplicantStatus == ApplicantStatus.Accepted).ToList())
                .Map(dest => dest.SavedOpportunities, src => src.OpportunitySaves);

        }


        private static int CalculateApplicantAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
