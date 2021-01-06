using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.DTOs.Role;
using MyBlog.Core.Security;
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
        [PermissionChecker(6)]
        public IActionResult Index()
        {
            return View(_permissionService.GetRoles());
        }

        [PermissionChecker(7)]
        public IActionResult CreateRole()
        {

            return View(_permissionService.GetPermissionRoleViewModel());
        }

        [HttpPost]
        public IActionResult CreateRole(CreateRoleViewModel model, List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return View(model);

            Role role = new Role()
            {
                IsDelete = false,
                RoleTitle= model.RoleTitle
            };
            int roleId = _permissionService.AddRole(role);

             model.SelectedPermission.Select(p => p.PermissionId).ToList();
            _permissionService.AddPermissionsToRole(roleId, SelectedPermission);

            return RedirectToAction("Index");
        }

        [PermissionChecker(8)]
        public IActionResult EditRole(int id)
        {
            _permissionService.GetPermissionRoleViewModel();
            ViewData["SelectedPermissions"] = _permissionService.PermissionsRole(id);
            return View(_permissionService.GetRoleViewModel(id));
        }

        [HttpPost]
        public IActionResult EditRole(EditRoleViewModel editRoleViewModel, List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return View();
            Role role = new Role()
            {
                RoleId=editRoleViewModel.RoleId,

                IsDelete = false,
                RoleTitle = editRoleViewModel.RoleTitle
            };

            _permissionService.UpdateRole(role);

            _permissionService.UpdatePermissionsRole(role.RoleId,SelectedPermission);

            return RedirectToAction("Index");
        }

        [PermissionChecker(9)]
        public IActionResult DeleteRole(int id)
        {
            return View(_permissionService.GetRoleViewModel(id));
        }


        [HttpPost]
        public IActionResult DeleteRole(EditRoleViewModel editRoleViewModel)
        {

            Role role = new Role()
            {
                RoleId = editRoleViewModel.RoleId,

                IsDelete = false,
                RoleTitle = editRoleViewModel.RoleTitle
            };

            _permissionService.DeleteRole(role);
            return RedirectToAction("Index");
        }




    }
}
