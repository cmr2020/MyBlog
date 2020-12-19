using MyBlog.Core.Convertors;
using MyBlog.Core.DTOs;
using MyBlog.Core.Generator;
using MyBlog.Core.Security;
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

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixEmail(login.Email);
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _context.SaveChanges();

            return true;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users;

            if(!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if(!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }


            //Show Item In Page
            int take = 1;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();

            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }
    }
}
