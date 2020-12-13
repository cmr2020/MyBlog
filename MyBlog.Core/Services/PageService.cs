using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DTOs;
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

        public bool PageExists(int pageId)
        {
            return _db.Pages.Any(p => p.PageID == pageId);
        }

        public IEnumerable<ShowTopPageViewModel> GetTopPage(int take = 4)
        {
            //return _db.Pages.OrderByDescending(p => p.PageVisit).Take(take).ToList();
            return _db.Pages.Include(p => p.PageGroup).OrderByDescending(p => p.PageVisit).Select(p => new ShowTopPageViewModel()
            {
                Imagename = p.ImageName,
                PageTitle = p.PageTitle,
                Grouptitle = _db.PageGroups.SingleOrDefault(c => c.GroupID == p.GroupID).GroupTitle,
                PageId = p.PageID

            }).Take(take).ToList();

        }

        public IEnumerable<GetLatesPageViewModel> GetLatesPage()
        {
            //return _db.Pages.OrderByDescending(p => p.CreateDate).Take(4).ToList();
            return _db.Pages.OrderByDescending(p => p.CreateDate).Select(p => new GetLatesPageViewModel()
            {
                ImageName = p.ImageName,
                PageTitle = p.PageTitle,
                ShortDescription = p.ShortDescription,
                PageGroupTitle = _db.PageGroups.SingleOrDefault(c => c.GroupID == p.GroupID).GroupTitle,
                PageId=p.PageID
            }).Take(4).ToList();

        }

        public IEnumerable<Page> GetPagesByGroupId(int groupId)
        {
            return _db.Pages.Where(p => p.GroupID == groupId).ToList();
        }

        public IEnumerable<Page> Search(string q)
        {
            var list = _db.Pages.Where(p =>
                p.PageTitle.Contains(q) || p.ShortDescription.Contains(q) || p.PageText.Contains(q) ||
                p.PageTags.Contains(q)).ToList();

            return list.Distinct().ToList();
        }
    }
}
