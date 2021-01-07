using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Services.Interfaces;
using MyBlog.DataLayer.Entities.Page;

namespace MyBlog.Web.Controllers
{

    public class HomeController : Controller
    {
        private IPageService _pageRepoitory;
        private IUserService _userService;

        public HomeController(IPageService pageRepoitory, IUserService userService)
        {
            _pageRepoitory = pageRepoitory;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(_pageRepoitory.GetLatesPage());
        }

        [Authorize]
        public IActionResult Test() => View();

        public IActionResult Err404()
        {
            return View();
        }
    }
}
