using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.DTOs;
using TachraFac.Core.Security;
using TachraFac.Core.Services.Interfaces;

namespace TachraFac.Web.Pages.Admin.Users
{
    [PermissionChecker(3)]
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermisisonService _permisisonService;
        public CreateUserModel(IUserService userService, IPermisisonService permisisonService)
        {
            _userService = userService;
            _permisisonService = permisisonService;
        }

        [BindProperty]
        public CreateUserViewModel createUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permisisonService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int userId = _userService.AddUserFromAdmin(createUserViewModel);

            //Add Role
            _permisisonService.AddRolesToUser(SelectedRoles, userId);
            return Redirect("/Admin/Users");
        }
    }
}
