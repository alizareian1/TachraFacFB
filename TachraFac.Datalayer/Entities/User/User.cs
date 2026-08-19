using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Datalayer.Entities.Product;

namespace TachraFac.Datalayer.Entities.User
{
    public class User
    {

        public User()
        {
            
        }

        [Key]
        public int UserId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string UserName { get; set; }


        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string Name { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "کد فعالسازی")]
        public string ActiveCode { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "آواتار")]
        public string UserAvatar { get; set; }

        public bool IsDelete { get; set; }

        #region Relation
        public List<UserRole> userRoles { get; set; }
        

        public UserContact UserContact { get; set; }
        public virtual List<Wallet.Wallet> Wallets { get; set; }
        public ICollection<ProductLike> ProductLikes { get; set; }


        public ICollection<Contact> contacts { get; set; }

        #endregion
    }
}
