using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public static class ClsRoleManipulateDataExtension
    {

        public static ClsRoles? Find(this ClsRoles role,int? ID)
        {
            RoleDto roleDto = new RoleDto();
            if (DataAccessRole.Find(ID,ref roleDto))
            {
               return new ClsRoles(roleDto);
            }

            return null;
        }

        public static string? GetRoleName(this ClsRoles role)
        {
            return DataAccessRole.RoleName(role.RoleID);
        }

        public static List<RoleDto> GetAllRoles(this ClsRoles role)
        {
            return DataAccessRole.GetAllRoles();
        }

    }
}
