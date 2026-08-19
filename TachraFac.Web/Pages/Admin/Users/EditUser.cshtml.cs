using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.DTOs;
using TachraFac.Core.Security;
using TachraFac.Core.Services.Interfaces;

namespace TachraFac.Web.Pages.Admin.Users
{
    [PermissionChecker(4)]
    public class EditUserModel : PageModel
    {
        private IUserService _userService;        
        IPermisisonService _permisisonService;
        public EditUserModel(IUserService userService, IPermisisonService permisisonService)
        {
            _userService = userService;
            _permisisonService= permisisonService;
        }

        [BindProperty]
        public EditUserViewModel editUserViewModel { get; set; }
        public void OnGet(int id)
        {
            editUserViewModel = _userService.GetUserForShowInEditMode(id);
            ViewData["Roles"] = _permisisonService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _userService.EditUserFormAdmin(editUserViewModel);
            //Edit Role
            _permisisonService.EditRolesUser(editUserViewModel.userId, SelectedRoles);
            return RedirectToPage("Index");
        }
    }
}
