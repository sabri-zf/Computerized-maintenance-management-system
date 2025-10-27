using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{
    [ApiController]

    [Route("Api/V1/category")]
    public class CategoryController:Controller
    {

        [HttpGet("get-gategories",Name ="get-all-gategories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllCategories()
        {
            var list = await clsCategories.Instance.GetAllCategories();


            if(list == null) return NotFound("Data is not Found");


            return Ok(list);
        }


        [HttpPost("add-gategory",Name ="new-gategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> AddNewGategory([FromBody] CategoryResponseDto responseDto)
        {
            if (responseDto is not CategoryResponseDto) return BadRequest("Invalid operation");

            var incomingResult = clsCategories.Instance;

            if(incomingResult is not null)
            {
                incomingResult.Category_Name = responseDto.CategoryName;

                return await incomingResult.AddNewCategoryAsync() ? Ok(incomingResult) : StatusCode(500, "Error Occurred on System");
            }

            return NotFound("Catagory Object Doesn't find");
        }

        [HttpPut("edit-category",Name ="update-category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> UpdateCategory([FromBody] CategoryRequestDto requestDto)
        {

            if (requestDto is not CategoryRequestDto || requestDto.ID < 1) return BadRequest("Invalid Operation");

            var FindOut = await clsCategories.Instance.FindAsync(requestDto.ID);

            if (FindOut is not clsCategories) return NotFound($"Category related with this ID '{requestDto.ID}' doesn't exist");

            FindOut.Category_Name = requestDto.CategoryName;

            return await FindOut.UpdateCategoryAsync() ? Ok("Update Category has been Done") : StatusCode(500, "Error Occurred on System");
        }


        [HttpDelete("delete-gategory/{id:int}",Name ="Remove-category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> DeleteCategory(int ID)
        {
            if (ID < 1) return BadRequest("Invalid Operation");

            return await clsCategories.Instance.DeleteCategoryAsync(ID)
                                               ? Ok("Category has been deleted")
                                               : StatusCode(500, "Error occurred on system");
        }

    }
}
