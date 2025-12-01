using Computerized_maintenance_Logic_layer.Module.DTO.PreventiveMaintenanceManagement;
using Computerized_maintenance_Logic_layer.Module.preventiveMaintenanceManagement;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.PreventiveMaintenanceManagement
{
    [ApiController]
    [Route("api/v1/preventive-maintenances")]
    public class PreventiveMaintenanceController(ClspreventiveMaintenances _Instance) : Controller
    {
        [HttpGet("retrive-pm", Name = "get-all-preventive-maintenances")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllPreventiveMaintenances()
        {
            var List = await _Instance.RetrieveWholeData();
            if (List is null) return NotFound("Data Not Found");
            return Ok(List);
        }

        [HttpGet("retrieve-one-pm/{id:int}", Name = "get-preventive-maintenance-byid")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetPreventiveMaintenanceById(int id)
        {
            if (id < 1) return BadRequest("Invalid operation");

            var responseDto = await _Instance.FindByIdAsync(id);

            if (responseDto is null) return NotFound("Preventive Maintenance doesn't find");

            return Ok(responseDto);
        }


        [HttpPost("create-pm", Name = "add-preventive-maintenance")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddNewPreventiveMaintenance(PreventiveMaintenanceResponseDto responseDto)
        {
            if (responseDto is not PreventiveMaintenanceResponseDto) return BadRequest("Invalid operation");
            var IsAdded = await _Instance.AddPreventiveMaintenanceAsync(responseDto);

            return IsAdded ? Ok("Add New Preventive Maintenance has been Done")
                           : StatusCode(500, "Error Occurred on System");
        }

        [HttpPut("edit-pm", Name = "update-preventive-maintenance")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdatePreventiveMaintenance(PreventiveMaintenanceRequesteDto requesteDto)
        {
            if (requesteDto is not PreventiveMaintenanceRequesteDto || requesteDto.ID < 1) return BadRequest("Invalid operation");
            var IsUpdated = await _Instance.UpdatePreventiveMaintenanceAsync(requesteDto);

            return IsUpdated ? Ok("Update Preventive Maintenance has been Done")
                           : StatusCode(500, "Error Occurred on System");
        }

        [HttpDelete("ommit-pm/{id:int}", Name = "delete-preventive-maintenance")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeletePreventiveMaintenance(int id)
        {
            if (id < 1) return BadRequest("Invalid operation");

            var IsDeleted = await _Instance.DeletePreventiveMaintenanceAsync(id);

            return IsDeleted ? Ok("Delete Preventive Maintenance has been Done")
                             : StatusCode(500, "Error Occurred on System");
        }

    }
}
