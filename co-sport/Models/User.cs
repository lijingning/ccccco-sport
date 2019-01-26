using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace co_sport.Models
{
    public class User
    {
        public int UserID { get; set; }

        public bool Gender { get; set; }

        public int StuNum { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual SportTimeTable SportTimeTable { get; set; }
    }
}