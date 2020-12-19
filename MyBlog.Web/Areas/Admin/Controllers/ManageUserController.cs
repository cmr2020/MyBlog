using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.DTOs;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageUserController : Controller
    {
        private IUserService _userService;
        public ManageUserController(IUserService userService)
        {
            _userService = userService;
        }



      
        public IActionResult Index(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            
            return View(_userService.GetUsers(pageId, filterEmail, filterUserName));
        }

       

    }
}
