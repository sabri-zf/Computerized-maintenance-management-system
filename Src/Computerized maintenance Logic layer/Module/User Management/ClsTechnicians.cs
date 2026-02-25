using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.UserManagement;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public sealed class ClsTechnicians(TechnicianRepo _Reop)
    {
        /// <summary>
        /// Retrieve one record form dataset asynchronous
        /// </summary>
        /// <param name="ID">unique identifier of technicion</param>
        /// <returns> data transfer object of <see cref="TechnicianDtoRepose"/>, otherwise <see langword="null"/></returns>
        public async Task<TechnicianDtoRepose?> FindAsync(int ID)
        {
            if (ID < 1) return null;

            return await _Reop.FindByIDAsync(ID);
        }

        /// <summary>
        /// Insert new record  to dataset asynchronous
        /// </summary>
        /// <param name="technicianDto">data transer object <see cref="ProcessToCreateTechnicianDto"/></param>
        /// <returns><see langword="true"/> in case operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> AddNewTehnicianAsync(ProcessToCreateTechnicianDto technicianDto)
        {
            if (technicianDto == null) return false;

            return await _Reop.AddNewTechnicianAsync(technicianDto);
        }

        /// <summary>
        /// Modfy a record from dataset asynchronous
        /// </summary>
        /// <param name="technicianDto">data transer object <see cref="ProcessToModifyTechnicianDto"/></param>
        /// <returns><see langword="true"/> in case operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateTehnicianAsync(ProcessToModifyTechnicianDto technicianDto)
        {
            if (technicianDto == null) return false;


            return await _Reop.UpdateTechnicianAsync(technicianDto);
        }

        /// <summary>
        /// Delete a record from dataset asynchronous
        /// </summary>
        /// <param name="ID">Unique Identifier <see cref="ClsTechnicians"/></param>
        /// <returns><see langword="true"/> in case operation has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> DeleteTechincianAsync(int ID)
        {
            if (ID < 1) return false;

            return await _Reop.DeleteTechnicianAsync(ID);
        }

        /// <summary>
        /// Retrieve whole records from dataset asynchronous
        /// </summary>
        /// <returns> A collaction of <see cref="TechnicianTableViewDto"/>, otherwise <see langword="null"/> </returns>
        public async Task<IEnumerable<TechnicianTableViewDto>?> RetrieveAllTechincinAsync()
        {
            return await _Reop.RetrieveAllTechnicionAsync();
        }
    }
}
