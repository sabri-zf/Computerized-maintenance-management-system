using Computerized_maintenance_Logic_layer.Module.User_Management;
using computrized_maintenance_Data_Access.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.UsersManagment
{
    [Route("api/v1/tehnicians")]
    [ApiController]
    public class TechnicionController(ClsTechnicians _instance) : Controller
    {
        [HttpGet("retrieve", Name = "retrieve_all_tehnician")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllTechnicion()
        {
            try
            {
                var ListOfUsers = await _instance.RetrieveAllTechincinAsync();

                if (ListOfUsers is null)
                    return NotFound("Not Found : Data of techincin Not Found");

                return Ok(ListOfUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }
        }

        [HttpGet("retrieve-one/{id}", Name = "get_one_tehnician")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserByID(int id)
        {
            if (id < 1) return BadRequest($"Error : Invlid ID ({id})");

            try
            {
                //if (!await _instance.IsExistUserAsync(id)) return StatusCode(500, "Server : User Doesn't Exist");

                var FindUser = await _instance.FindAsync(id);

                if (FindUser is not TechnicianDtoRepose)
                {
                    return NotFound("Not Found : Error Occurred invalid techincin");
                }

                return Ok(FindUser);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }
        }

        [HttpPost("create", Name = "add_new_tehnician")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddNewTehnician(ProcessToCreateTechnicianDto resquest)
        {
            if (resquest is not ProcessToCreateTechnicianDto) return BadRequest("Bad Request : Invalid operation");
           
            try
            {
                var IsInseted = await _instance.AddNewTehnicianAsync(resquest);

                if (!IsInseted)
                {
                    return StatusCode(500, "Server : Error has been occurred ,Technician Not Saved");
                }

                return Ok($"Add new Technician Hass been Successful ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }
        }

        [HttpPut("edit/{id}", Name = "update_tehnician")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTehnician(ProcessToModifyTechnicianDto resquest)
        {
            if (resquest.TechnicianID < 1 || resquest is not ProcessToModifyTechnicianDto) return BadRequest("Bad a request : Invalid Operation");

            try
            {
                var FindUser = await _instance.UpdateTehnicianAsync(resquest);

                if (!FindUser)
                {
                    return StatusCode(500, "Server : Error has been occurred ,tehnician doesn't Save");
                }

                return Ok($"Update a tehnician Has been succeed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }

        }

        [HttpDelete("omit/{id}", Name = "delete_tehnician0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTehnician(int id)
        {
            if (id < 1) return BadRequest($"Bad Requst : Invalid operation");

            try
            {
                //if (!await _instance.IsExistUserAsync(id))
                //{
                //    return NotFound("Not Found : User doesn't find");
                //}

                if (!await _instance.DeleteTechincianAsync(id))
                {
                    return StatusCode(500, "Server : Error has been occurred");
                }

                return Ok($"Delete a tehnician Has been succeed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error : {ex.Message}");
            }

        }
    }
}
