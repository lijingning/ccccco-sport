using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace co_sport.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(9),MaxLength(9)]
        [Display(Name="学号/用户名*")]
        public string StuNum { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="密码*")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordConfirmed { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name="姓名*")]
        public string Name { get; set; }

        [Display(Name ="手机号")]
        [MinLength(11),MaxLength(11)]
        public string Contact { get; set; }

        [Display(Name ="微信号")]
        [MinLength(1)]
        public string WeChatID { get; set; }
    }
}