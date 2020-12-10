using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Web.ViewComponents
{
    public class ShowTopPageComponent : ViewComponent
    {
        private IPageService _pageRepoitory;

        public ShowTopPageComponent(IPageService pageRepoitory)
        {
            _pageRepoitory = pageRepoitory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowTopPageComponent",
                _pageRepoitory.GetTopPage()));
        }
    }
}
