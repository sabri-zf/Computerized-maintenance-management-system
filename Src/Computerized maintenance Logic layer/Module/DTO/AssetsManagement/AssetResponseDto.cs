using computrized_maintenance_Data_Access.Enumes;

namespace CMMS_Api.DTO
{
    public record AssetResponseDto
    (
     string AssetName ,
     string AssetTagNumber ,
     string ManufactuerName,
     string ManufactuerModelNumber,
     DateTime PurchaseDate,
     decimal PurchaseCost ,
     DateTime WarrantyExpiryDate,
     DateTime? InstallationDate,
     int AssetCategoryID,
     int AssetLocationID,
     En_Asset_Status_Type AssetStatus,
     En_MeterReading MeterReading,
     En_Criticality_Rating Criticality,
     DateTime CreateAssetDate,
     DateTime? UpdateAssetDate,
     int CreateByUser
    );
}
