using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Services.Interfaces;

namespace MyBlog.Web.Controllers
{
   
    public class HomeController : Controller
    {
        private IPageService _pageRepoitory;

        public HomeController(IPageService pageRepoitory)
        {
            _pageRepoitory = pageRepoitory;
        }
        public IActionResult Index()
        {
            return View(_pageRepoitory.GetLatesPage());
        }

        [Authorize]
        public IActionResult Test() => View();
    }
}
