using Mapster;
using mosahem.Application.Features.Admin.Commands.AddAdmin;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Enums;
using Mosahem.Application.Features.Admin.Commands.EditBasicInfo;
using Mosahem.Application.Features.Admin.Queries.GetAdminById;
using Mosahem.Application.Features.Admin.Queries.GetAllAdmins;

namespace mosahem.Application.Mapping
{
    public class AdminMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            #region Command Mapping
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

            //Edit Basic Info Mapping
            config.NewConfig<EditAdminInfoCommand, MosahmUser>()
                .Map(dest => dest.FullName, src => src.UserName)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
                .IgnoreNullValues(true);
            #endregion

            #region Query Mapping
            //Get Admin By Id Mapping
            config.NewConfig<MosahmUser, GetAdminByIdQueryResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FullName, src => src.FullName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
            //Get All Admins Mapping
            config.NewConfig<MosahmUser, GetAllAdminsQueryResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FullName, src => src.FullName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
            #endregion
        }
    }
}