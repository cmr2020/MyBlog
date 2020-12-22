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
        IPermissionService _permissionService;
        public ManageUserController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }




        public IActionResult Index(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {

            return View(_userService.GetUsers(pageId, filterEmail, filterUserName));
        }



        public IActionResult CreateUser()
        {
            ViewData["Roles"] = _permissionService.GetRoles();
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(List<int> SelectedRoles, CreateUserViewModel createUserViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            int userId = _userService.AddUserFromAdmin(createUserViewModel);


            //Add Roles
            _permissionService.AddRolesToUser(SelectedRoles, userId);



            return RedirectToAction("index");
        }




        public IActionResult EditUser(int id)
        {
            var edituser = _userService.GetUserForShowInEditMode(id);
            ViewData["Roles"] = _permissionService.GetRoles();
            return View(edituser);
        }

        [HttpPost]
        public IActionResult EditUser(List<int> SelectedRoles,EditUserViewModel editUserViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            _userService.EditUserFormAdmin(editUserViewModel);

            //Edit Roles
            _permissionService.EditRolesUser(editUserViewModel.UserId, SelectedRoles);


           return RedirectToAction("Index");

            
        }


    }
}
