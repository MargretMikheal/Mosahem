using Mapster;
using mosahem.Domain.Entities.Identity;
using Mosahem.Application.Features.Users.Queries.GetUserInfo;

namespace Mosahem.Application.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MosahmUser, GetUserInfoResponse>()
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber ?? string.Empty)
                .Map(dest => dest.Email, src => src.Email ?? string.Empty)
                .Map(dest => dest.Role, src => src.Role.ToString());
        }
    }
}
