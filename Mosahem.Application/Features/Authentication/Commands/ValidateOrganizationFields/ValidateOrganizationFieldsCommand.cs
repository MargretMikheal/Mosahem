using MediatR;
using mosahem.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mosahem.Application.Features.Authentication.Commands.ValidateOrganizationFields
{
    public class ValidateOrganizationFieldsCommand : IRequest<Response<string>>
    {
        public List<Guid> FieldIds { get; set; }
    }
}
