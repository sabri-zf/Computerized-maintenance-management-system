using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{

    [ApiController]
    [Route("Api/V1/asset-image")]
    public class AssetImageController:Controller
    {
        [HttpGet("get-images")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AssetImage>> GetAll()
        {
            var List_Image = await ClsAssetImage.Instance!.GetAllImages();

            if (List_Image is null)
            {
                return NotFound( "Data doesn't find");
            }

            return Ok(List_Image);
        }

        [HttpPost("add-assetimage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> AddAssetImage(AssetImageResponseDto responseDto)
        {
            if (responseDto == null) return BadRequest("Invalid Operation");

            var insertImage = ClsAssetImage.Instance;

            if (insertImage is null) return StatusCode(500, "Error Ouccrred on System");

            insertImage.ImagePath = responseDto.ImagePath;
            insertImage.ImageWidth = responseDto.Imagewidth;
            insertImage.ImageHeight = responseDto.ImageHight;
            insertImage.AssetID = responseDto.AssetID;

            return await insertImage.AddNewImageAsync() ? Created("get-assetimage/{id}", insertImage)
                                                        : StatusCode(500, "Error Ouccrred on System"); ;
        }

        [HttpPut("edit-assetimage/{id:int}",Name ="edit-image")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> EditAssetImag(AssetImageRequestDto requestDto)
        {

            if (requestDto == null || requestDto.ID < 1) return BadRequest("Invalid Operation");

            var Asset_Image = await ClsAssetImage.Instance.FindAsync(requestDto.ID);

            if (Asset_Image is null) return NotFound("not found data");

            Asset_Image.ImagePath = requestDto.ImagePath;
            Asset_Image.ImageWidth = requestDto.Imagewidth;
            Asset_Image.ImageHeight = requestDto.ImageHight;


            return Ok(new {imagePath = Asset_Image.ImagePath,Asset_Image.ImageWidth , Asset_Image.ImageHeight, status = "image has been update it"});
        }

        [HttpDelete("delete-assetImage/{id:int}",Name ="delete-image")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteAssetImage(int id)
        {
            if (id < 1) return BadRequest("invalid operation");

            var Is_deleted = await ClsAssetImage.DeleteImageAsync(id);


            if (Is_deleted) return StatusCode(500, "Erorr occurred on system");


            return Ok("Image Asset Has been Deleted");
        }
    }
}
