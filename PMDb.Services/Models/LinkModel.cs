﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Models
{
    public class LinkModel
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }

        public LinkModel()
        {

        }
    }
}
