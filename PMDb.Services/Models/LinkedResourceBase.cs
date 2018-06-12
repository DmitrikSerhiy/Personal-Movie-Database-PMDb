using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Models
{
    public abstract class LinkedResourceBase
    {
        public List<LinkModel> Links { get; set; }
        = new List<LinkModel>();

        public List<LinkModel> LinksForPagination { get; set; } = null;

    }
}
