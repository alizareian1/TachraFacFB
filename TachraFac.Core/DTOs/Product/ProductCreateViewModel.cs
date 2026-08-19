using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Core.DTOs.Product
{
    public class ProductCreateViewModel
    {       
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string ProductTitle { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]        
        public required string Discription { get; set; }

        [Display(Name = "تاریخ انقضا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string ExpirationDate { get; set; }

        [Display(Name = "وزن")]
        public short Weight { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        // برای انتخاب مواد اولیه (مثلاً در یک Dropdown یا Checkbox)
        public List<int> MaterialIds { get; set; } = new();

        // برای آپلود فایل‌های عکس
        [Display(Name = "انتخاب عکس‌ها")]
        public List<IFormFile> PhotoPaths { get; set; } = new();
    }
}
