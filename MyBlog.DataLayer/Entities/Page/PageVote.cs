using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.DataLayer.Entities.Page
{
    public class PageVote
    {

        [Key]
        public int VoteId { get; set; }
        [Required]
        public int PageID { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool Vote { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.Now;


        public User.User User { get; set; }
        public Page Page { get; set; }

    }
}
