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

        public virtual Group Inviter { get; set; }

        public virtual User Receptor { get; set; }
    }
}