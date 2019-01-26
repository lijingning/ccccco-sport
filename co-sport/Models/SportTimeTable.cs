using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace co_sport.Models
{
    public class SportTimeTable
    {
        public Guid SportTimeTableID { get; set; }

        public virtual ICollection<Time> Times { get; set; }

        public virtual User User { get; set; }
    }

    public class Time
    {
        public int TimeID { get; set; }

        public ICollection<Sport> Sports { get; set; }
    }

    public enum Sport
    {
        Running,Swinmming,Basketball,Volleyball,TableTennis,Tennis,Badminton,Soccer,
    }
}