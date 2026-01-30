using Mapster;
using mosahem.Application.Features.Admin.Commands.AddAdmin;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Enums;

namespace mosahem.Application.Mapping
{
    public class AdminMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddAdminCommand, MosahmUser>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.UserName, src => src.Email)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.FullName, src => src.FullName)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
                .Map(dest => dest.Role, src => UserRole.Admin)
                .Map(dest => dest.AuthProvider, src => AuthProvider.Local)
                .Map(dest => dest.IsDeleted, src => false)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.PasswordHash);
        }
    }
}