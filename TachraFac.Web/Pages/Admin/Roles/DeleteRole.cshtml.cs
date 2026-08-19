using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.Security;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Entities.User;

namespace TachraFac.Web.Pages.Admin.Roles
{
    [PermissionChecker(9)]
    public class DeleteRoleModel : PageModel
    {
        IPermisisonService _permisisonService;
        public DeleteRoleModel(IPermisisonService permisisonService)
        {
            _permisisonService = permisisonService;
        }
        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int id)
        {
            Role = _permisisonService.GetRoleById(id);
        }

        public IActionResult OnPost()
        {
            _permisisonService.DeleteRole(Role);
            return RedirectToPage("Index");
        }
        
    }
}
