using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{
    [ApiController]

    [Route("Api/V1/location")]
    public class LocationController:Controller
    {


        [HttpGet("get-locations",Name ="retrieve-location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> GetAllLocation()
        {
            var List = await clsLocations.Instance.GetAllLocations();


            if (List is not IEnumerable<Location>) return NotFound("Data you looing for doesn't exist");
            
            return Ok(List);
        }


        [HttpPost("add-new-location",Name ="add-location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> AddNewLocation([FromBody] LocationResponseDto responseDto)
        {
            if(responseDto is not LocationResponseDto) return BadRequest("Invalid Operation");

            var Location_obj = clsLocations.Instance;

            if (Location_obj is not clsLocations) return StatusCode(5001, "Error occurred when you try to make a instance");

            Location_obj.LocationName = responseDto.LocationName;

            return await Location_obj.AddNewLocationAsync() 
                                     ? Ok(Location_obj)
                                     : StatusCode(500, "error occurred on system");

        }

        [HttpPut("edit-location",Name ="update-location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateLocation([FromBody] LocationRequestDto requestDto)
        {
            if (requestDto is not LocationRequestDto || requestDto.ID < 1) return BadRequest("Invalid Operation");

            var Location_obj = clsLocations.Instance;

            if (Location_obj is not clsLocations) return StatusCode(5001, "Error Occurred whe you try to make a instance");

            Location_obj.LocationName = requestDto.LocationName;

            return await Location_obj.UpdateLocationAsync()
                                     ? Ok("Update Location Has been done")
                                     : StatusCode(500, "Error Occurred on system");
        }

        [HttpDelete("delete-location/{id:int}", Name ="remove-location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> DeleteLocation(int Id)
        {
            if (Id < 1) return BadRequest("Invalid operation");


            return await clsLocations.DeleteLocationAsync(Id)
                                     ? Ok("Delete Location Has been Done")
                                     : StatusCode(500, "Error occurred on system");
        }

    }
}
