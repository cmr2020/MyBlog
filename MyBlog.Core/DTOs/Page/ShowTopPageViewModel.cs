using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.DTOs
{
    public class ShowTopPageViewModel
    {
        public int PageId { get; set; }
        public string Imagename { get; set; }
        public string Grouptitle { get; set; }
        public string PageTitle { get; set; }
        public int PageVisit { get; set; }
    }
}
