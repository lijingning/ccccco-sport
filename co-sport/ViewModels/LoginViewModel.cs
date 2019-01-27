using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace co_sport.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="学号")]
        public string StuNum { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="密码")]
        public string Password { get; set; }
    }
}