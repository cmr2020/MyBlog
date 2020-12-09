using MyBlog.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddUser(User user);
    }
}
