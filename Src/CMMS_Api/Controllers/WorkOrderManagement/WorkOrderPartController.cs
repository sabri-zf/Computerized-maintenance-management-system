using Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto;
using Computerized_maintenance_Logic_layer.Module.workOrderManagement;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.WorkOrderManagement
{
    [ApiController]
    [Route("api/v1/work-order-parts")]
    public class WorkOrderPartController(ClsWorkOrderParts _workorder_parts) : Controller
    {

        [HttpGet("retrieve", Name = "get-all-workorder-parts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<WorkOrderPartResponseDto>>> GetAllWorkOrdersParts()
        {
            var WorkOrdersPart_List = await _workorder_parts.GetAllItems();
            if (WorkOrdersPart_List is null || !WorkOrdersPart_List.Any()) return NotFound("Data Doesn't find");

            return Ok(WorkOrdersPart_List);
        }

        [HttpGet("retrieve-one/{Id}", Name = "get-Work-order-part")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WorkOrderPartResponseDto>> GetByID([FromRoute] int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");

            var WorkOrderPart_Obj = await _workorder_parts.FindByID(Id);

            if (WorkOrderPart_Obj is not WorkOrderPartResponseDto) return NotFound("Data Doesn't find");

            return Ok(WorkOrderPart_Obj);
        }

        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AddNewWorkOrder([FromBody] WorkOrderPartResponseDto responseDto)
        {
            if (responseDto is not WorkOrderPartResponseDto) return BadRequest("Invalid Operation");


            WorkOrderPart workOrderPart = new WorkOrderPart()
            {
             WO_ID = responseDto.WorkOrderID,
             PartItemID = responseDto.PartItemID,
             QuantityUsed = responseDto.QuantityUsed 
            };


            var New_WorkOrder = await _workorder_parts.AddNewItem(workOrderPart);

            if (!New_WorkOrder) return StatusCode(500, "Error occurred on system");

            return Created();
        }

        [HttpPut("edit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateWorkOrder([FromBody] WorkOrderPartRequestDto requestDto)
        {
            if (requestDto is not WorkOrderPartRequestDto && requestDto.ID < 1) return BadRequest("Invalid Operation");

            var Is_It_Updated = await _workorder_parts.UpdateItem(requestDto);

            if (!Is_It_Updated) return StatusCode(500, "An error occurred on system");

            return Ok("Work-Order has been Updated ");
        }

        [HttpDelete("ommit/{Id}", Name = "remove-work-order-Part")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteWorkOrder(int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");

            var Is_It_Deleted = await _workorder_parts.DeleteItem(Id);

            if (!Is_It_Deleted) return StatusCode(500, "An error occurred on system");

            return Ok("Work order has been deleted");
        }

    }



}
