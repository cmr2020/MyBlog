using Microsoft.EntityFrameworkCore;
using MyBlog.DataLayer.Entities;
using MyBlog.DataLayer.Entities.Page;
using MyBlog.DataLayer.Entities.PageGroup;
using MyBlog.DataLayer.Entities.Permissions;
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
        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion
        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<About> Abouts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);
            base.OnModelCreating(modelBuilder);
        }

    }
}
