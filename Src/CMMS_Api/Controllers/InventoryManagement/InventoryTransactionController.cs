using Computerized_maintenance_Logic_layer.Module.DTO.InventoryDto;
using Computerized_maintenance_Logic_layer.Module.InventoryManagement;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.InventoryManagement
{
    [ApiController]
    [Route("api/v1/inventory-Tranactions")]
    public class InventoryTransactionController(ClsInventoryItems _InventoryItem):Controller
    {


        [HttpGet("retrieve", Name = "retrieve-inventorytrasactions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<IEnumerable<InventoryTrasactionsResponseDto>>> GetAllItems()
        {

            var List = await _InventoryItem.GetAllInventoryItems();

            if (List is null) return NotFound("Data Doesn't exist");

            return Ok(List);
        }


        [HttpGet("retrieve-one/{Id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<InventoryItemResponseDto>> GetPartItemById(int Id)
        {
            if (Id < 1) return BadRequest("Invalid Opration");

            var Response = await _InventoryItem.GetById(Id);

            if (Response is null) return NotFound("Data you have been looked for doesn't exist");

            return Ok(Response);
        }


        [HttpPost("create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<InventoryItemResponseDto>> AddNewPartItem(InventoryItemResponseDto responseDto)
        {
            if (responseDto is not InventoryItemResponseDto) return BadRequest("Invalid Opration");

            var Response = await _InventoryItem.AddNewInventoryItem(responseDto);

            if (Response) return NotFound("Data you have been looked for add it, doesn't exist");

            return Ok("Add new Part Item has been Succeed");
        }



        [HttpPut("edit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<InventoryItemResponseDto>> UpdatePartItem(InventoryItemRequestDto requestDto)
        {
            if (requestDto is not InventoryItemRequestDto && requestDto.ID < 1) return BadRequest("Invalid Opration");

            var Response = await _InventoryItem.UpdateInventoryItem(requestDto);

            if (Response) return NotFound("Data you have been looked for update it, doesn't exist");

            return Ok("Update Part Item has been Succeed");
        }


        [HttpDelete("ommit/{Id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<InventoryItemResponseDto>> DeletePartItem(int Id)
        {
            if (Id < 1) return BadRequest("Invalid Opration");

            var Response = await _InventoryItem.DeleteInventoryItem(Id);

            if (Response) return NotFound("Data you have been looked for delete it, doesn't exist");

            return Ok("Delete Part Item has been Succeed");
        }







    }
}
