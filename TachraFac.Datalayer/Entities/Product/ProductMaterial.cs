using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Datalayer.Entities.Product
{
    public class ProductMaterial
    {
        [Key]
        public int PM_Id { get; set; }
        public int ProductId { get; set; }
        public int MaterialId { get; set; }

        #region Realation
        [ForeignKey("ProductId")]
        public Product product { get; set; }

        [ForeignKey("MaterialId")]
        public RawMaterial rawMaterial { get; set; }
        #endregion
    }
}
