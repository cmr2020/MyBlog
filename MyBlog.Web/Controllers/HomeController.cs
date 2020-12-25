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

namespace MyBlog.Web.Controllers
{

    public class HomeController : Controller
    {
        private IPageService _pageRepoitory;
        private IHostingEnvironment _environment;

        public HomeController(IPageService pageRepoitory, IHostingEnvironment environment)
        {
            _pageRepoitory = pageRepoitory;
            _environment = environment;
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
