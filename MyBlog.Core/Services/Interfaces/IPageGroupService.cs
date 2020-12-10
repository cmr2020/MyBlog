using MyBlog.Core.DTOs;
using MyBlog.DataLayer.Entities.PageGroup;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.Services.Interfaces
{
    public interface IPageGroupService
    {
        List<PageGroup> GetAllPageGroups();
        PageGroup GetPageGroupById(int groupId);
        void InsertPageGroup(PageGroup pageGroup);
        void UpdatePageGroup(PageGroup pageGroup);
        void DeletePageGroup(PageGroup pageGroup);
        void DeletePageGroup(int groupId);
        bool PageGroupExists(int pageGroupId);
        List<ShowGroupsViewModel> GetListGroups();
        void Save();
    }
}
