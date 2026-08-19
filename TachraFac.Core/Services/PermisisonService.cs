using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Context;
using TachraFac.Datalayer.Entities.Permission;
using TachraFac.Datalayer.Entities.User;

namespace TachraFac.Core.Services
{
    public class PermisisonService : IPermisisonService
    {
        private TachraContext _context;
        public PermisisonService(TachraContext context)
        {
            _context = context;
        }

        public void AddPermissionToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                _context.RolePermission.Add(new RolePermission()
                {
                    PermissionId = p,
                    RoleId = roleId
                });
            }
            _context.SaveChanges();
        }

        public int AddRole(Role role)
        {
            _context.tblRole.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (var roleId in roleIds) 
            {
                _context.tblUserRole.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }
            _context.SaveChanges();

        }

        public bool CheckPermission(int permissionId, string userName)
        {
            int userId = _context.tblUser.Single(u => u.UserName == userName).UserId;
            List<int> UsersRoles = _context.tblUserRole.Where(r => r.UserId == userId).Select(r => r.RoleId).ToList();
            if (!UsersRoles.Any())
            {
                return false;
            }
            List<int> RolePermission = _context.RolePermission.Where(p => p.PermissionId == permissionId)
                
                .Select(p=>p.RoleId).ToList();
            return RolePermission.Any(p => UsersRoles.Contains(p));
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }

        public void EditRolesUser(int userId, List<int> rolesId)
        {
            // Delete All Role User
            _context.tblUserRole.Where(r => r.UserId == userId).ToList().ForEach(r => _context.tblUserRole.Remove(r));

            // Add new Roles
            AddRolesToUser(rolesId, userId);
        }

        public List<Permission> GetAllPermission()
        {
            return _context.Permission.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.tblRole.Find(roleId);
        }

        public List<Role> GetRoles()
        {
            return _context.tblRole.ToList();
        }

        public List<int> PermissionsRole(int roleId)
        {
            return _context.RolePermission.Where(r => r.RoleId == roleId).Select(r=>r.PermissionId).ToList();
        }

        public void UpdatePermissionRoles(int roleId, List<int> permission)
        {
            _context.RolePermission.Where(r => r.RoleId == roleId).ToList().ForEach(p => _context.RolePermission.Remove(p));
            AddPermissionToRole(roleId, permission);
        }

        public void UpdateRole(Role role)
        {
            _context.tblRole.Update(role);
            _context.SaveChanges();
        }
    }
}
