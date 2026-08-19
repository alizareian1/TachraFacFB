using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.Security;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Entities.User;

namespace TachraFac.Web.Pages.Admin.Roles
{
    [PermissionChecker(7)]
    public class CreateRoleModel : PageModel
    {
        IPermisisonService _permisisonService;
        public CreateRoleModel(IPermisisonService permisisonService)
        {
            _permisisonService = permisisonService;   
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet()
        {
            ViewData["Permission"] = _permisisonService.GetAllPermission();
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Permission"] = _permisisonService.GetAllPermission();
                return Page();
            }

            
            Role.IsDelete = false;
            int roleId = _permisisonService.AddRole(Role);

            //TODO Add Permission
            _permisisonService.AddPermissionToRole(roleId,SelectedPermission);
            return RedirectToPage("Index");
        }
    }
}
