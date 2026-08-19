using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Core.DTOs
{
    public class InformationUserViewModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Wallet { get; set; }
        public long PhoneNumber { get; set; }
        public long PostalCode { get; set; }
        public string Address { get; set; }
        public string UserAvatar { get; set; }
    }

    

        public class EditProfileViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public string Email { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public required string Name { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long PhoneNumber { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public required string Address { get; set; }

        [Display(Name = "کدپستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long PostalCode { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public required string OldPassword { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public required string Password { get; set; }

        [Display(Name = " تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public required string RePassword { get; set; }

        public string Email { get; set; }
        public DateTime RegiisterDate { get; set; }
        public int Wallet { get; set; }
        public int PhoneNumber { get; set; }
        public int CodePosti { get; set; }
        public string Address { get; set; }

    }
}
