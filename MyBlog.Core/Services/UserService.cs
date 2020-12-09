using MyBlog.Core.Services.Interfaces;
using MyBlog.DataLayer.Context;
using MyBlog.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Core.Services
{
    public class UserService : IUserService
    {
        private MyBlogContext _context;

        public UserService(MyBlogContext context)
        {
            _context = context;
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }
    }
}
