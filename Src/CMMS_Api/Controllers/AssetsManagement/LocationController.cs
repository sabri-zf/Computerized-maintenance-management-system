using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{
    [ApiController]

    [Route("api/v1/locations")]
    public class LocationController(clsLocations Instance) :Controller
    {


        [HttpGet("retrieve", Name ="retrieve-location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllLocation()
        {
            var List = await Instance.GetAllLocations();

            if (List is not IEnumerable<LocationResponseDto>) return NotFound("Data you looing for doesn't exist");
            
            return Ok(List);
        }


        [HttpGet("retrieve-one/{id:int}", Name ="get-location-byid")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocationResponseDto>> GetLocationById(int id)
        {
            if (id < 1) return BadRequest("Invalid operation");

            var responseDto = await Instance.FindAsync(id);
            if (responseDto is null) return NotFound("Location doesn't find");

            return Ok(responseDto);
        }


        [HttpPost("create",Name ="add-location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddNewLocation(LocationResponseDto responseDto)
        {
            if(responseDto is not LocationResponseDto) return BadRequest("Invalid Operation");

            var Location_obj = await Instance.AddNewLocationAsync(responseDto);

            if (!Location_obj) return StatusCode(5001, "Error occurred when you try to make a instance");


            return Ok("Add new Location has been done");
        }

        [HttpPut("edit",Name ="update-location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateLocation( LocationRequestDto requestDto)
        {
            if (requestDto is not LocationRequestDto || requestDto.ID < 1) return BadRequest("Invalid Operation");

            var Location_obj = await Instance.UpdateLocationAsync(requestDto);

            if (!Location_obj) return StatusCode(5001, "Error Occurred whe you try to make a instance");

            return Ok("Update Location Has been done");
        }

        [HttpDelete("ommit/{id:int}", Name ="remove-location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteLocation(int Id)
        {
            if (Id < 1) return BadRequest("Invalid operation");

            return await Instance.DeleteLocationAsync(Id)
                                     ? Ok("Delete Location Has been Done")
                                     : StatusCode(500, "Error occurred on system");
        }

    }
}
