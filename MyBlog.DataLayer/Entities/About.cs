using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.DataLayer.Entities
{
  public  class About
    {
        [Key]
        public int AboutId { get; set; }
        public string Text { get; set; }
    }
}
