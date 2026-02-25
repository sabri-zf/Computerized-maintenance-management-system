using Computerized_maintenance_Logic_layer.Module.DTO.PM_SchedulingManagement;
using Computerized_maintenance_Logic_layer.Module.DTO.PreventiveMaintenanceManagement;
using Computerized_maintenance_Logic_layer.Module.PM_SchedulingManagement;
using Computerized_maintenance_Logic_layer.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.PM_SchedulingManagement
{


    // this filter is used to specify that this class is validation for API controller and it will automatically handle the model state validation and return appropriate responses
    [ApiController]

    [Route("api/v1/schedules")]
    public class PMScheduleController : ControllerBase
    {

        private readonly ClsPMSchedule _PmService;
        private readonly ScheduleService _service;
  

        public PMScheduleController(ClsPMSchedule pmService, ScheduleService service)
        {
            _PmService = pmService;
            _service = service;
        }

        [HttpGet("retrieve-schedules", Name = "get-all-schedules")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<IActionResult> GetAllSchedules()
        {
            var List = await _PmService.RetrieveAllData();
            if (List is null) return NotFound("Data Not Found");

            return Ok(List);
        }

        [HttpGet("retrieve-schedule/{id:int}", Name = "get-schedule-byid")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            if (id < 1) return BadRequest("Invalid operation");
            var responseDto = await _PmService.FindByIdAsync(id);
            if (responseDto is null) return NotFound("Schedule doesn't find");
            return Ok(responseDto);
        }

        [HttpGet("retrieve-schedules-by-pm/{pmId:int}", Name = "get-schedules-by-pmId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<IActionResult> GetSchedulesByPmId(int pmId)
        {
            if (pmId < 1) return BadRequest("Invalid operation");

            var List = await _PmService.FindByPmIdAsync(pmId);
            if (List is null) return NotFound("Schedules doesn't find for this PM Id");

            return Ok(List);
        }


        [HttpPost("create-schedule", Name = "add-schedule")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddNewSchedule(PMScheduleResponseDto responseDto)
        {
            if (responseDto is not PMScheduleResponseDto) return BadRequest("Invalid operation");

            var IsAdded = await _PmService.AddPMScheduleAsync(responseDto);

            return IsAdded ? Ok("Add New Schedule has been Done")
                           : StatusCode(500, "Error Occurred on System");
        }


        [HttpPut("edit-schedule-next-due-date", Name = "update-schedule-next-due-date")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateScheduleNextDueDate(int scheduleId, DateTime newNextDueDate)
        {
            if (scheduleId < 1 || newNextDueDate <= DateTime.UtcNow) return BadRequest("Invalid operation");

            var IsUpdated = await _PmService.UpdateNextDueDateAsync(scheduleId, newNextDueDate);

            return IsUpdated ? Ok("Schedule Next Due Date has been Updated")
                             : StatusCode(500, "Error Occurred on System");
        }


        [HttpPut("edit-schedule-last-completion-date", Name = "update-schedule-last-completion-date")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateScheduleLastCompletionDate(int scheduleId, DateTime LastCompletionDate)
        {
            if (scheduleId < 1 || LastCompletionDate <= DateTime.UtcNow) return BadRequest("Invalid operation");

            var IsUpdated = await _PmService.UpdateLastCompletionDateAsync(scheduleId, LastCompletionDate);

            return IsUpdated ? Ok("Schedule Last Completion Date has been Updated")
                             : StatusCode(500, "Error Occurred on System");
        }


        [HttpDelete("remove-schedule/{id:int}", Name = "remove-schedule")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RemoveSchedule(int id)
        {
            if (id < 1) return BadRequest("Invalid operation");
            var IsRemoved = await _PmService.UpdateScheduleFlagAsDeleted(id);

            return IsRemoved ? Ok("Schedule has been Removed")
                             : StatusCode(500, "Error Occurred on System");
        }


        [HttpPost("assgin-schedule", Name ="assgin-schedule-prventive")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Assgin_Schedule_For_PreventiveMaintenace(PreventiveMaintenanceResponseDto responseDto)
        {
            if (responseDto is not PreventiveMaintenanceResponseDto) return BadRequest("Invalid Operation");

            var IsAssgined = await _service.Assgin_Schedule_to_PreventiveMaintence(responseDto);


            return IsAssgined ? Ok("Assgin Operation has been done")
                              : StatusCode(500, "Error occurred on system");
        }


        [HttpPost("reassgin-schedule", Name = "reassgin-schedule-prventive")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Reassgin_Schedule_For_PreventiveMaintenace(ReassginScheduleDto reassgin)
        {
            if (reassgin is not ReassginScheduleDto) return BadRequest("Invalid Operation");

            var IsAssgined = await _service.Reassgin_Schedule_to_PreventiveMaintence(reassgin);


            return IsAssgined ? Ok("ReAssgin Operation has been done")
                              : StatusCode(500, "Error occurred on system");
        }

    }
}
