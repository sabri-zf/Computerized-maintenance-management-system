using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{
    [ApiController]

    [Route("api/v1/categories")]
    public class CategoryController(clsCategories Instance) :Controller
    {

        [HttpGet("retrieve",Name ="get-all-gategories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllCategories()
        {
            var list = await Instance.GetAllCategories();


            if(list == null) return NotFound("Data is not Found");


            return Ok(list);
        }

        [HttpGet("retrieve-one/{id:int}", Name ="get-gategory-byid")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            if (id < 1) return BadRequest("Invalid operation");

            var responseDto = await Instance.FindAsync(id);

            if (responseDto is null) return NotFound("Category doesn't find");

            return Ok(responseDto);
        }

        [HttpPost("create",Name ="new-gategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddNewGategory(CategoryResponseDto responseDto)
        {
            if (responseDto is not CategoryResponseDto) return BadRequest("Invalid operation");

            var IsAdded = await Instance.AddNewCategoryAsync(responseDto);
           
            return IsAdded ? Ok("Add New Catagory has been Done") 
                           : StatusCode(500, "Error Occurred on System");
        }

        [HttpPut("edit",Name ="update-category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCategory(CategoryRequestDto requestDto)
        {

            if (requestDto is not CategoryRequestDto || requestDto.ID < 1) return BadRequest("Invalid Operation");

            var FindOut = await Instance.UpdateCategoryAsync(requestDto);

            return  FindOut ? Ok("Update Category has been Done") 
                            : StatusCode(500, "Error Occurred on System");
        }

        [HttpDelete("ommit/{id:int}",Name ="Remove-category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteCategory(int ID)
        {
            if (ID < 1) return BadRequest("Invalid Operation");

            return await Instance.DeleteCategoryAsync(ID)
                                 ? Ok("Category has been deleted")
                                 : StatusCode(500, "Error occurred on system");
        }
    }
}
