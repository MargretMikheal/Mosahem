using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Files.Queries.GetFileUrl
{
    public class GetFileUrlQuery : IRequest<Response<string>>
    {
        public string Key { get; set; }
        public bool IsPrivate { get; set; } = true;

        public GetFileUrlQuery(string key, bool isPrivate)
        {
            Key = key;
            IsPrivate = isPrivate;
        }
    }
}