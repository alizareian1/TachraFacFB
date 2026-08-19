using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TachraFac.Core.DTOs;
using TachraFac.Core.Services.Interfaces;

namespace TachraFac.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IUserService _userService;
        public WalletController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
            return View();
        }


        [Route("UserPanel/Wallet")]
        [HttpPost]
        public IActionResult Index(CharegeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
                return View(charge);
            }
            ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
            int walletId = _userService.ChargeWallet(User.Identity.Name, charge.Amount, "شارژ حساب");

            #region Online Payment
            //var payment = new ZarinPalSandbox.Payment(charge.Amount); 
            //var Res = PaymentRequest("Charge", "LocalHost/OnlinePayment/" + walletId);
            //if(Res.Result.Status == 100)
            //{
            //    return Redirct("https://sandbox.zarinpal.com/pg/startPay/" + Res.Result.Authority");
            //}
            #endregion
            return View();
        }
    }
}
