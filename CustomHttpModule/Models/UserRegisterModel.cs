using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomHttpModule.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage="Please, enter email")]
        [EmailAddress(ErrorMessage="Incorrect e-mail")]
        [Display(Name = "Email")]
        public string Email{get;set;}
        [Required(ErrorMessage = "Please, enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter confirm password")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don`t match")]
        public string ConfirmPassword { get; set; }
    }
}