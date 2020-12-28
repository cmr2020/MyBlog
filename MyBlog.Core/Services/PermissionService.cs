using MyBlog.Core.DTOs.Role;
using MyBlog.Core.Services.Interfaces;
using MyBlog.DataLayer.Context;
using MyBlog.DataLayer.Entities.Permissions;
using MyBlog.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private MyBlogContext _db;

        public PermissionService(MyBlogContext db)
        {
            _db = db;
        }
        public void AddPermissionsToRole(int roleId, List<int> permission)
        {
            throw new NotImplementedException();
        }

        public int AddRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _db.UserRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }

            _db.SaveChanges();
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            throw new NotImplementedException();
        }

        public CreateRoleViewModel GetPermissionRoleViewModel()
        {
            return new CreateRoleViewModel() {
                SelectedPermission = GetAllPermission()
            };

        }

        public void DeleteRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void EditRolesUser(int userId, List<int> rolesId)
        {
            //Delete All Roles User
            _db.UserRoles.Where(r => r.UserId == userId).ToList().ForEach(r => _db.UserRoles.Remove(r));

            //Add New Roles
            AddRolesToUser(rolesId, userId);
        }

        public List<Permission> GetAllPermission()
        {
            throw new NotImplementedException();
        }

        public Role GetRoleById(int roleId)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRoles()
        {
            return _db.Roles.ToList();
        }

        public List<int> PermissionsRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
