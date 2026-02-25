using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.UserManagement;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsRoles(RoleRepo _repostory)
    {
        /// <summary>
        /// Looking for Role Name  from dataset aysnchronously
        /// </summary>
        /// <param name="Id">unique identifier of Role</param>
        /// <returns>data transfer object  <see cref="RoleDtoResponse"/>, otherwise <see langword="null"/></returns>
        public async Task<RoleDtoResponse?> FindByID(int Id)
        {
            if (Id < 1) return null;

            return await _repostory.FindByIdAsync(Id);
        }

        /// <summary>
        /// Retrieve whole Role data form dataset aysnchronously
        /// </summary>
        /// <returns>A List of <see cref="IEnumerable{T}"/> has <see cref="RoleDtoResponse"/>, otherwise <see langword="null"/></returns>
        public async Task<IEnumerable<RoleDtoResponse>?> RetriveAllRolesAsync()
        {
            return await _repostory.RetrieveWholeRolesAsync();
        }


        /// <summary>
        /// Insert new role record on dataset aysnchronously
        /// </summary>
        /// <param name="Rolename">The role name type it from user</param>
        /// <returns><see langword="true"/> if the operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> AddnewRoleAsync(string Rolename)
        {
            return await _repostory.AddNewRoleAsync(Rolename);
        }


        /// <summary>
        /// Update role record on dataset aysnchronously
        /// </summary>
        /// <param name="OldRoleName">old value of role name already exist on system </param>
        /// <param name="NewRolename">new value of role name made by user</param>
        /// <returns><see langword="true"/> if the operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateRoleAsync(string OldRoleName, string NewRolename)
        {
            if (NewRolename.Equals(OldRoleName, StringComparison.OrdinalIgnoreCase)) return false;

            return await _repostory.UpdateRoleAsync(OldRoleName, NewRolename);
        }


        /// <summary>
        /// Omitting Role record on dataset asynchronously
        /// </summary>
        /// <param name="rolename">value of role name provided from user</param>
        /// <returns><see langword="true"/> if the operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> DeleteRoleAsync(string rolename)
        {
            return await _repostory.DeleteRoleAsync(rolename);
        }


    }
}
