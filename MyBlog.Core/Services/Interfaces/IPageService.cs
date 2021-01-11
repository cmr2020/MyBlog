using MyBlog.Core.DTOs;
using MyBlog.DataLayer.Entities.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Core.Services.Interfaces
{
    public interface IPageService
    {

        #region Page
        IEnumerable<Page> GetAllPage();
        IEnumerable<ShowTopPageViewModel> GetTopPage(int take = 4);
        IEnumerable<GetLatesPageViewModel> GetLatesPage();
        IEnumerable<Page> GetPagesByGroupId(int groupId);
        IEnumerable<Page> Search(string q);
        Page GetPageById(int pageId);
        void InsertPage(Page page);
        void UpdatePage(Page page);
        void DeletePage(Page page);
        void DeletePage(int pageId);
        bool PageExists(int pageId);     
        void Save();
        #endregion

        #region Comments

        void AddComment(PageComment comment);
        Tuple<List<PageComment>,int> GetPageComment(int pageId,int paging=1);

        #endregion

        #region Page Vote

        void AddVote(int userId, int pageId, bool vote);
        Tuple<int, int> GetPageVotes(int pageId);

        #endregion
    }
}
