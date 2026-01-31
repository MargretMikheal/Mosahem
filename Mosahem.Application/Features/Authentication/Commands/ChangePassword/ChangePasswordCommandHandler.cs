using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces.Security;

namespace mosahem.Application.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ResponseHandler responseHandler, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.Id);
            var generalError = _localizer[SharedResourcesKeys.General.OperationFailed].Value;

            if (user == null)
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>> { { "User", new List<string> { _localizer[SharedResourcesKeys.User.NotFound] } } });
            }

            if (!_passwordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash))
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>> { { "CurrentPassword", new List<string> { _localizer[SharedResourcesKeys.Auth.InvalidCredentials] } } });
            }

            user.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.Success.PasswordChanged]);
        }
    }
}