using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TachraFac.Core.DTOs;
using TachraFac.Core.Services;
using TachraFac.Core.Services.Interfaces;

namespace TachraFac.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }

        public IActionResult profile()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }
        public IActionResult test()
        {
            return View();
        }

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var contact = _userService.GetUserContactByUserId(user.UserId);
           
            return View();
        }

        [HttpPost]
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile(InformationUserViewModel information)
        {
            if (!ModelState.IsValid)
            {
                return View(information);
            }
            var user = _userService.GetUserByUserName(User.Identity.Name);
            UserSharedDtoViewModel userEdit = new UserSharedDtoViewModel()
            {
                Email = information.Email,
            };
            UserContactSharedDtoViewModel contactEdit = new UserContactSharedDtoViewModel()
            {
                Address = information.Address,
                PostalCode = information.PostalCode,
                PhoneNumber = information.PhoneNumber,
            };
            return RedirectToAction("Index", "UserPanel");
        }

        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            string curentUsername = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View(change);
            }

            if(!_userService.CompareOldPassword(change.OldPassword,curentUsername))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمی باشد");
                return View(change);
            }
            _userService.ChangeUserPassword(curentUsername, change.Password);
            ViewBag.IsSuccess = true;
            return View();
        }

    



        //public IActionResult Index()
        //{
        //    return View(_userService.GetUserByUserName(User.Identity.Name));
        //}
    }

}
