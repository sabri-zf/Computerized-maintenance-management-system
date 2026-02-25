namespace Computerized_maintenance_Logic_layer.Module.DTO
{
    public sealed record Assetdto
    (
     string AssetName,
     string AssetTagNumber,
     string ManufactuerName,
     string ManufactuerModelNumber,
     DateTime PurchaseDate,
     decimal PurchaseCost,
     DateTime WarrantyExpiryDate,
     DateTime? InstallationDate,
     DateTime CreateAssetDate,
     int CreateByUser
    );
}
