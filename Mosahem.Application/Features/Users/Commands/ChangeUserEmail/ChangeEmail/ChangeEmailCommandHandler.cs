using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;
using Mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Users.Commands.ChangeUserEmail.ChangeEmail
{
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IOtpService _otpService;

        public ChangeEmailCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IOtpService otpService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _otpService = otpService;
        }

        public async Task<Response<string>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);
            try
            {
                await _otpService.MakeAsUsedAsync(
                    request.UserId,
                    request.Email,
                    request.Code,
                    OtpPurpose.ChangeEmailVerification,
                    cancellationToken);

                request.Adapt(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }
            catch (InvalidOperationException ex)
            {
                return _responseHandler.BadRequest<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        {"Otp" , new() { _localizer[ex.Message] } }
                    });
            }

            return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.General.Updated]);
        }
    }
}
