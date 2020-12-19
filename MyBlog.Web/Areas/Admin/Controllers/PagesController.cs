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
using MyBlog.DataLayer.Entities.Page;

namespace MyBlog.Web.Areas.Admin.Controllers
{
    [PermissionChecker(1)]
    [Area("Admin")]
    public class PagesController : Controller
    {
        private IPageService _pageService;
        private IPageGroupService _pageGroupService;
        public PagesController(IPageService pageService, IPageGroupService pageGroupService)
        {
            _pageService = pageService;
            _pageGroupService = pageGroupService;
        }
        public IActionResult Index()
        {
            return View(_pageService.GetAllPage());
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageService.GetPagesByGroupId(id.Value);

            if (page == null)
            {
                return NotFound();
            }

            return View(page);

        }


        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_pageGroupService.GetAllPageGroups(), "GroupID", "GroupTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PageID,GroupID,PageTitle,ShortDescription,PageText,PageVisit,ImageName,PageTags,ShowInSlider,CreateDate")] Page page, IFormFile imgup)
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

                _pageService.InsertPage(page);
                _pageService.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupID"] = new SelectList(_pageGroupService.GetAllPageGroups(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }


        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageService.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_pageGroupService.GetAllPageGroups(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("PageID,GroupID,PageTitle,ShortDescription,PageText,PageVisit,ImageName,PageTags,ShowInSlider,CreateDate")] Page page, IFormFile imgup)
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
                             imgup.CopyToAsync(stream);
                        }

                    }
                    _pageService.UpdatePage(page);
                    _pageService.Save();
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
            ViewData["GroupID"] = new SelectList(_pageGroupService.GetAllPageGroups(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageService.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Page page)
        {
            _pageService.DeletePage(page);
            _pageService.Save();
            return RedirectToAction("Index");
        }

        private bool PageExists(int id)
        {
            return _pageService.PageExists(id);
        }
    }
}
