using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{
    [ApiController]
    [Route("Api/V1/subgategory")]
    public class SubCategoryController:Controller
    {


        [HttpGet("get-subcategories",Name ="retrieve-sub-category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> GetAllSubCategory()
        {
            var List = await clsSubCategories.Instance.GetAllSubCategories();

            if (List is not IEnumerable<SubCategory>) return NotFound("Data Doesn't exist"); 

            return Ok(List);
        }

        [HttpPost("add-new-subcategory",Name ="add-subcategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(501)]

        public async Task<ActionResult> AddNewSubCategory([FromBody] SubCategoryResponseDto responseDto)
        {
            if (responseDto is not SubCategoryResponseDto) return BadRequest("invalid operation");

            var SubCategory_Obj = clsSubCategories.Instance;

            if (SubCategory_Obj is not clsSubCategories) return StatusCode(StatusCodes.Status501NotImplemented,"Error when try to make a instance");

            SubCategory_Obj.Sub_Category_Name = responseDto.SubCategoryName;
            SubCategory_Obj.CategoryID = responseDto.CategoryID;

            return await SubCategory_Obj.AddNewSubCategoryAsync() ? Ok(SubCategory_Obj) : StatusCode(500, "Error occurred on system");

        }


        [HttpPut("edit-subcategory", Name ="update-subcategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> UpdateSubCategory([FromBody] SubCategoryRequestDto requestDto)
        {
            if (requestDto is not SubCategoryRequestDto || requestDto.ID < 1) return BadRequest("Invalid operation");

            var Findout = await clsSubCategories.Instance.FindAsync(requestDto.ID);

            if (Findout is not clsSubCategories) return NotFound( "Error occurred when to make a instance");

            Findout.Sub_Category_Name = requestDto.SubCategoryName;
            // here you can make endpoint to send data to db 
            // e.i update subcategories set SubcategoryName = "your value" where Id = Id;


            return await Findout.UpdateSubCategoryAsync()
                                        ? Ok("Update SubCategory Has been Done")
                                        : StatusCode(500, "Error Occurred On system");
        }


        [HttpDelete("delete-subcategory/{ID:int}",Name ="remove-subcategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        
        public async Task <ActionResult> DeleteSubCategory(int ID)
        {
            if (ID < 1) return BadRequest("Invalid operation");

            return await clsSubCategories.DeleteSubCategoryAsync(ID)
                                         ? Ok("SubCategory has been Deleted")
                                         : StatusCode(500, "Error Occurred on system");
        }

    }
}
