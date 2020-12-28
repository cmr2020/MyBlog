using MyBlog.DataLayer.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.DTOs.Role
{
   public class CreateRoleViewModel
    {
       
        public string RoleTitle { get; set; }
        public List<Permission> SelectedPermission { get; set; }

    }
}
