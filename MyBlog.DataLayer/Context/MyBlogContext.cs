using Microsoft.EntityFrameworkCore;
using MyBlog.DataLayer.Entities.Page;
using MyBlog.DataLayer.Entities.PageGroup;
using MyBlog.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.DataLayer.Context
{
   public class MyBlogContext : DbContext
    {
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {

        }

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        #endregion

        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<Page> Pages { get; set; }

    }
}
