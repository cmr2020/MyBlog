using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.DTOs
{
    public class ShowGroupsViewModel
    {
        public int GroupID { get; set; }
        public string GroupTitle { get; set; }
        public int PageCount { get; set; }

    }
}
