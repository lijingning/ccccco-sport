using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace co_sport.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [Display(Name ="学号")]
        public int StuNum { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name ="姓名")]
        public string Name { get; set; }

        [Display(Name ="手机")]
        public string Contact { get; set; }

        [Display(Name="微信号")]
        public string WeChatID { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual SportTimeTable SportTimeTable { get; set; }
    }
}