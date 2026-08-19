using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Datalayer.Entities.Product
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "نام محصصول")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(50,ErrorMessage ="{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string ProductTitle { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string Discription { get; set; }


        [Display(Name = "تاریخ انقضا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string ExpirationDate { get; set; }

        [Display(Name = "وزن")]
        public short Weight { get; set; }

        [Display(Name = "قیمت")]
        public int price { get; set; }

        public int LikeCount { get; set; } = 0;



        #region Realation
        public ICollection<ProductMaterial> ProductMaterials { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<ProductLike> ProductLikes { get; set; }
        #endregion
    }
}
