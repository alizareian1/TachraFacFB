using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.DTOs;
using TachraFac.Core.Security;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Entities.User;

namespace TachraFac.Web.Pages.Admin.Roles
{
    [PermissionChecker(6)]
    public class IndexModel : PageModel
    {
        private IPermisisonService _permisisonService;
        public IndexModel(IPermisisonService permisisonService)
        {
            _permisisonService = permisisonService;
        }

        public  List<Role> RolesList { get; set; }
        public void OnGet()
        {
            RolesList = _permisisonService.GetRoles();
        }

        
    }
}
