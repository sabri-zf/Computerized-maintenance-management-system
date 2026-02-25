using Computerized_maintenance_Logic_layer.Module.DTO.PM_SchedulingManagement;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.PM_SchedulingManagement;
using Microsoft.EntityFrameworkCore;
namespace Computerized_maintenance_Logic_layer.Module.PM_SchedulingManagement
{
    public class ClsPMSchedule(AppDbContext _Context)
    {

        /// <summary>
        /// Retrieves all PM schedules from the database and maps them to a list of PMScheduleResponseDto objects.
        /// </summary>
        /// <returns> 
        ///  IEnumerable <see cref="PMScheduleResponseDto"/>, or null if no schedules are found or an exception occurs.
        /// </returns>
        public async Task<IEnumerable<PMScheduleResponseDto>?> RetrieveAllData()
        {
            try
            {
                var data = await _Context.Schedules
                                          .AsNoTracking()
                                          .Select(x => new PMScheduleResponseDto
                                          (
                                              x.PmID,
                                              x.NextDueDate,
                                              x.LastCompletionDate,
                                              x.IsActive,
                                              x.CreatedDate
                                          )).ToListAsync();

                if (data.Count <= 0) return null;


                return data;

            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                //throw new Exception(ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Find out a specific PM schedule by its ID from the database and maps it to a PMScheduleResponseDto object.
        /// </summary>
        /// <param name="Id"> unique identifier of schedule entity</param>
        /// <returns>
        /// <see cref="PMScheduleResponseDto"/> object if found, or null if not found or an exception occurs.
        /// </returns>
        public async Task<PMScheduleResponseDto?> FindByIdAsync(int Id)
        {

            if (Id < 1) return null;

            try
            {
                var data = await _Context.Schedules
                                          .AsNoTracking()
                                          .Where(x => x.ScheduleID == Id)
                                          .Select(x => new PMScheduleResponseDto
                                          (
                                              x.PmID,
                                              x.NextDueDate,
                                              x.LastCompletionDate,
                                              x.IsActive,
                                              x.CreatedDate
                                          )).SingleOrDefaultAsync();

                if (data == null) return null;

                return data;
            }
            catch
            (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                //throw new Exception(ex.Message);
                return null;

            }
        }


        public async Task<DateTime?> FindByIdLastCompletionDateAsync(int Id)
        {
            if (Id < 1) return null;

            try
            {
                var LastCompletionDate = await _Context.Schedules
                                                       .AsNoTracking()
                                                       .Where (x => x.ScheduleID == Id)
                                                       .Select(x => x.LastCompletionDate)
                                                       .SingleOrDefaultAsync();

                return LastCompletionDate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<DateTime?> FindById_Old_NextDueDateAsync(int Id)
        {
            if (Id < 1) return null;

            try
            {
                var OldDate = await _Context.Schedules
                                            .AsNoTracking()
                                            .Where(x => x.ScheduleID == Id)
                                            .Select(x => x.NextDueDate)
                                            .SingleOrDefaultAsync();

                return OldDate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves PM schedules from the database that match the specified PM ID and maps them to a list of PMScheduleResponseDto objects.
        /// </summary>
        /// <param name="PmId">unique identifier of perventive maintenace entity</param>
        /// <returns>
        /// IEnumerable <see cref="PMScheduleResponseDto"/> containing the PM schedules that match the specified PM ID, 
        /// or <see langword="null"/> if no schedules are found or an exception occurs.   
        /// </returns>
        public async Task<IEnumerable<PMScheduleResponseDto>?> FindByPmIdAsync(int PmId)
        {

            if (PmId < 1) return null;

            try
            {
                var data = await _Context.Schedules
                                          .AsNoTracking()
                                          .Where(x => x.PmID == PmId)
                                          .Select(x => new PMScheduleResponseDto
                                          (
                                              PmId,
                                              x.NextDueDate,
                                              x.LastCompletionDate,
                                              x.IsActive,
                                              x.CreatedDate
                                          )).ToListAsync();

                if (data.Count <= 0) return null;
                return data;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                //throw new Exception(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Updates the LastCompletionDate of a PM schedule in the database based on the provided ScheduleId and CompletionDate.
        /// </summary>
        /// <param name="ScheduleId">unique identifier of schedule entity</param>
        /// <param name="CompletionDate">Date of Completion schedule</param>
        /// <returns>
        /// <see langword="bool" "/> indicating whether the update was successful  <see langword="true" "/> otherwise  <see langword="false" "/>
        /// </returns>
        public async Task<bool> UpdateLastCompletionDateAsync(int ScheduleId, DateTime? CompletionDate)
        {

            if (ScheduleId < 1 && CompletionDate <= DateTime.UtcNow) return false;
            try
            {
                var IsUpdated = await _Context.Schedules
                                         .Where(x => x.ScheduleID == ScheduleId)
                                         .ExecuteUpdateAsync(x => x.SetProperty(s => s.LastCompletionDate, CompletionDate));
                return IsUpdated > 0;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                //throw new Exception(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// updates the NextDueDate of a PM schedule in the database based on the provided ScheduleId and NextDueDate.
        /// </summary>
        /// <param name="ScheduleId">unique identifier of schedule entity</param>
        /// <param name="NextDueDate">Date of Next Preventive maintenace Schedule</param>
        /// <returns>
        /// <see langword="bool" "/> indicating whether the update was successful  <see langword="true" "/> otherwise  <see langword="false" "/>
        /// </returns>
        public async Task<bool> UpdateNextDueDateAsync(int ScheduleId, DateTime NextDueDate)
        {
            if (ScheduleId < 1 && NextDueDate <= DateTime.UtcNow) return false;

            try
            {
                var IsUpdated = await _Context.Schedules
                                         .Where(x => x.ScheduleID == ScheduleId)
                                         .ExecuteUpdateAsync(x => 
                                         x.SetProperty(s => s.NextDueDate, NextDueDate)
                                         );
                return IsUpdated > 0;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                //throw new Exception(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Updates the Activeness status of a PM schedule in the database based on the provided ScheduleId and IsActive value.
        /// </summary>
        /// <param name="ScheduleId">unique identifier of schedule entity</param>
        /// <param name="IsActive"></param>
        /// <returns>
        /// <see langword="bool" "/> indicating whether the update was successful  <see langword="true" "/> otherwise  <see langword="false" "/>
        /// </returns>
        public async Task<bool> UpdateIsActiveAsync(int ScheduleId, bool IsActive)
        {
            if (ScheduleId < 1) return false;

            try
            {
                var IsUpdated = await _Context.Schedules
                                         .Where(x => x.ScheduleID == ScheduleId)
                                         .ExecuteUpdateAsync(x => x.SetProperty(s => s.IsActive, IsActive));
                return IsUpdated > 0;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                //throw new Exception(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Adds a new PM schedule to the database based on the provided PMScheduleRequestDto object.
        /// </summary>
        /// <param name="requestDto">Data transfer object <see cref="PMScheduleResponseDto"/></param>
        /// <returns>
        /// <see langword="bool" "/> indicating whether the addition was successful  <see langword="true" "/> otherwise  <see langword="false" "/>
        /// </returns>
        public async Task<bool> AddPMScheduleAsync(PMScheduleResponseDto requestDto)
        {

            if (requestDto is not PMScheduleResponseDto) return false;

            if (requestDto.PmID <= 0 && requestDto.LastCompletionDate <= DateTime.UtcNow
                && requestDto.NextDueDate <= DateTime.UtcNow && requestDto.CreatedDate == default)
            {
                return false;
            }

            try
            {
                var entity = new PMSchedule
                {
                    PmID = requestDto.PmID,
                    NextDueDate = requestDto.NextDueDate,
                    LastCompletionDate = requestDto.LastCompletionDate,
                    IsActive = requestDto.IsActive,
                    CreatedDate = requestDto.CreatedDate
                };


                await _Context.Schedules.AddAsync(entity);
                var IsAdded = await _Context.SaveChangesAsync();

                return IsAdded > 0;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                //throw new Exception(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes a PM schedule from the database by setting its IsDeleted property to true based on the provided ScheduleId.
        /// </summary>
        /// <param name="ScheduleId">unique identifier of schedule entity</param>
        /// <returns></returns>
        public async Task<bool> UpdateScheduleFlagAsDeleted(int ScheduleId)
        {
            if (ScheduleId < 1) return false;

            try
            {
                var IsUpdated = await _Context.Schedules
                                         .Where(x => x.ScheduleID == ScheduleId)
                                         .ExecuteUpdateAsync(x => x.SetProperty(s => s.IsDeleted, true));
                return IsUpdated > 0;
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your logging mechanism)
                //throw new Exception(ex.Message);
                return false;
            }
        }
    }
};
