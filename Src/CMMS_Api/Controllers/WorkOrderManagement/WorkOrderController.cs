using Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto;
using Computerized_maintenance_Logic_layer.Module.workOrderManagement;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.WorkOrderManagement
{
    [ApiController]
    [Route("Api/V1/work-order")]
    public class WorkOrderController (ClsWorkorders _workorder):Controller
    {

        [HttpGet("get-workorders",Name ="get-all-workorder")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async  Task<ActionResult<IEnumerable<WorkOredrResponseDto>>> GetAllWorkOrders()
        {
            var WorkOrders_List = await _workorder.GetAllWorkOrders();
            if (WorkOrders_List is null || !WorkOrders_List.Any()) return NotFound("Data Doesn't find");

            return Ok(WorkOrders_List);
        }

        [HttpGet("get-work-order/{Id}",Name ="get-Work-order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<WorkOredrResponseDto>> GetByID([FromRoute]int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");

            var WorkOrder_Obj = await _workorder.FindByID(Id);

            if (WorkOrder_Obj is not WorkOredrResponseDto) return NotFound("Data Doesn't find");

            return Ok(WorkOrder_Obj);
        }

        [HttpPost("addnew-work-order")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AddNewWorkOrder([FromBody] WorkOredrResponseDto responseDto)
        {
            if (responseDto is not WorkOredrResponseDto) return BadRequest("Invalid Operation");


            WorkOrder workOrder = new WorkOrder()
            {
                WorkOrderNumber = responseDto.WorkOrderNumber,
                Description     = responseDto.Description,
                AssetID         = responseDto.AssetID,
                CreatedByID     = responseDto.CreatedByID,
                AssignedToID    = responseDto.AssignedToID,
                Status          = Enum.Parse<workOrderStatus>(responseDto.Status),
                CreatedDate     = responseDto.CreatedDate,
                StartDate       = responseDto.StartDate,
                DueDate         = responseDto.DueDate,
                CompeletedDate  = responseDto.CompeletedDate,
                Note            = responseDto.Note
            };


            var New_WorkOrder = await _workorder.AddNewWorkOrder(workOrder);

            if (!New_WorkOrder) return StatusCode(500, "Error occurred on system");



            return Created();
        }


        [HttpPut("edit-work-order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> UpdateWorkOrder([FromBody] WorkOredrRequestDto requestDto)
        {
            if (requestDto is not WorkOredrRequestDto && requestDto.ID < 1) return BadRequest("Invalid Operation");

            var Is_It_Updated = await _workorder.UpdateWorkOrder(requestDto);

            if (!Is_It_Updated) return StatusCode(500, "An error occurred on system");

            return Ok("Work-Order has been Updated ");
        }


        [HttpDelete("delete-work-order/{Id}",Name ="remove-work-order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> DeleteWorkOrder(int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");

            var Is_It_Deleted = await _workorder.DeleteWorkOrder(Id);

            if (!Is_It_Deleted) return StatusCode(500, "An error occurred on system");

            return Ok("Work order has been deleted");
        }

    }


}
