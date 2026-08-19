using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Core.Services.Interfaces;

namespace TachraFac.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        IPermisisonService _permisisonService;
        private int _permissionId = 0;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;   
        }
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permisisonService = (IPermisisonService)context.HttpContext.RequestServices.GetService(typeof(IPermisisonService));
            if (context.HttpContext.User.Identity.IsAuthenticated) 
            {
                string userName = context.HttpContext.User.Identity.Name;
                if (!_permisisonService.CheckPermission(_permissionId,userName))
                {
                    context.Result = new RedirectResult("/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
