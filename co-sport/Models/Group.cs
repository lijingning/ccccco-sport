using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace co_sport.Models
{
    public class Group
    {
        public Guid GroupID { get; set; }

        [Required]
        [Display(Name="团体名称")]
        public string Name { get; set; }

        [Display(Name="团体简介")]
        public string Abstract { get; set; }

        [Display(Name="团体人数")]
        public int Count
        {
            get
            {
                return Users.Count + 1;
            }
        }

        public virtual ICollection<User> Users { get; set; }

        public virtual User Manager { get; set; }
    }
}