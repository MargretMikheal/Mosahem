using Mapster;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;
using Mosahem.Application.Features.Authentication.Commands.CompleteVolunteerRegistration;

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

            config.NewConfig<Guid, VolunteerField>()
                .Map(dest => dest.FieldId, src => src)
                .Ignore(dest => dest.VolunteerId);

            config.NewConfig<Guid, VolunteerSkill>()
                .Map(dest => dest.SkillId, src => src)
                .Ignore(dest => dest.VolunteerId);


        }
    }
}
