using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Core.Security;
using MyBlog.Core.Services.Interfaces;
using MyBlog.DataLayer.Context;
using MyBlog.DataLayer.Entities.Page;

namespace MyBlog.Web.Areas.Admin.Controllers
{
    [PermissionChecker(1)]
    [Area("Admin")]
    public class PagesController : Controller
    {
        private IPageService _pageRepoitory;
        private IPageGroupService _pageGroupRepository;

        public PagesController(IPageService pageRepoitory, IPageGroupService pageGroupRepository)
        {
            _pageRepoitory = pageRepoitory;
            _pageGroupRepository = pageGroupRepository;
        }


        // GET: Admin/Pages
        public async Task<IActionResult> Index()
        {
            var myCmsDbContext = _pageRepoitory.GetAllPage();
            return View(myCmsDbContext);
        }

        // GET: Admin/Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageRepoitory.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Admin/Pages/Create
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_pageGroupRepository.GetAllPageGroups(), "GroupID", "GroupTitle");
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageID,GroupID,PageTitle,ShortDescription,PageText,PageVisit,ImageName,PageTags,ShowInSlider,CreateDate")] Page page, IFormFile imgup)
        {
            if (ModelState.IsValid)
            {
                page.PageVisit = 0;
                page.CreateDate = DateTime.Now;

                if (imgup != null)
                {
                    page.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/PageImages", page.ImageName
                    );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        imgup.CopyToAsync(stream);
                    }

                }

                _pageRepoitory.InsertPage(page);
                _pageRepoitory.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupID"] = new SelectList(_pageGroupRepository.GetAllPageGroups(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // GET: Admin/Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageRepoitory.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_pageGroupRepository.GetAllPageGroups(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PageID,GroupID,PageTitle,ShortDescription,PageText,PageVisit,ImageName,PageTags,ShowInSlider,CreateDate")] Page page, IFormFile imgup)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    if (imgup != null)
                    {

                        if (page.ImageName == null)
                        {
                            page.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);
                        }

                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/PageImages", page.ImageName
                        );
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PageImages", page.ImageName);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await imgup.CopyToAsync(stream);
                        }

                    }
                    _pageRepoitory.UpdatePage(page);
                    _pageRepoitory.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PageID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupID"] = new SelectList(_pageGroupRepository.GetAllPageGroups(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // GET: Admin/Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageRepoitory.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Page pages)
        {
            _pageRepoitory.DeletePage(pages);
            _pageRepoitory.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _pageRepoitory.PageExists(id);
        }
    }
}
