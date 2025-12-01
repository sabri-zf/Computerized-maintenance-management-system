using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{
    [ApiController]
    [Route("api/v1/subgategories")]
    public class SubCategoryController(clsSubCategories Instance) :Controller
    {


        [HttpGet("retrieve", Name = "retrieve-sub-category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<SubCategoryResponseDto>>> GetAllSubCategory()
        {
            var List = await Instance.GetAllSubCategories();

            if (List is not IEnumerable<SubCategoryResponseDto>) return NotFound("Data Doesn't exist");

            return Ok(List);
        }


        [HttpGet("retrieve-one/{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SubCategoryResponseDto>> GetSubCategoryByID(int id)
        {
            if (id < 1) return BadRequest("Invalid operation");

            var SubCategory_obj = await Instance.FindAsync(id);

            return SubCategory_obj is SubCategoryResponseDto ? Ok(SubCategory_obj)
                                                             : NotFound($"SubCategory with that Id '{id}' desn't find");
        }


        [HttpPost("create",Name ="add-subcategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(501)]
        public async Task<ActionResult> AddNewSubCategory(SubCategoryResponseDto responseDto)
        {
            if (responseDto is not SubCategoryResponseDto) return BadRequest("invalid operation");

            var IsAdded = await Instance.AddNewSubCategoryAsync(responseDto);

            if (!IsAdded) return StatusCode(StatusCodes.Status501NotImplemented,"Error when try to make a instance");

          
            return  Ok("Add new SubCategory has been done") ;
        }


        [HttpPut("edit", Name ="update-subcategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateSubCategory(SubCategoryRequestDto requestDto)
        {
            if (requestDto is not SubCategoryRequestDto || requestDto.ID < 1) return BadRequest("Invalid operation");

            return await Instance.UpdateSubCategoryAsync(requestDto)
                                        ? Ok("Update SubCategory Has been Done")
                                        : StatusCode(500, "Error Occurred On system");
        }


        [HttpDelete("ommit/{ID:int}",Name ="remove-subcategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task <ActionResult> DeleteSubCategory(int ID)
        {
            if (ID < 1) return BadRequest("Invalid operation");

            return await Instance.DeleteSubCategoryAsync(ID)
                                         ? Ok("SubCategory has been Deleted")
                                         : StatusCode(500, "Error Occurred on system");
        }

    }
}
