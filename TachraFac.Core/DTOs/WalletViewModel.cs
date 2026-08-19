using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachraFac.Core.DTOs
{
    public class CharegeWalletViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }

    public class WalletViewModel
    {
        public int WalletId { get; set; }
        public int Amount { get; set; }
        public int Type { get; set; }
        public string Discription { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsPay { get; set; }
    }
}
