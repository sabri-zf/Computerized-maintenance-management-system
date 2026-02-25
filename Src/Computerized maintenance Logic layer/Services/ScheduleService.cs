using Computerized_maintenance_Logic_layer.Module.DTO.PM_SchedulingManagement;
using Computerized_maintenance_Logic_layer.Module.DTO.PreventiveMaintenanceManagement;
using Computerized_maintenance_Logic_layer.Module.PM_SchedulingManagement;
using Computerized_maintenance_Logic_layer.Module.preventiveMaintenanceManagement;
using computrized_maintenance_Data_Access.Enumes;

namespace Computerized_maintenance_Logic_layer.Services
{
    public sealed class ScheduleService
    {

        private readonly ClspreventiveMaintenances _PriventiveRepo;
        private readonly ClsPMSchedule _ScheduleRepo;
        private readonly PreventiveMaintenaceService _PreventiveService;

        public ScheduleService(ClspreventiveMaintenances priventiveRepo, ClsPMSchedule scheduleRepo, PreventiveMaintenaceService preventiveService)
        {
            _PriventiveRepo = priventiveRepo;
            _ScheduleRepo = scheduleRepo;
            _PreventiveService = preventiveService;
        }


        /// <summary>
        /// Make new Next schedule task depanced on the frequency sets to machine
        /// </summary>
        /// <param name="FrequencyTask">Represent the days of set task</param>
        /// <returns>
        ///  <see cref="DateTime"/> of New next schedule 
        /// </returns>
        private DateTime MakeNewNextDueDate(string FrequencyTask)
        {
            var FequencyDays = _PreventiveService.ConvertFequencyScheduleFromStringToDigit((En_FrequencyTask)Enum.Parse(typeof(En_FrequencyTask), FrequencyTask));
            var NextDueDate = DateTime.Now.AddDays(FequencyDays);
            return NextDueDate;
        }

         /// <summary>
         /// Assgined Schedule to Preventive maintenace, Register it at dataset
         /// </summary>
         /// <param name="PM_response">date transfer object <see cref="PreventiveMaintenanceResponseDto"/></param>
         /// <returns>
         /// <see langword="true"/> in case that operation has been done, otherwise <see langword="false"/>
         /// </returns>
        public async Task<bool> Assgin_Schedule_to_PreventiveMaintence(PreventiveMaintenanceResponseDto PM_response)
        {

            var makePM_ID = await _PriventiveRepo.AddPreventiveMaintenanceAsync(PM_response);

            if (makePM_ID == -1) return false;

            var NextDueDate = MakeNewNextDueDate(PM_response.Frequency);

            var ScheduleResponse = new PMScheduleResponseDto
                (
                makePM_ID,
                NextDueDate,
                null,
                true,
                DateTime.Now
                );

            var makePM_Schedule = await _ScheduleRepo.AddPMScheduleAsync(ScheduleResponse);

            return makePM_Schedule;
        }
   
    
        /// <summary>
        /// Reassgin Schedule to Preventive maintance
        /// </summary>
        /// <param name="input">Data transfer object <see cref="ReassginScheduleDto"/></param>
        /// <returns>
        /// <see langword="true"/> on case operation was successfully, otherwise <see langword="false"/>
        /// </returns>
        public async Task<bool> Reassgin_Schedule_to_PreventiveMaintence(ReassginScheduleDto input)
        {
            if(input is not ReassginScheduleDto) return false;
            if(input.SecheduleId < 1 || string.IsNullOrEmpty(input.FrequencyTask)) return false; 

            var OldDate = await _ScheduleRepo.FindById_Old_NextDueDateAsync(input.SecheduleId);

            if(OldDate == null) return false;

            var NextDueDate = MakeNewNextDueDate(input.FrequencyTask);

             if( await _ScheduleRepo.UpdateNextDueDateAsync(input.SecheduleId, NextDueDate))
            {
                return await _ScheduleRepo.UpdateLastCompletionDateAsync(input.SecheduleId, OldDate);
            }

             return false;
        }


    }
}
