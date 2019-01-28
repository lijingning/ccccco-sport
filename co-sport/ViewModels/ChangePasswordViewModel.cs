using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace co_sport.ViewModels
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required]
        [Display(Name ="初始密码")]
        public string OriginalPassword { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name ="请输入新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name ="请再次输入新密码")]
        public string NewPasswordConfirmed { get; set; }
    }
}