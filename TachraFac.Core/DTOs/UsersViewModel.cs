using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Datalayer.Entities.User;

namespace TachraFac.Core.DTOs
{
    public class UsersForAdminViewModel
    {
        public List<User> users { get; set; }
        public int CurentPage { get; set; }
        public int PageCount { get; set; }
    }

    public class CreateUserViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public required string UserName { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public required string Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public required string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public required string Password { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long Mobile { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }

        [Display(Name = " کدپستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Int64 PostalCode { get; set; }

        public IFormFile userAvatar { get; set; }
        //public string? AvatarName { get; set; }
        //public List<int> SelectedRoles { get; set; }
    }

    public class EditUserViewModel 
    {
        public int userId { get; set; }
        public required string UserName { get; set; }       
        public required string Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public required string Email { get; set; }

        [Display(Name = "کلمه عبور")]   
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "شماره موبایل")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long Mobile { get; set; }

        [Display(Name = "آدرس")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }

        [Display(Name = " کدپستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Int64 PostalCode { get; set; }

        public IFormFile userAvatar { get; set; }
        public List<int> UserRoles { get; set; }
        public string AvatarName { get; set; }
    }

}
