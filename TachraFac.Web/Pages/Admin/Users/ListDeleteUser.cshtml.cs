using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.DTOs;
using TachraFac.Core.Services.Interfaces;

namespace TachraFac.Web.Pages.Admin.Users
{
    public class ListDeleteUserModel : PageModel
    {
        private IUserService _userService;
        public ListDeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }
        public UsersForAdminViewModel UsersForAdminViewModel { get; set; }
        public void OnGet(int pageId = 1, string filterUsername = "", string filterEmail = "")
        {
            UsersForAdminViewModel = _userService.GetDeleteUsers(pageId, filterEmail, filterUsername);

        }
    }
}
