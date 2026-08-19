using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.DTOs;
using TachraFac.Core.Security;
using TachraFac.Core.Services.Interfaces;

namespace TachraFac.Web.Pages.Admin.Users
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        public UsersForAdminViewModel UsersForAdminViewModel { get; set; }
        public void OnGet(int pageId = 1,string filterUsername="", string filterEmail="")
        {
            UsersForAdminViewModel = _userService.GetUsers(pageId, filterEmail,filterUsername);
        }

        
    }
}
