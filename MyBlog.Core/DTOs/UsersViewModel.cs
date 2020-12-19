﻿using MyBlog.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.DTOs
{
    public class UserForAdminViewModel
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

}
