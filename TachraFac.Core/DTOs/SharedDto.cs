using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Core.DTOs
{
    public class UserSharedDtoViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

    }

    public class UserContactSharedDtoViewModel
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long PhoneNumber { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }

        [Display(Name = " کدپستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long PostalCode { get; set; }
    }
}
