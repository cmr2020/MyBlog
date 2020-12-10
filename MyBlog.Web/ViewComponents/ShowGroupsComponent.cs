using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Web.ViewComponents
{
    public class ShowGroupsComponent : ViewComponent
    {
        private IPageGroupService _groupRepository;

        public ShowGroupsComponent(IPageGroupService groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowGroupsComponent",
                _groupRepository.GetListGroups()));
        }
    }
}
