using Computerized_maintenance_Logic_layer.Module.User_Management;
using computrized_maintenance_Data_Access.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.UsersManagment
{
    [ApiController]
    [Route("api/v1/roles")]
    //[Authorize]
    public class RoleController(ClsRoles _instance) : Controller
    {

        [HttpGet("retrieve", Name = "retrieve_all_roles")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllRoles()
        {
            try
            {
                var Role_List = await _instance.RetriveAllRolesAsync();

                if (Role_List == null) return NotFound("Not Found : list of Roles don't find");

                return Ok(Role_List);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error: {ex.Message}");
            }
        }

        [HttpGet("retrive-one/{rolename:alpha}",Name ="retrive-role-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> GetRoleData(string rolename)
        {
            if (string.IsNullOrEmpty(rolename)) return BadRequest("Invalid operation");


        }

        [HttpPost("create", Name = "add_new_role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddNewRole(RoleDtoResponse request)
        {
            if (request is not RoleDtoResponse || string.IsNullOrEmpty(request.RoleName)) return BadRequest("Bad request : Invalid Data");

            try
            {
                bool IsInserted = await _instance.AddnewRoleAsync(request.RoleName);

                if (!IsInserted) return StatusCode(500, "Server : Error occurred on system");

                Ok("the operation of add new admin has been done");

                return StatusCode(200, $"The operation has been succeed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }

        }

        [HttpPut("edit", Name = "update_role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateRole([FromQuery]string oldRolename,[FromQuery]string newRolename)
        {
            if (string.IsNullOrEmpty(newRolename) || string.IsNullOrEmpty(oldRolename)) return BadRequest("Bad Request : Invalid operation");

            try
            {

                var Isupdated = await _instance.UpdateRoleAsync(oldRolename,newRolename);

                if (Isupdated) return StatusCode(500, "Error ouccourred on system, update had been failed");

                return Ok("The operation (Update an Role) has been succeed");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }

        }

        [HttpDelete("omit/{rolename:alpha}", Name = "DeleteRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAdmin(string rolename)
        {
            if (string.IsNullOrEmpty(rolename)) return BadRequest("Bad Request : Invalid operation");


            try
            {
                if (!await _instance.DeleteRoleAsync(rolename)) return StatusCode(500, "Server : Error occurred on system");

                return Ok("the operationhas been succeed");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Server error : {ex.Message}");
            }

        }
    }
}
