using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Helpers
{
    public class GetMoviesParameters
    {
        private const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}
