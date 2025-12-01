using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{

    [ApiController]
    [Route("api/v1/asset-images")]
    public class AssetImageController(ClsAssetImage Instance) :Controller
    {
        [HttpGet("retrieve",Name ="asset-images")]
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


        [HttpGet("retrieve-one/{Id:int}",Name ="get-asset-image-byid")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> GetByIDAssetImageAsync(int Id)
        {
            if(Id< 1) return BadRequest("Invalid operation");

            var ResopnseEntity = await Instance.FindAsync(Id);

            return ResopnseEntity is AssetImageResponseDto
                                  ? Ok(ResopnseEntity)
                                  : NotFound("Data you looking for is not Found");
        }

        [HttpPost("create",Name ="make-asset-image")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> AddAssetImageAsync(AssetImageResponseDto responseDto)
        {
            if (responseDto is not AssetImageResponseDto) return BadRequest("Invalid Operation");

            var insertImage = await Instance.AddNewImageAsync(responseDto);

            return insertImage ? Ok("Add new image Asset has been done")
                               : StatusCode(500, "Error Ouccrred on System"); ;
        }

        [HttpPut("edit",Name ="update-asset-image")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> EditAssetImagAsync(AssetImageRequestDto requestDto)
        {
            if (requestDto is not AssetImageRequestDto || requestDto.ID < 1) return BadRequest("Invalid Operation");

            var IsUpdated = await Instance.UpdateImageAsync(requestDto);

            return IsUpdated ? Ok("Asset image has been updated")
                             : StatusCode(500, "An error occurred on system");
        }

        [HttpDelete("ommit/{id:int}",Name ="delete-asset-image")]
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
