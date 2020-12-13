using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.DTOs
{
    public class GetLatesPageViewModel
    {
        public string ImageName { get; set; }
        public string PageGroupTitle { get; set; }
        public string PageTitle { get; set; }
        public string ShortDescription { get; set; }
    }
}
