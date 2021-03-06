﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.DTOs;
using MyBlog.Core.Security;
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


        [PermissionChecker(2)]

        public IActionResult Index(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {

            return View(_userService.GetUsers(pageId, filterEmail, filterUserName));
        }


        [PermissionChecker(3)]
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



        [PermissionChecker(4)]
        public IActionResult EditUser(int id)
        {
            var edituser = _userService.GetUserForShowInEditMode(id);
            ViewData["Roles"] = _permissionService.GetRoles();
            return View(edituser);
        }

        [HttpPost]
        public IActionResult EditUser(List<int> SelectedRoles, EditUserViewModel editUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _userService.EditUserFormAdmin(editUserViewModel);

            //Edit Roles
            _permissionService.EditRolesUser(editUserViewModel.UserId, SelectedRoles);


            return RedirectToAction("Index");


        }


        public IActionResult ListDeleteUsers(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            return View(_userService.GetDeleteUsers(pageId, filterEmail, filterUserName));
        }

        [PermissionChecker(5)]
        public IActionResult DeleteUser(int id)
        {
            ViewData["UserId"] = id;
            var deleteUser = _userService.GetUserInformation(id);
            return View(deleteUser);
        }


        [HttpPost]
        public IActionResult DeleteUser(int Userid,InformationUserViewModel informationUserViewModel)
        {
            _userService.DeleteUser(Userid);
            return RedirectToAction("Index");
        }


        public IActionResult PanelAdmin()
        {
            return View();
        }

       


    }
}
