using Microsoft.EntityFrameworkCore;
using MyBlog.Core.Services.Interfaces;
using MyBlog.DataLayer.Context;
using MyBlog.DataLayer.Entities.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Core.Services
{
    public class PageService : IPageService
    {
        private MyBlogContext _db;

        public PageService(MyBlogContext db)
        {
            _db = db;
        }

        public IEnumerable<Page> GetAllPage()
        {
            return _db.Pages.ToList();
        }

        public Page GetPageById(int pageId)
        {
            return _db.Pages.Find(pageId);
        }

        public void InsertPage(Page page)
        {
            _db.Pages.Add(page);
        }

        public void UpdatePage(Page page)
        {
            _db.Entry(page).State = EntityState.Modified;
        }

        public void DeletePage(Page page)
        {
            _db.Entry(page).State = EntityState.Deleted;
        }

        public void DeletePage(int pageId)
        {
            var page = GetPageById(pageId);
            DeletePage(page);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
