using MyBlog.DataLayer.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Core.DTOs.Role
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "لطفا نام نقش را وارد کنید")]
        public string RoleTitle { get; set; }
        public List<Permission> SelectedPermission { get; set; }

    }

    public class EditRoleViewModel
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "لطفا نام نقش را وارد کنید")]
        public string RoleTitle { get; set; }
        public List<Permission> SelectedPermission { get; set; }
        public bool  IsDelete { get; set; }
    }
}
