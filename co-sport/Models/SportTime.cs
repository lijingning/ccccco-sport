using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace co_sport.Models
{
    public class SportTime
    {
        public Guid SportTimeID { get; set; }

        public int TimeID { get; set; }

        public virtual User User { get; set; }
    }
}