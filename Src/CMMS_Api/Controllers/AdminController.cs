using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.User_Management;
using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.DTO.DtoWrite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers
{
    [ApiController]
    [Route("api.Admin/")]
    //[Authorize]
    public class AdminController:ControllerBase
    {


        [HttpGet("AllAdmins",Name ="AllAdmins")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<List<AdminTableViewDto>> GetAllAdmins()
        {
            var AdminsList = ClsAdmins.GetAllAdmin();

            if (AdminsList == null) return NotFound("Not Found : Data of admin is not found");


            return Ok(AdminsList);
        }



        [HttpGet("GetAdminById{id}",Name ="GetAdminById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<Api_AdminDto> getAdminByID(int id)
        {

            if(id < 1) return BadRequest("Bad Request : Invalid Input");

            if (!ClsAdmins.IsExistAdmin(id)) return StatusCode(500, "Server : Error occurred on System");

            var FindAdmin = ClsAdmins.Find(id);

            if (FindAdmin is not ClsAdmins) return BadRequest("Bad Request : Invalid Admins");

            Api_AdminDto AdminDto = new()
            {

                ID = id,
                UserName = FindAdmin.Users?.UserName,
                FirstName = FindAdmin.Users?.First_Name,
                LastName = FindAdmin.Users?.Last_Name,
                Email= FindAdmin.Users?.Email,
                Phone= FindAdmin.Users?.Phone,
                Address= FindAdmin.Users?.Address,
                BirthDay= FindAdmin.Users?.BithDay,
                RoleName= FindAdmin.Users?.Role?.RoleName,
                Persmision = FindAdmin.Users!.Permisson,
                IsActive= FindAdmin.Users.IsActive

            };
             
            return Ok(AdminDto);

        }


        [HttpPost("AddNewAdmin",Name ="AddNewAddmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult AddNewAdmin(UserWriteData userWrite)
        {
            if (userWrite == null) return BadRequest("Bad request : Invalid Data");

            var Admin = new ClsAdmins();
            Admin.Users = new ClsUsers(userWrite);

            if (!Admin.Users.Save()) return StatusCode(500, "Server : Error occurred on system");

            if (!Admin.Save()) return StatusCode(500, "Server : Error occurred on system");

            return StatusCode(200, $"Save Admin ID ({Admin.AdminID}) has been successed");
        }


        [HttpPut("UpdateAdmin{id}", Name = "UpdateAddmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult UpdateAdmin(int id ,UserWriteData userWrite)
        {
            if (id < 1) return StatusCode(400, "Bad Request : Invalid input");
            if (userWrite is not UserWriteData) return StatusCode(400, "Bad Request : Invalid Object");

            var FindAdmin = ClsAdmins.Find(id);

            if (FindAdmin == null) return StatusCode(404, "Not Found : Error occurred on system");

            FindAdmin.Users.UserName = userWrite.UserName;
            FindAdmin.Users.Password = userWrite.Password;
            FindAdmin.Users.RoleID = userWrite.RoleID;
            FindAdmin.Users.Permisson = userWrite.permission;
            FindAdmin.Users.IsActive = userWrite.IsActive;
            FindAdmin.Users.First_Name = userWrite.FirstName;
            FindAdmin.Users.Last_Name = userWrite.LastName;
            FindAdmin.Users.Email = userWrite.Email;
            FindAdmin.Users.Phone = userWrite.Phone;
            FindAdmin.Users.BithDay = userWrite.BirthDay;
            FindAdmin.Users.Address = userWrite.Addrees;

            if (!FindAdmin.Save()) return StatusCode(500, "Server : Error occured on system");


            return StatusCode(200, "Update Admin has been successed");
        }



        [HttpDelete("DeleteAdmin{id}", Name = "DeleteAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult DeleteAdmin(int id)
        {
            if (id < 1) return StatusCode(400, "Bad Request : Invalid input");

            if (!ClsAdmins.IsExistAdmin(id)) return StatusCode(404, "Not Found : admin is not exist");

            if (!ClsAdmins.DeleteAdmin(id)) return StatusCode(500, "Server : Error occurred on system");

            return StatusCode(200, $"Delete Admin ID '{id}' has been successed");
        }
    }
}
