using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Core.Services.Interfaces;
using MyBlog.DataLayer.Entities.PageGroup;

namespace MyBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageGroupsController : Controller
    {
        private IPageGroupService _pageService;
        public PageGroupsController(IPageGroupService pageGroupService)
        {
            _pageService = pageGroupService;
        }

        public IActionResult Index()
        {
            return View(_pageService.GetAllPageGroups());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pageGroup = _pageService.GetPageGroupById(id.Value);
            if (pageGroup == null)
            {
                return NotFound();
            }

            return View(pageGroup);

        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GroupID,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                _pageService.InsertPageGroup(pageGroup);
                _pageService.Save();
                return RedirectToAction("Index");
            }
            return View(pageGroup);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroup = _pageService.GetPageGroupById(id.Value);

            if (pageGroup == null)
            {
                return NotFound();
            }

            return View(pageGroup);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("GroupID,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _pageService.UpdatePageGroup(pageGroup);
                    _pageService.Save();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!PageGroupExists(pageGroup.GroupID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(pageGroup);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pageGroup = _pageService.GetPageGroupById(id.Value);

            if (pageGroup == null)
            {
                return NotFound();
            }

            return View(pageGroup);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(PageGroup pageGroup)
        {
            _pageService.DeletePageGroup(pageGroup);
            _pageService.Save();
            return RedirectToAction("Index");
        }

        private bool PageGroupExists(int id)
        {
            return _pageService.PageGroupExists(id);
        }


    }
}
