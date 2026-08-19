using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TachraFac.Datalayer.Entities.Permission;
using TachraFac.Datalayer.Entities.User;


namespace TachraFac.Datalayer.Entities.User
{
    public class Role
    {
        public Role()
        {

            userRoles = new List<UserRole>();
            RolePermissions = new List<Permission.RolePermission>();
        }

        [Key]
        public int RoleId { get; set; }


        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(70,ErrorMessage ="{0} نمی‌تواند بیشتر از {1} کارکتر باشد")]
        public string RoleTitle { get; set; }


        public bool IsDelete { get; set; }

        #region Relation
        public virtual List<UserRole> userRoles { get; set; }
        public  List<Permission.RolePermission> RolePermissions { get; set; }
        

        #endregion
    }
}
