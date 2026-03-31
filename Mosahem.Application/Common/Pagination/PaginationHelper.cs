using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Mosahem.Application.Common.Pagination
{


    public static class PaginationHelper
    {
        public static PaginatedResponse<T> Create<T>(
            IReadOnlyList<T> items,
            int total,
            int page,
            int pageSize,
            HttpRequest request)
        {
            var lastPage = (int)Math.Ceiling(total / (double)pageSize);

            var path = $"{request.Scheme}://{request.Host}{request.Path}";

            var meta = new PaginationMeta
            {
                CurrentPage = page,
                From = total == 0 ? 0 : ((page - 1) * pageSize) + 1,
                To = ((page - 1) * pageSize) + items.Count,
                LastPage = lastPage,
                PerPage = pageSize,
                Total = total,
                Path = path,
                Links = GenerateLinks(page, lastPage, request)
            };

            return new PaginatedResponse<T>(items, meta);
        }

        private static IReadOnlyList<PageLink> GenerateLinks(
            int page,
            int lastPage,
            HttpRequest request)
        {
            var links = new List<PageLink>();

            var path = $"{request.Scheme}://{request.Host}{request.Path}";

            var query = request.Query
                .ToDictionary(x => x.Key, x => x.Value.ToString());


            // Previous
            query["Page"] = (page - 1).ToString();

            links.Add(new PageLink
            {
                Url = page > 1 ? QueryHelpers.AddQueryString(path, query) : null,
                Label = "Previous",
                Active = false
            });

            query["Page"] = page.ToString();

            //Current
            links.Add(new PageLink
            {
                Url = QueryHelpers.AddQueryString(path, query),
                Label = page.ToString(),
                Active = true
            });

            // Next
            query["Page"] = (page + 1).ToString();

            links.Add(new PageLink
            {
                Url = page < lastPage ? QueryHelpers.AddQueryString(path, query) : null,
                Label = "Next",
                Active = false
            });

            return links;
        }
    }
}
