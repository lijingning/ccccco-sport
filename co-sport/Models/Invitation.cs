using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace co_sport.Models
{
    public class Invitation
    {
        public Guid InvitationID { get; set; }

        public bool? Agreed { get; set; }

        public string StuNum { get; set; }

        public string GroupName { get; set; }

        public Guid GroupID { get; set; }
    }

    public class Request
    {
        public Guid RequestID { get; set; }

        public bool? Agreed { get; set; }

        public string StuNum { get; set; }

        public string StuName { get; set; }

        public Guid GroupID { get; set; }
    }
}