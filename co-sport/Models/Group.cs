using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace co_sport.Models
{
    public class Group
    {
        public Guid GroupID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual User Manager { get; set; }
    }
}