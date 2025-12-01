using Computerized_maintenance_Logic_layer.Module.User_Management;
using computrized_maintenance_Data_Access.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.UsersManagment
{
    [Route("api/v1/users")]
    [ApiController]
    //[Authorize]
    public class UserController(ClsUsers _instance):Controller
    {

        [HttpGet("retrieve",Name ="retrieve_all_Users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllUsers()
        {

            try
            {
                var ListOfUsers = await _instance.GetAllUsersAsync();

                if (ListOfUsers is null)
                    return NotFound("Not Found : Data of User Not Found");

                return Ok(ListOfUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }
        }

        [HttpGet("retrieve-one/{id}",Name ="get_one_user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async  Task<ActionResult> GetUserByID(int id)
        {
            if (id < 1) return BadRequest($"Error : Invlid ID ({id})");

            try
            {
                if (!await _instance.IsExistUserAsync(id)) return StatusCode(500, "Server : User Doesn't Exist");

                var FindUser = await _instance.FindUserAsync(id);

                if (FindUser is not UserDtoResponse)
                {
                    return NotFound("Not Found : Error Occurred invalid User");
                }

                return Ok(FindUser);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }
        }

        [HttpPost("create",Name = "add_new_user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async  Task<ActionResult> AddNewUser(ProcessAddUserDto resquest)
        {
            if (resquest is not ProcessAddUserDto)
            {
                return BadRequest("Bad Request : Invalid operation");
            }

            try
            {
                var IsInseted = await _instance.AddNewUserAsync(resquest);

                if (!IsInseted)
                {
                    return StatusCode(500, "Server : Error has been occurred ,User Not Saved");
                }

                return Ok($"Add new User id Hass been Successful ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }
        }


        [HttpPut("edit/{id}", Name = "update_user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUser(ProcessUpdateUserDto resquest)
        {
            if (resquest.UserId < 1 || resquest is not ProcessUpdateUserDto) return BadRequest("Bad a request : Invalid Operation");

            try
            {
                var FindUser = await _instance.UpdateUserAsync(resquest);

                if (!FindUser)
                {
                    return StatusCode(500, "Server : Error has been occurred ,User doesn't Save");
                }

                return Ok($"Update a user Has been succeed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }

        }


        [HttpDelete("omit/{id}", Name = "delete_user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id < 1 ) return BadRequest($"Bad Requst : Invalid operation");

            try
            {
                if (!await _instance.IsExistUserAsync(id))
                {
                    return NotFound("Not Found : User doesn't find");
                }

                if (!await _instance.DeleteUserAsync(id))
                {
                    return StatusCode(500, "Server : Error has been occurred");
                }

                return Ok($"Delete user Has been succeed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }

        }


    }
}
