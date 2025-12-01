using Computerized_maintenance_Logic_layer.Module.User_Management;
using computrized_maintenance_Data_Access.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.UsersManagment
{
    [ApiController]
    [Route("api/v1/admins")]
    //[Authorize]
    public class AdminController(ClsAdmins _instance):ControllerBase
    {

        [HttpGet("retrieve",Name ="retrieve_all_admins")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllAdmins()
        {
            try
            {
                var AdminsList = await _instance.GetAllAsync();

                if (AdminsList == null) return NotFound("Not Found : Data of admin is not found");

                return Ok(AdminsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error: {ex.Message}");
            }
        }

        [HttpGet("retrieve-one/{id}",Name ="get_one_admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> FindById(int id)
        {

            if(id < 1) return BadRequest("Bad Request : Invalid Input");

            try
            {
                if (!await _instance.IsExistAsync(id)) return StatusCode(500, "Server : Error occurred on System");

                var FindAdmin = await _instance.FindAsync(id);

                if (FindAdmin is not AdminDtoResponse) return BadRequest("Bad Request : Invalid Admins");


                return Ok(FindAdmin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }

           
        }

        [HttpPost("create", Name = "add_new_admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddNewAdmin(ProcessAddUserDto request)
        {
            if (request is not ProcessAddUserDto) return BadRequest("Bad request : Invalid Data");

            try
            {
                bool IsInserted = await _instance.AddNewAsync(request);

                if (!IsInserted) return StatusCode(500, "Server : Error occurred on system");

                Ok("the operation of add new admin has been done");

                return StatusCode(200, $"The operation has been succeed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }
           
        }

        [HttpPut("edit/{id}", Name = "update_admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAdmin(ProcessUpdateAdminDto request)
        {
            if (request is not ProcessUpdateAdminDto && request.AdminID < 1) return BadRequest("Bad Request : Invalid operation");


            try
            {

                var Isupdated = await _instance.UpdateAsync(request);

                if (Isupdated) return StatusCode(500, "Error ouccourred on system, update had been failed");

                return  Ok("The operation (Update an admin) has been succeed");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }

        }

        [HttpDelete("omit/{id}", Name = "DeleteAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAdmin(int id)
        {
            if (id < 1) return BadRequest("Bad Request : Invalid operation");


            try
            {
                if (!await _instance.IsExistAsync(id)) return NotFound($"Not Found : the Admin with this id ({id}), Doesn't exist");

                if (!await _instance.DeleteAsync(id)) return StatusCode(500, "Server : Error occurred on system");

                return Ok("the operationhas been succeed");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Server error : {ex.Message}");
            }
            
        }
    }
}
