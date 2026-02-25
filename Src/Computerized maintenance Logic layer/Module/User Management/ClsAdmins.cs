using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.UserManagement;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsAdmins(AdminRepo _Repo)
    {

        /// <summary>
        /// Retrieve Admin <see cref="AdminDtoResponse"/> by ID"/>
        /// </summary>
        /// <param name="ID">unique identifier of admin</param>
        /// <returns>data transfer object of <see cref="AdminDtoResponse"/> in case an operation has been done, otherwise <see langword="null"/></returns>
        public async Task<AdminDtoResponse?> FindAsync(int ID)
        {
            return await _Repo.FinsByIdAsync(ID);
        }


        /// <summary>
        /// Insert new admin record on the system
        /// </summary>
        /// <param name="adminRequest">data transfer object of <see cref="ProcessAddUserDto"/></param>
        /// <returns><see langword="true"/> if an operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> AddNewAsync(ProcessAddUserDto adminRequest)
        {
            if (adminRequest is not ProcessAddUserDto) return false;

            return await _Repo.AddNewAdminAsync(adminRequest) > 0;
        }

        /// <summary>
        /// Update an admin record on the system
        /// </summary>
        /// <param name="adminRequest">data transfer object of <see cref="AdminDtoRequest"/></param>
        /// <returns><see langword="true"/> if an operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateAsync(ProcessUpdateAdminDto adminRequest)
        {
            if (adminRequest is not ProcessUpdateAdminDto) return false;
            return await _Repo.UpdateAdminAsync(adminRequest);
        }

        /// <summary>
        /// Delete an admin record on the system
        /// </summary>
        /// <param name="Id">Unique identifier of admin </param>
        /// <returns><see langword="true"/> if an operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> DeleteAsync(int Id)
        {
            if (Id < 1) return false;
            return await _Repo.DeleteAdminAsync(Id);
        }

        /// <summary>
        /// Verify an admin existence on the system
        /// </summary>
        /// <param name="Id">Unique identifier of admin </param>
        /// <returns><see langword="true"/> if an operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> IsExistAsync(int Id)
        {
            if (Id < 1) return false;
            return await _Repo.IsExistAdminAsync(Id);
        }


        /// <summary>
        /// retrieve all admins on the system
        /// </summary>
        /// <returns>a collection of Admins list <see cref="IEnumerable{AdminTableViewDto}"/>, otherwise <see langword="null"/></returns>
        public async Task<IEnumerable<AdminTableViewDto>?> GetAllAsync()
        {
            return await _Repo.GetAllAdminsAsync();
        }

    }
}
