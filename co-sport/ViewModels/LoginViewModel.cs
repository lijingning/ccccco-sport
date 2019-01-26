using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace co_sport.ViewModels
{
    public class LoginViewModel
    {
        public string StuNum { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}