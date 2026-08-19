using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Datalayer.Entities.Product
{
    public class RawMaterial
    {
        public RawMaterial()
        {
            ProductMaterials = new HashSet<ProductMaterial>();
        }

        [Key]
        public int MaterialId { get; set; }

        [Display(Name = "مواد اولیه")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(15,ErrorMessage ="{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string MaterialTitle { get; set; }

        #region Realation
        public ICollection<ProductMaterial> ProductMaterials { get; set; }
        #endregion
    }
}
