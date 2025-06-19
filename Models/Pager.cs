namespace CareerHub.Models
{
    public class Pager
    {
        private const int ShowPages = 5;
        public int TotalItems { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int StartItemsShowing { get; set; }

        public int EndItemsShowing { get; set; }

        public Pager() { }

        public Pager(int totalItems, int currentPage, int pageSize, int startItemsShowing, int endItemsShowing)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            int startPage = currentPage - ShowPages / 2;
            int endPage = currentPage + ShowPages - 1;  

            if(startPage < 1)
            {
                endPage = endPage - startPage + 1;
                startPage = 1;
            }

            if(endPage > totalPages)
            {
                endPage = totalPages;
                if(endPage > ShowPages)
                {
                    startPage = endPage - startPage + 1;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            StartItemsShowing = Math.Min(startItemsShowing, TotalItems);
            EndItemsShowing = Math.Min(endItemsShowing, TotalItems);
        }
    }
}
