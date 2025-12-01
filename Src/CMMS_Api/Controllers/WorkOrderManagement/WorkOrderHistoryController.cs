using Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto;
using Computerized_maintenance_Logic_layer.Module.workOrderManagement;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.WorkOrderManagement
{
    [ApiController]
    [Route("api/v1/work-order-histories")]
    public class WorkOrderHistoryController(ClsWorkOrderHistory _workOrderHistory) : Controller
    {

        [HttpGet("retrieve", Name = "get-all-workorder-hisorty")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<WorkOredrResponseDto>>> GetAllWorkOrderHistories()
        {
            var WorkOrders_List = await _workOrderHistory.GetAllHistory();
            if (WorkOrders_List is null || !WorkOrders_List.Any()) return NotFound("Data Doesn't find");

            return Ok(WorkOrders_List);
        }

        [HttpGet("retrieve-one/{Id}", Name = "get-Work-order-history")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<WorkOrderHistoryResponseDto>> GetByID([FromRoute] int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");

            var WorkOrder_Obj = await _workOrderHistory.FindByID(Id);

            if (WorkOrder_Obj is not WorkOrderHistoryResponseDto) return NotFound("Data Doesn't find");

            return Ok(WorkOrder_Obj);
        }

        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AddNewWorkOrderHistory([FromBody] WorkOrderHistoryResponseDto responseDto)
        {
            if (responseDto is not WorkOrderHistoryResponseDto) return BadRequest("Invalid Operation");

            var New_WorkOrder = await _workOrderHistory.AddNewHistory(responseDto);

            if (!New_WorkOrder) return StatusCode(500, "Error occurred on system");

            return Created();
        }


        [HttpPut("edit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> UpdateWorkOrderHistory([FromBody] WorkOrderHistoryRequestDto requestDto)
        {
            if (requestDto is not WorkOrderHistoryRequestDto && requestDto.ID < 1) return BadRequest("Invalid Operation");

            var Is_It_Updated = await _workOrderHistory.UpdateHistory(requestDto);

            if (!Is_It_Updated) return StatusCode(500, "An error occurred on system");

            return Ok("Work-Order history has been Updated");
        }


        [HttpDelete("ommit/{Id}", Name = "remove-work-order-history")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> DeleteWorkOrderHistory(int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");

            var Is_It_Deleted = await _workOrderHistory.DeleteHistory(Id);

            if (!Is_It_Deleted) return StatusCode(500, "An error occurred on system");

            return Ok("Work order History has been deleted");
        }

    }


}
