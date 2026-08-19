using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Datalayer.Entities.Permission;
using TachraFac.Datalayer.Entities.User;

namespace TachraFac.Core.Services.Interfaces
{
    public interface IPermisisonService
    {
        #region Role
        List<Role> GetRoles();
        int AddRole(Role role);
        Role GetRoleById(int roleId);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void AddRolesToUser(List<int> roleIds,int userId);
        void EditRolesUser(int userId,List<int> rolesId);
        #endregion

        #region Permission
        List<Permission> GetAllPermission();
        void AddPermissionToRole(int roleId, List<int> permission);
        List<int> PermissionsRole(int roleId);
        void UpdatePermissionRoles(int roleId, List<int> permission);
        bool CheckPermission(int permissionId, string userName);
        #endregion
    }
}
