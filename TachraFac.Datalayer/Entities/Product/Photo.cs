using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Datalayer.Entities.Product
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(255,ErrorMessage ="{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string PhotoProduct { get; set; }

        public int ProductId { get; set; }  // مستقیم اینجا

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
