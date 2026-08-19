using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Datalayer.Entities.User;

namespace TachraFac.Datalayer.Entities.Product
{
    public class ProductLike
    {
        [Key]
        public int ProductId { get; set; }
        
        
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User.User User { get; set; }
    }
}
