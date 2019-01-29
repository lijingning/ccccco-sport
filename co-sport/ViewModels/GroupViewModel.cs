using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace co_sport.ViewModels
{
    public class GroupViewModel
    {
        public Guid GroupID { get; set; }

        public string GroupName { get; set; }

        public string Abstract { get; set; }

        public int Count { get; set; }

        public bool IsManager { get; set; }
    }
}