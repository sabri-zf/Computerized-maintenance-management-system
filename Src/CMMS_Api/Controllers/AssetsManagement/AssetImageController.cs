using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{

    [ApiController]
    [Route("Api/V1/asset-image")]
    public class AssetImageController(ClsAssetImage Instance) :Controller
    {
        [HttpGet("get-asset-images",Name ="asset-images")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetAllImagesAsync()
        {
            var List_Image = await Instance.GetAllAssetImage();

            if (List_Image is null)
            {
                return NotFound( "Data doesn't find");
            }

            return Ok(List_Image);
        }


        [HttpGet("get-asset-image/{Id:int}",Name ="get-asset-image-byid")]
        public async Task<ActionResult> GetByIDAssetImageAsync(int Id)
        {
            if(Id< 1) return BadRequest("Invalid operation");

            var ResopnseEntity = await Instance.FindAsync(Id);

            return ResopnseEntity is AssetImageResponseDto
                                  ? Ok(ResopnseEntity)
                                  : StatusCode(500,"An error on system");
        }

        [HttpPost("add-asset-image",Name ="make-asset-image")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> AddAssetImageAsync(AssetImageResponseDto responseDto)
        {
            if (responseDto == null) return BadRequest("Invalid Operation");

            var insertImage = await Instance.AddNewImageAsync(responseDto);

            return insertImage ? Created("get-assetimage/{id}", insertImage)
                               : StatusCode(500, "Error Ouccrred on System"); ;
        }

        [HttpPut("edit-asset-image",Name ="update-asset-image")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> EditAssetImagAsync(AssetImageRequestDto requestDto)
        {
            if (requestDto is not AssetImageRequestDto || requestDto.ID < 1) return BadRequest("Invalid Operation");

            var IsItupdated = await Instance.UpdateImageAsync(requestDto);

            return IsItupdated ? Ok("Asset image has been update it")
                               : StatusCode(500, "An error occurred on system");
        }

        [HttpDelete("delete-asset-image/{id:int}",Name ="delete-asset-image")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteAssetImage(int id)
        {
            if (id < 1) return BadRequest("invalid operation");

            var Is_deleted = await Instance.DeleteImageAsync(id);

            return Is_deleted ? Ok("Image Asset Has been Deleted")
                              : StatusCode(500, "Erorr occurred on system");
        }
    }
}
