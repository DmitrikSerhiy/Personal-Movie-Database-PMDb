using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMDb.Services.Helpers
{
    public class PagedList<T> : List<T> 
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        private bool hasNext;
        private bool hasPrevious;
        public bool HasPrevious
        {
            get => hasPrevious = CurrentPage > 1;
            private set => hasPrevious = value;
        }
        public bool HasNext
        {
            get => hasNext = CurrentPage < TotalPages;
            private set => hasNext = value;
        }
        private PagedList(ICollection<T> Items, int Count, int PageNumber, int PageSize)
        {
            TotalCount = Count;
            this.PageSize = PageSize;
            CurrentPage = PageNumber;
            TotalPages = (int)Math.Ceiling(Count / (double)this.PageSize);
            AddRange(Items);
        }

        public PagedList()
        {

        }

        public PagedList(int CurrentPage,
                         int TotalPages,
                         int PageSize,
                         int TotalCount,
                         bool HasPrevious,
                         bool HasNext)
        {
            this.CurrentPage = CurrentPage;
            this.TotalPages = TotalPages;
            this.PageSize = PageSize;
            this.TotalCount = TotalCount;
            this.HasPrevious = HasPrevious;
            this.HasNext = HasNext;
        }

        public static PagedList<T> Create(IEnumerable<T> Source, int PageNumber, int PageSize)
        {
            var count = Source.Count();
            var items = Source.Skip((PageNumber - 1) * PageSize)
                              .Take(PageSize)
                              .ToList();
            return new PagedList<T>(items, count, PageNumber, PageSize);
        }

        //public PagedList<T> InitDefault(PagedList<T> Source)
        //{
        //    PaginationParameters parameters = new PaginationParameters();
        //    Source.CurrentPage = 1;
        //    Source.HasNext = parameters.
        //} 
    }
}
