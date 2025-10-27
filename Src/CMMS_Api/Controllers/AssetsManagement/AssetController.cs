using CMMS_Api.DTO;
using Computerized_maintenance_Logic_layer.Module.AssetsManagement;
using Computerized_maintenance_Logic_layer.Module.DTO;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.AspNetCore.Mvc;

namespace CMMS_Api.Controllers.AssetsManagementController
{
    [ApiController]
    [Route("Api/V1/asset")]
    public class AssetController:Controller
    {


        [HttpGet("get-assets",Name ="get-assets")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Asset>> GetAll()
        {
            var List_Assets = await clsAssets.Instance.GetAllAssets();

            if(List_Assets is null) return NotFound("Data Not Found");


            return Ok(List_Assets);
        }

        [HttpGet("get-asset{asset_name:alpha}",Name ="asset")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Assetdto>> GetAssetByName(string asset_name)
        {
            if (string.IsNullOrEmpty(asset_name)) return BadRequest("Invalid Operation");

            var Output = await clsAssets.Instance.FindByAssetNameAsync(asset_name);

            if (Output is null) return NotFound("Asset doesn't find");


            return Ok(Output);
        }


        [HttpGet("get-asset/{id:int}", Name = "asset-id")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Assetdto>> GetAssetById(int id)
        {
            if (id < 1) return BadRequest("invalid operation");

            var Result = await clsAssets.Instance.FindAsync(id);

            if (Result is null) return NotFound("Asset doesn't find");

            var Dto = new Assetdto(Result.AssetName,Result.AssetTagNumber,Result.ManufactuerName,
                Result.ManufactuerModelNumber,Result.PurchaseDate,Result.PurchaseCost,Result.WarrantyExpiryDate,
                Result.InstallationDate,Result.CreateAssetDate,Result.CreateByUser);

            return Ok(Dto);
        }

        [HttpPost("add-new-asset",Name ="add-asset")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> postAsset(AssetDtoResponse ResponseDto)
        {

            if (ResponseDto is null) return BadRequest("invalid input");

            clsAssets asset = clsAssets.Instance;

            asset.AssetName = ResponseDto.AssetName;
            asset.AssetTagNumber = ResponseDto.AssetTagNumber;
            asset.ManufactuerName = ResponseDto.ManufactuerName;
            asset.ManufactuerModelNumber = ResponseDto.ManufactuerModelNumber;
            asset.PurchaseDate = ResponseDto.PurchaseDate;
            asset.PurchaseCost = ResponseDto.PurchaseCost;
            asset.WarrantyExpiryDate = ResponseDto.WarrantyExpiryDate;
            asset.InstallationDate = ResponseDto.InstallationDate;
            asset.AssetCategoryID = ResponseDto.AssetCategoryID;
            asset.AssetLocationID = ResponseDto.AssetLocationID;
            asset.AssetStatus = ResponseDto.AssetStatus;
            asset.MeterReading = ResponseDto.MeterReading;
            asset.Criticality = ResponseDto.Criticality;
            asset.CreateAssetDate = ResponseDto.CreateAssetDate;
            asset.UpdateAssetDate = ResponseDto.UpdateAssetDate;
            asset.CreateByUser= ResponseDto.CreateByUser;


            if (! await asset.AddNewAssetAsync())
            {
                return  StatusCode(500,"Erorr Occurred On System");
            }



            return Created("get-asset/asset_name", asset);
        }


        [HttpPut("edit-Asset",Name ="edit-asset")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> editAsset(AssetDroRequest requestDto)
        {
            if (requestDto is null || requestDto.ID < 1) return BadRequest("Invalid Operation");

            var find_out = await clsAssets.Instance.FindAsync(requestDto.ID);

            if(find_out is null)
            {
                return NotFound($"Asset not Found by This ({requestDto.ID}) ID");
            }

            find_out.AssetName = requestDto.AssetName;
            find_out.AssetTagNumber = requestDto.AssetTagNumber;
            find_out.ManufactuerName = requestDto.ManufactuerName;
            find_out.ManufactuerModelNumber = requestDto.ManufactuerModelNumber;
            find_out.PurchaseDate = requestDto.PurchaseDate;
            find_out.PurchaseCost = requestDto.PurchaseCost;
            find_out.WarrantyExpiryDate = requestDto.WarrantyExpiryDate;
            find_out.InstallationDate = requestDto.InstallationDate;
            find_out.AssetCategoryID = requestDto.AssetCategoryID;
            find_out.AssetLocationID = requestDto.AssetLocationID;
            find_out.AssetStatus = requestDto.AssetStatus;
            find_out.MeterReading = requestDto.MeterReading;
            find_out.Criticality = requestDto.Criticality;
            find_out.CreateAssetDate = requestDto.CreateAssetDate;
            find_out.UpdateAssetDate = requestDto.UpdateAssetDate;
            find_out.CreateByUser = requestDto.CreateByUser;

            if(await find_out.UpdateAssetAsync())
            {

            return Ok("Update asset has been done");
            }

            return StatusCode(500, "Error Occurred on system");
        }


        [HttpDelete("delet-asset/{ID:int}",Name ="delete-asset")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeletAsset(int ID)
        {
            if (ID < 1) return BadRequest("Invalid Operation");

            var IsDeleted = await clsAssets.DeleteAssetAsync(ID);

            if (!IsDeleted)
            {
                return StatusCode(500, "Error Occurred on system");
            }

            return Ok("Delete Asset Has been Done");
        }

    }
}
