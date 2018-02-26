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

        public bool HasPrevious
        {
            get => CurrentPage > 1;
        }
        public bool HasNext
        {
            get => CurrentPage < TotalPages;
        }
        private PagedList(List<T> Items, int Count, int PageNumber, int PageSize)
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

        public static PagedList<T> Create(IQueryable<T> Source, int PageNumber, int PageSize)
        {
            var count = Source.Count();
            var items = Source.Skip((PageNumber - 1) * PageSize)
                              .Take(PageSize)
                              .ToList();
            return new PagedList<T>(items, count, PageNumber, PageSize);
        }
    }
}
