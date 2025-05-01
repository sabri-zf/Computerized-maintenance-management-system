using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.User_Management;
using Computerized_maintenance_Logic_layer.Module.User_Management.Extensions;
using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.DTO.DtoWrite;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers
{
    [Route("api.Users/")]
    [ApiController]
    //[Authorize]
    public class UserController:ControllerBase
    {


        [HttpGet("AllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<List<UserTableViewDto>> GetAllUsers()
        {
            var ListOfUsers = ClsUsers.GetAllUsers();

            if(ListOfUsers is null)
            return NotFound("Not Found : Data of User Not Found");

            return Ok(ListOfUsers);
        }


        [HttpGet("GetUserByID{id}",Name ="Get User By ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<Api_UserDto> GetUserByID(int? id)
        {
            if (id is null || id <= 0) return BadRequest($"Error : Invlid ID ({id})");


            if (!ClsUsers.IsExistUser(id)) return StatusCode(500, "Server : User Doesn't Exist");

            var FindUser = ClsUsers.FindUser(id);

            if(FindUser is not ClsUsers)
            {
                return NotFound("Not Found : Error Occurred invalid User");
            }


            Api_UserDto apiUser = new()
            {
                UserId = FindUser.UserID,
                UserName = FindUser.UserName,
                FirstName = FindUser.First_Name,
                LastName = FindUser.Last_Name,
                Email = FindUser.Email,
                Phone = FindUser.Phone,
                Address = FindUser.Address,
                BirthDay = FindUser.BithDay,
                RoleName = FindUser.Role?.RoleName,
                Permission = FindUser.Permisson,
                IsActive = FindUser.IsActive
            };

            return Ok(apiUser);
        }

        [HttpPost("AddNewUser",Name = "Add New User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<Api_UserDto> AddNewUser(UserWriteData userWrite)
        {
            if (userWrite is not UserWriteData)
            {
                return BadRequest("Bad Request : Error occurred on Sent");
            }

            var User = new ClsUsers(userWrite);

            if(!User.Save())
            {
               return StatusCode(500, "Server : Error has been occurred ,User Not Saved");
            }

            return Ok($"Add new User id ({User.UserID}) Hass been Successful ");
        }


        [HttpPut("Update{id}", Name = "Update User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult UpdateUser(int id, UserWriteData userWrite)
        {
            if (id <= 0) return BadRequest("Error : Invalid Request");

            if (userWrite is not UserWriteData)
            {
                return BadRequest("Error : Error occurred on Sent");
            }

            var FindUser = ClsUsers.FindUser(id);

            if (FindUser == null) return NotFound("Not Found : User Not Found");

            FindUser.UserName   = userWrite.UserName;
            FindUser.Password   = userWrite.Password;
            FindUser.RoleID     = userWrite.RoleID;
            FindUser.Permisson  = userWrite.permission;
            FindUser.IsActive   = userWrite.IsActive;
            FindUser.First_Name = userWrite.FirstName;
            FindUser.Last_Name  = userWrite.LastName;
            FindUser.Email      = userWrite.Email;
            FindUser.Phone      = userWrite.Phone;
            FindUser.BithDay    = userWrite.BirthDay;
            FindUser.Address    = userWrite.Addrees;


            if (!FindUser.Save())
            {
                return StatusCode(500, "Server : Error has been occurred ,User not Saved");
            }



            return Ok($"Update user ID:({id}) Has been successed");
        }


        [HttpDelete("Delete{id}", Name = "Delete User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult DeleteUser(int id)
        {
            if (id < 1 ) return BadRequest($"Bad Requst : Invalid Request ID {id}");

            if(!ClsUsers.IsExistUser(id))
            {
                return NotFound("Not Found : Invalid User");
            }

            int? PersonId = UserService.GetPersonIdOfUser(id);

            if (!PersonId.HasValue) return StatusCode(500, "Server : Error occurred on system");

            if (!ClsUsers.DeleteUser(id,PersonId))
            {
                return StatusCode(500,"Server : Error has been occurred");
            }

            return Ok($"Delete user ID:({id}) Has been successed");
        }


    }
}
