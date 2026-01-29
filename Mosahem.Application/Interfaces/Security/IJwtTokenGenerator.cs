using mosahem.Domain.Entities.Identity;
using Mosahem.Application.DTOs.Auth;
using Mosahem.Domain.Entities.Identity;

namespace Mosahem.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        JwtAuthResult GenerateTokens(MosahmUser user);
        string GenerateRefreshToken();
    }
}  