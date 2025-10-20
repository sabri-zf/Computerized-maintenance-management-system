using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers
{
    [ApiController]
    [Route("Api/V1/sub-gategory")]
    public class SubCategoryController:Controller
    {


        [HttpGet("get-sub-categories",Name ="retrieve-sub-category")]
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


       
    }
}
