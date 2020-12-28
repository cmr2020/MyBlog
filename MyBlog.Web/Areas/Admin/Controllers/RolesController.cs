using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Services.Interfaces;
using MyBlog.DataLayer.Entities.User;

namespace MyBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private IPermissionService _permissionService;


        public RolesController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public IActionResult Index()
        {
            return View(_permissionService.GetRoles());
        }


        public IActionResult CreateRole()
        {
            return View(_permissionService.GetRoles());
        }
    }
}
