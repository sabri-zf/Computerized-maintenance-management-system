using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using Computerized_maintenance_Logic_layer.Module.DTO;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{
    [ApiController]
    [Route("api/v1/assets")]
    public class AssetController(clsAssets Instance):Controller
    {


        [HttpGet("retrieve",Name ="get-assets")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Asset>> GetAll()
        {
            var List_Assets = await Instance.GetAllAssetsAsync();

            if(List_Assets is null) return NotFound("Data Not Found");


            return Ok(List_Assets);
        }

        [HttpGet("retrieve-one/{asset_name}")] // add costume constatint regx to avoid send number to request
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AssetResponseDto>> GetAssetByName(string asset_name)
        {
            if (string.IsNullOrEmpty(asset_name)) return BadRequest("Invalid Operation");

            var Output = await Instance.FindByAssetNameAsync(asset_name);

            if (Output is null) return NotFound("Asset doesn't find");


            return Ok(Output);
        }

        [HttpGet("retrieve-one/{Id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AssetResponseDto>> GetAssetById(int Id)
        {
            if (Id < 1) return BadRequest("Invalid Operation");

            var Output = await Instance.FindAsync(Id);

            if (Output is null) return NotFound("Asset doesn't find");


            return Ok(Output);
        }

        [HttpPost("create",Name ="add-asset")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<AssetImageRequestDto>> AddNewAsset(AssetResponseDto ResponseDto)
        {

            if (ResponseDto is null) return BadRequest("Invalid Operation");

            var IsAdded = await Instance.AddNewAssetAsync(ResponseDto);

            if (!IsAdded)
            {
                return  StatusCode(500,"Erorr Occurred On System");
            }

            return CreatedAtAction("GetAssetByName", new { asset_name = ResponseDto.AssetName},ResponseDto);
        }

        [HttpPut("edit",Name ="edit-asset")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditAsset(AssetRequestDto requestDto)
        {
            if (requestDto is null || requestDto.ID < 1) return BadRequest("Invalid Operation");

            var find_out = await Instance.UpdateAssetAsync(requestDto);

            return find_out ? Ok("Update Asset has been succeed")
                            : StatusCode(500, "Error Occurred on system");
        }

        [HttpDelete("ommit/{ID:int}",Name ="delete-asset")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeletAsset(int ID)
        {
            if (ID < 1) return BadRequest("Invalid Operation");

            var IsDeleted = await Instance.DeleteAssetAsync(ID);

            if (!IsDeleted)
            {
                return StatusCode(500, "Error Occurred on system");
            }

            return Ok("Delete Asset Has been Done");
        }
    }
}
