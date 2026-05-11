using Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto;
using Computerized_maintenance_Logic_layer.Module.workOrderManagement;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.WorkOrderManagement
{
    [ApiController]
    [Route("api/v1/work-orders")]
    public class WorkOrderController (ClsWorkorders _workorder):Controller
    {

        [HttpGet("retrieve", Name ="get-all-workorder")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async  Task<ActionResult<IEnumerable<WorkOredrList_view>>> GetAllWorkOrders()
        {
            var WorkOrders_List = await _workorder.GetAllWorkOrders();
            if (WorkOrders_List is null || !WorkOrders_List.Any()) return NotFound("Data Doesn't find");

            return Ok(WorkOrders_List);
        }


        // add fetch data with pagenation


        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<WorkOredrList_view>?>> GetWorkOrderByPage([FromQuery]short PageNumber = 1)
        {
            if(PageNumber <= 0) return BadRequest("Invalid input");

            var GetSegment = await _workorder.GetByPage(PageNumber);

            if (GetSegment is null || GetSegment.Count() <= 0) return NotFound("Bad Request: Data don't found");


            return Ok(GetSegment);
        }

        [HttpGet("retrieve-one/{Id}", Name ="get-Work-order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<WorkOredrList_view>> GetByID([FromRoute]int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");

            var WorkOrder_Obj = await _workorder.FindByID(Id);

            if (WorkOrder_Obj is not WorkOredrResponseDto) return NotFound("Data Doesn't find");

            return Ok(WorkOrder_Obj);
        }

        [HttpPost("create")]
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
                Status          = Enum.Parse<En_workOrderStatus>(responseDto.Status),
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


        [HttpPut("edit")]
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


        [HttpDelete("ommit/{Id}",Name ="remove-work-order")]
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
