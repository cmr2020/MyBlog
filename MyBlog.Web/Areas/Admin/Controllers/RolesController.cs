using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.DTOs.Role;
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
            return View(_permissionService.GetPermissionRoleViewModel());
        }

        [HttpPost]
        public IActionResult CreateRole(CreateRoleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Role role = new Role()
            {
                IsDelete = false,
                RoleTitle= model.RoleTitle
            };
            int roleId = _permissionService.AddRole(role);

            List<int> selectedPermission = model.SelectedPermission.Select(p => p.PermissionId).ToList();
            _permissionService.AddPermissionsToRole(roleId,selectedPermission );

            return RedirectToPage("Index");
        }
    }
}
