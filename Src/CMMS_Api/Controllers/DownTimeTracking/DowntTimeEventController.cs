using Computerized_maintenance_Logic_layer.Module.DownTimeTracking;
using Computerized_maintenance_Logic_layer.Module.DTO.DownTimeTrackingDto;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.DownTimeTracking
{
    [ApiController]
    [Route("Api/V1/downtime")]
    public class DowntTimeEventController(ClsDownTimeEvents _instance):Controller
    {

        [HttpGet("get-downtimes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<IEnumerable<DownTimeEventResponseDto>>> GetDowntimesEvent()
        {
            try
            {
                var List = await _instance.GetAllDownTimes();

                if(List is null) return NotFound("List of Downtime doesn't exist");

                return Ok(List);
            }
            catch (Exception ex)
            {
                // logging exception 

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-downtime/{Id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<DownTimeEventResponseDto>> GetDownTimeByID(int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");
            try
            {
                var Find_out = await _instance.FindById(Id);

                if (Find_out is not DownTimeEventResponseDto) return NotFound("DowntimeEvent Doesn't find");

                return Ok(Find_out);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [HttpPost("add-downtime")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AddNewDownTimeEvent(DownTimeEventResponseDto responseDto)
        {
            if (responseDto == null) return BadRequest("Invalid Operation");

            try
            {
                var IsAdded = await _instance.AddNewDownTimeEvent(responseDto);


                return IsAdded ? Ok("Add Down-Time Event has been succeed")
                               : StatusCode(500, "Error occurred on sysmtem");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("edit-downtime")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> UpdateDownTimeEvent(DownTimeEventRequestDto requestDto)
        {

            if (requestDto == null) return BadRequest("Invalid Operation");

            try
            {
                var  IsUpdated = await _instance.UpdateDownTimeEvent(requestDto);


                return IsUpdated ? Ok("Update DownTime has been succeed")
                                 : StatusCode(500, "Erro occurred on system");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpDelete("delete-downtime/{Id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteDownTimeEvent(int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");

            try
            {
                return await _instance.DeleteDownTimeEvent(Id) ? Ok("delete DownTime Event has been done")
                                                               : StatusCode(500, "Error occurred on system");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
