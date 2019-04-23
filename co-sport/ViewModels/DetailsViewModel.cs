using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using co_sport.Models;

namespace co_sport.ViewModels
{
    public class DetailsViewModel
    {
        public Group Group { get; set; }

        public string[,] Table { get; set; }
    }
}