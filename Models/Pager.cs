using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

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

        public string SortingParameter { get; set; }

        public bool IsDescending { get; set; }

        public List<SelectListItem> PageSizeOptions { get; set; } = new List<SelectListItem>();

        public Pager() { }

        public Pager(int totalItems, int currentPage, int pageSize, int startItemsShowing, int endItemsShowing, List<SelectListItem> pageSizeOptions, string sortBy, bool isDescending)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            int startPage = Math.Max(1, currentPage - ShowPages / 2);
            int endPage = Math.Min(totalPages, currentPage + ShowPages - 1);

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            StartItemsShowing = Math.Min(startItemsShowing, TotalItems);
            EndItemsShowing = Math.Min(endItemsShowing, TotalItems);
            PageSizeOptions = pageSizeOptions;
            SortingParameter = sortBy;
            IsDescending = isDescending;
        }
    }
}
