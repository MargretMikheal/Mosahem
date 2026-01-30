using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Identity;
using Mosahem.Application.Interfaces.Security;

namespace mosahem.Application.Features.Admin.Commands.AddAdmin
{
    public class AddAdminCommandHandler : IRequestHandler<AddAdminCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public AddAdminCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {
            var adminUser = _mapper.Map<MosahmUser>(request);

            adminUser.PasswordHash = _passwordHasher.HashPassword(request.Password);

            await _unitOfWork.Users.AddAsync(adminUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Created<string>(_localizer[SharedResourcesKeys.Success.AdminAdded]);
        }
    }
}