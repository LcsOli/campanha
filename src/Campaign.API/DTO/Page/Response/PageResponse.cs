namespace Campaign.API.DTO.Page.Response
{
    public class PageResponse<TContent>
        where TContent : class
    {
        public int TotalPages { get; }
        public int CurrentPage { get; }
        public int TotalElements { get; }
        public int CurrentElements { get; }
        public List<TContent> Content { get; }

        public PageResponse(int totalPages,
                            int currentPage,
                            int totalElements,
                            List<TContent> content)
        {
            Content = content;
            CurrentPage = currentPage;
            TotalElements = totalElements;
            CurrentElements = content.Count;
            TotalPages = totalPages <= 0 ? 1 : totalPages;
        }
    }

    public class PageResponse<TContent, TResume> : PageResponse<TContent>
        where TContent : class
        where TResume : class

    {
        public TResume Resume { get; }
        public PageResponse(int totalPages,
                            TResume resume,
                            int currentPage,
                            int totalElements,
                            List<TContent> content) : base(totalPages, currentPage, totalElements, content)
        {
            Resume = resume;
        }
    }
}
