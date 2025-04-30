using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public static class RoleManipulateDataExtension
    {

        public static ClsRoles? Find(this ClsRoles role,int? ID)
        {
            RoleTableDto roleDto = new RoleTableDto();
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

        public static List<RoleTableDto> GetAllRoles(this ClsRoles role)
        {
            return DataAccessRole.GetAllRoles();
        }

    }
}
