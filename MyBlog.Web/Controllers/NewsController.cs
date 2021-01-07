using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Services.Interfaces;
using MyBlog.DataLayer.Entities.Page;

namespace MyBlog.Web.Controllers
{
    public class NewsController : Controller
    {
        private IPageService pageRepoitory;
        private IUserService _userService;
        public NewsController(IPageService pageRepoitory, IUserService userService)
        {
            this.pageRepoitory = pageRepoitory;
            _userService = userService;
        }


        [Route("News/{newsId}")]
        public IActionResult ShowNews(int newsId)
        {
            var page = pageRepoitory.GetPageById(newsId);

            if (page != null)
            {
                page.PageVisit += 1;
                pageRepoitory.UpdatePage(page);
                pageRepoitory.Save();
            }

            return View(page);
        }

        [Route("Group/{groupId}/{title}")]
        public IActionResult ShowNewsByGroupId(int groupId, string title)
        {
            ViewData["GroupTitle"] = title;
            return View(pageRepoitory.GetPagesByGroupId(groupId));
        }

        [Route("Search")]
        public IActionResult Search(string q)
        {
            return View(pageRepoitory.Search(q));
        }


        [HttpPost]
        public IActionResult CreateComment(PageComment comment)
        {
            comment.IsDelete = false;
            comment.CreateDate = DateTime.Now;
            comment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);
            pageRepoitory.AddComment(comment);

            return View("ShowComment", pageRepoitory.GetPageComment(comment.PageID));
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return View(pageRepoitory.GetPageComment(id, pageId));
        }


    }

}

