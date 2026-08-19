using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.Security;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Entities.User;

namespace TachraFac.Web.Pages.Admin.Roles
{
    [PermissionChecker(8)]
    public class EditRoleModel : PageModel
    {
        IPermisisonService _permisisonService;
        public EditRoleModel(IPermisisonService permisisonService)
        {
            _permisisonService = permisisonService;
        }
        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int id)
        {
            Role = _permisisonService.GetRoleById(id);
            ViewData["Permission"] = _permisisonService.GetAllPermission();
            ViewData["SelectedPermission"] = _permisisonService.PermissionsRole(id);
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _permisisonService.UpdateRole(Role);

            //TODO Add Permision
            _permisisonService.UpdatePermissionRoles(Role.RoleId,SelectedPermission);
            return RedirectToPage("Index");
        }
    }
}
