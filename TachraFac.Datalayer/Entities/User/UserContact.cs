using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Datalayer.Entities.User
{
    public class UserContact
    {
        #region Relation
        public int UserId { get; set; } // FK به جدول
        public User User { get; set; }
        #endregion

        //[Display(Name = "ایمیل")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        //[EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        //public required string Email { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Int64 PhoneNumber { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public required string Address { get; set; }

        [Display(Name = "کدپستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Int64 PostalCode { get; set; }


    }
}
