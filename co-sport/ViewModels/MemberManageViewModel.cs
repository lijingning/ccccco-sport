using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using co_sport.Models;

namespace co_sport.ViewModels
{
    public class MemberManageViewModel
    {
        public Group Group { get; set; }

        public User User { get; set; }

        public bool IsManager { get; set; }

        public List<Request> Requests { get; set; }
    }
}