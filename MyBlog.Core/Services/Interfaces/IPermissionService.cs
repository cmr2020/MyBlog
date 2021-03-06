﻿using MyBlog.Core.DTOs.Role;
using MyBlog.DataLayer.Entities.Permissions;
using MyBlog.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Roles

        List<Role> GetRoles();
        int AddRole(Role role);
        Role GetRoleById(int roleId);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditRolesUser(int userId, List<int> rolesId);
        EditRoleViewModel GetRoleViewModel(int id);

        #endregion

        #region Permission

        List<Permission> GetAllPermission();
        CreateRoleViewModel GetPermissionRoleViewModel();
        void AddPermissionsToRole(int roleId, List<int> permission);
        List<int> PermissionsRole(int roleId);
        void UpdatePermissionsRole(int roleId, List<int> permissions);

        bool CheckPermission(int permissionId, string userName);
        //CreateRoleViewModel CreateRole();


        #endregion
    }
}
