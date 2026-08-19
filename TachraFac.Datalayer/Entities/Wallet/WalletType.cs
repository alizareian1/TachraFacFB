using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Datalayer.Entities.Wallet
{
    public class WalletType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(12)]
        public string TypeTitle { get; set; }

        public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
