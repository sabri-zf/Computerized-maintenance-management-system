using computrized_maintenance_Data_Access.Enumes;

namespace CMMS_Api.DTO
{
    public sealed record AssetDroRequest
     (
     int ID,
     string AssetName,
     string AssetTagNumber,
     string ManufactuerName,
     string ManufactuerModelNumber,
     DateTime PurchaseDate,
     decimal PurchaseCost,
     DateTime WarrantyExpiryDate,
     DateTime? InstallationDate,
     int AssetCategoryID,
     int AssetLocationID,
     Asset_Status_Type AssetStatus,
     MeterReading MeterReading,
     Criticality_Rating Criticality,
     DateTime CreateAssetDate,
     DateTime? UpdateAssetDate,
     int CreateByUser
    );
}
