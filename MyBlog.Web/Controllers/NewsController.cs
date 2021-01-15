using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Security;
using MyBlog.Core.Services.Interfaces;
using MyBlog.DataLayer.Entities.Page;

namespace MyBlog.Web.Controllers
{

    public class NewsController : Controller
    {

        private IPageService pageRepoitory;
        private IUserService _userService;
        private IPermissionService _permissionService;
        public NewsController(IPageService pageRepoitory, IUserService userService, IPermissionService permissionService)
        {
            this.pageRepoitory = pageRepoitory;
            _userService = userService;
            _permissionService = permissionService;
        }


        [Route("News/{newsId}")]
        public IActionResult ShowNews(int newsId)
        {


            if (User.Identity.IsAuthenticated)
            {
                if (_permissionService.CheckPermission(18, User.Identity.Name))
                {
                    ViewBag.Checkrole = true;
                }
            }
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

        public IActionResult PageVote()
        {
            return PartialView();
        }

        [Authorize]
        public IActionResult AddVote(int id, bool vote)
        {
            pageRepoitory.AddVote(_userService.GetUserIdByUserName(User.Identity.Name), id, vote);

            return PartialView("PageVote", pageRepoitory.GetPageVotes(id));
        }


        [PermissionChecker(18)]
        public IActionResult PageVoteAdmin(int Id)
        {


            return PartialView(pageRepoitory.GetPageVotes(Id));

        }


    }

}

