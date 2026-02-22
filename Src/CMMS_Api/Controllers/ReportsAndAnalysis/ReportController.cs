using Computerized_maintenance_Logic_layer.Module.DTO.ReportsAndAnalysis;
using Computerized_maintenance_Logic_layer.Module.ReportsAndAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.ReportsAndAnalysis
{
    [ApiController]
    [Route("api/v1/reports")]
    public class ReportController(ClsReports _instance) : Controller
    {

        [HttpGet("retrive-one/{asset_id:int}", Name = "retrive-one-reoprt")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> getReportByAssetId(int asset_id)
        {
            if (asset_id < 1) return BadRequest("Invalid operation :(");

            try
            {
                var LookFor = await _instance.FindAsync(asset_id);

                if (LookFor is not ReportEquipmentDto) return NotFound("Data doesn't find :(");

                return Ok(LookFor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erorr ouccurred on internal server,\n Reason:{ex.Message}");
            }
        }
    }
}
