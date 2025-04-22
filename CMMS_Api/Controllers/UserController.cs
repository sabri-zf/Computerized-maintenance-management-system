using Computerized_maintenance_Logic_layer.Module.User_Management;
using computrized_maintenance_Data_Access.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers
{
    [ApiController]
    [Route("/Users/")]
    [Authorize]
    public class UserController:ControllerBase
    {


        [HttpGet("AllUsers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]

        public ActionResult<List<UserViewDto>> GetAllUsers()
        {
            var ListOfUsers = ClsUsers.GetAllUsers();

            if(ListOfUsers is null) return NotFound("Data Not Found");

            return Ok(ListOfUsers);
        }


    }
}
