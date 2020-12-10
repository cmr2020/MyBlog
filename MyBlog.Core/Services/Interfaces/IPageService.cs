using MyBlog.DataLayer.Entities.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.Services.Interfaces
{
    public interface IPageService
    {
        IEnumerable<Page> GetAllPage();
        Page GetPageById(int pageId);
        void InsertPage(Page page);
        void UpdatePage(Page page);
        void DeletePage(Page page);
        void DeletePage(int pageId);
        void Save();
    }
}
