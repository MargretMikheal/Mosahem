namespace Mosahem.Application.Common.Pagination
{
    public class PaginatedResponse<T>
    {
        public IReadOnlyList<T> Items { get; set; }

        public PaginationMeta Meta { get; set; }

        public PaginatedResponse(IReadOnlyList<T> items, PaginationMeta meta)
        {
            Items = items;
            Meta = meta;
        }
    }
    public class PaginationMeta
    {
        public int CurrentPage { get; set; }

        public int From { get; set; }

        public int LastPage { get; set; }

        public int PerPage { get; set; }

        public int To { get; set; }

        public int Total { get; set; }

        public string Path { get; set; }

        public IReadOnlyList<PageLink> Links { get; set; }
    }
    public class PageLink
    {
        public string? Url { get; set; }

        public string Label { get; set; }

        public bool Active { get; set; }
    }
}
