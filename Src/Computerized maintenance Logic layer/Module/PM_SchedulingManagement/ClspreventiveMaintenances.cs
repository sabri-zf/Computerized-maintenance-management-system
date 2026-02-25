using Computerized_maintenance_Logic_layer.Module.DTO.PreventiveMaintenanceManagement;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.PM_SchedulingManagement;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.preventiveMaintenanceManagement
{
    /// <summary>
    /// Provides methods for managing preventive maintenance operations within the application.
    /// </summary>
    /// <remarks>This class is designed to interact with the application's database context to perform various
    /// operations related to preventive maintenance. It is sealed to prevent inheritance and ensure consistent behavior
    /// across the application.
    /// </remarks>
    /// <param name="_Context"> a Instance of AppDbContext Class EF core</param>
    public sealed class ClspreventiveMaintenances(AppDbContext _Context)
    {

        /// <summary>
        /// Retrieve list of whole data for <see cref="PreventiveMaintenance"/>
        /// </summary>
        /// <returns><see cref="IEnumerable{PreventiveMaintenanceResponseDto}"/>, otherwise <see langword="null"/></returns>
        /// <exception cref="Exception">Throw exception if error occorring from Ef core</exception>
        public async Task<IEnumerable<PreventiveMaintenanceResponseDto>?> RetrieveWholeData()
        {
            try
            {
                var List = await _Context.PreventiveMaintenances
                                     .AsNoTracking()
                                        .Select(x => new PreventiveMaintenanceResponseDto
                                        (
                                            x.AssetID,
                                            x.TaskDescription,
                                            x.Frequency.ToString(),
                                            x.CreatedDate

                                        )).ToListAsync();

                if (List.Count <= 0) return null;

                return List.AsEnumerable();

            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Find a single record of <see cref="PreventiveMaintenance"/> by unique identitfier
        /// </summary>
        /// <param name="Id">unique identitfier of <see cref="PreventiveMaintenance"/></param>
        /// <returns>Data transfer object of <see cref="PreventiveMaintenanceResponseDto"/>,otherwise <see langword="null"/> </returns>
        /// <exception cref="Exception">Throw exception if error occorring from Ef core</exception>
        public async Task<PreventiveMaintenanceResponseDto?> FindByIdAsync(int Id)
        {
            try
            {
                if (Id < 1) return null;

                var entity = await _Context.PreventiveMaintenances
                                           .AsNoTracking()
                                           .Where(x => x.ID == Id)
                                           .Select(x => new PreventiveMaintenanceResponseDto
                                           (
                                               x.AssetID,
                                               x.TaskDescription,
                                               x.Frequency.ToString(),
                                               x.CreatedDate
                                           ))
                                           .SingleOrDefaultAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add new record of <see cref="PreventiveMaintenance"/> into data store
        /// </summary>
        /// <param name="responseDto">Data transfer object of <see cref="PreventiveMaintenanceRequesteDto"/></param>
        /// <returns><see langword="true"/> if add Preventive Maintenance has been done, otherwise <see langword="false"/> </returns>
        /// <exception cref="Exception">Throw exception if error occorring from Ef core</exception>
        public async Task<int> AddPreventiveMaintenanceAsync(PreventiveMaintenanceResponseDto responseDto)
        {
            try
            {
                if (_IsInputDataValidate(responseDto)) return -1;


                var entity = new PreventiveMaintenance
                {
                    AssetID = responseDto.AssetID,
                    TaskDescription = responseDto.TaskDescription,
                    Frequency = (En_FrequencyTask)Enum.Parse(typeof(En_FrequencyTask), responseDto.Frequency),
                    CreatedDate = responseDto.ScheduledDate,
                };

                await _Context.PreventiveMaintenances.AddAsync(entity);


                await _Context.SaveChangesAsync();

                return entity.AssetID;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update existing record of <see cref="PreventiveMaintenance"/> in the data store
        /// </summary>
        /// <param name="requestDto">Data transfer object of <see cref="PreventiveMaintenanceRequesteDto"/></param>
        /// <returns><see langword="true"/> if update Preventive Maintenance has been done, otherwise <see langword="false"/> </returns>
        /// <exception cref="Exception">Throw exception if error occorring from Ef core</exception>
        public async Task<bool> UpdatePreventiveMaintenanceAsync(PreventiveMaintenanceRequesteDto requestDto)
        {
            try
            {
                var ResponseDto = new PreventiveMaintenanceResponseDto
                (
                    requestDto.AssetID,
                    requestDto.TaskDescription,
                    requestDto.Frequency,
                    requestDto.ScheduledDate
                );

                if (_IsInputDataValidate(ResponseDto, true, requestDto.ID)) return false;

                return await _Context.PreventiveMaintenances
                                     .Where(x => x.ID == requestDto.ID)
                                     .ExecuteUpdateAsync(u => u
                                        .SetProperty(p => p.AssetID, requestDto.AssetID)
                                        .SetProperty(p => p.TaskDescription, requestDto.TaskDescription)
                                        .SetProperty(p => p.Frequency, (En_FrequencyTask)Enum.Parse(typeof(En_FrequencyTask), requestDto.Frequency))
                                        .SetProperty(p => p.CreatedDate, requestDto.ScheduledDate)
                                     ) > 0;

            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete existing record of <see cref="PreventiveMaintenance"/> from data store
        /// </summary>
        /// <param name="Id">unique identitfier of <see cref="PreventiveMaintenance"/></param>
        /// <returns><see langword="true"/> if delete Preventive Maintenance has been done, otherwise <see langword="false"/> </returns>
        /// <exception cref="Exception">Throw exception if error occorring from Ef core</exception>
        public async Task<bool> DeletePreventiveMaintenanceAsync(int Id)
        {
            try
            {
                if (Id < 1) return false;

                return await _Context.PreventiveMaintenances
                                     .Where(x => x.ID == Id)
                                     .ExecuteDeleteAsync() > 0;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Check input data is valid or not
        /// </summary>
        /// <param name="responseDto">Data transfer object of <see cref="PreventiveMaintenanceRequesteDto"/></param>
        /// <param name="IsRequest"> Check If It onRequest Mode or not </param>
        /// <param name="Id">unique identitfier of <see cref="PreventiveMaintenance"/></param>
        /// <returns><see langword="true"/> if data inputs are valids, otherwise <see langword="false"/> </returns>
        private bool _IsInputDataValidate(PreventiveMaintenanceResponseDto responseDto, bool IsRequest = false, int Id = 0)
        {
            if (IsRequest)
            {
                if (Id < 1) return false;
            }
            if (string.IsNullOrWhiteSpace(responseDto.TaskDescription)) return false;
            if (responseDto.AssetID < 1) return false;
            return true;
        }
    }
}
