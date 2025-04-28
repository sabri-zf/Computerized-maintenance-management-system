using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    public class clsAssets
    {

        private readonly AppDbContext _Context;

        public int ID { get; set; }
        public string AssetName { get; set; } = null!;
        public string AssetTagNumber { get; set; } = null!;
        public string ManufactuerName { get; set; } = null!;
        public string ManufactuerModelNumber { get; set; } = null!;
        public DateTime PurchaseDate { get; set; } 
        public decimal PurchaseCost { get; set; }
        public DateTime WarrantyExpiryDate { get; set; } 
        public DateTime? InstallationDate { get; set; }
        public int AssetCategoryID { get; set; }
        public int AssetLocationID { get; set; }
        public Asset_Status_Type AssetStatus { get; set; }
        public MeterReading MeterReading { get; set; }
        public Criticality_Rating Criticality { get; set; }
        public DateTime CreateAssetDate { get; set; }
        public DateTime? UpdateAssetDate { get; set; }
        public int CreateByUser { get; set; }

        public clsAssets(AppDbContext dbContext)
        {
            this._Context = dbContext;
        }

        private clsAssets(int iD, string assetName, string assetTagNumber, string manufactuerName,
            string manufactuerModelNumber, DateTime purchaseDate, decimal purchaseCost,
            DateTime warrantyExpiryDate, DateTime? installationDate, int assetCategoryID, 
            int assetLocationID,Asset_Status_Type assetStatus, MeterReading meterReading,
            Criticality_Rating criticality, DateTime createAssetDate, 
            DateTime? updateAssetDate, int createByUser)
        {
            this.ID = iD;
            this.AssetName = assetName;
            this.AssetTagNumber = assetTagNumber;
            this.ManufactuerName = manufactuerName;
            this.ManufactuerModelNumber = manufactuerModelNumber;
            this.PurchaseDate = purchaseDate;
            this.PurchaseCost = purchaseCost;
            this.WarrantyExpiryDate = warrantyExpiryDate;
            this.InstallationDate = installationDate;
            this.AssetCategoryID = assetCategoryID;
            this.AssetLocationID = assetLocationID;
            this.AssetStatus = assetStatus;
            this.MeterReading = meterReading;
            this.Criticality = criticality;
            this.CreateAssetDate = createAssetDate;
            this.UpdateAssetDate = updateAssetDate;
            this.CreateByUser = createByUser;
        }

        public async Task<clsAssets?> FindAsync(int ID)
        {
            var Assets = await _Context.Assets.AsNoTracking().FirstOrDefaultAsync(x => x.ID == ID);

            if(Assets is Asset)
            {
                return new clsAssets(Assets.ID, Assets.AssetName, Assets.AssetTagNumber, Assets.ManufactuerName
                    , Assets.ManufactuerModelNumber, Assets.PurchaseDate, Assets.PurchaseCost, Assets.WarrantyExpiryDate,
                    Assets.InstallationDate, Assets.AssetCategoryID, Assets.AssetLocationID, Assets.AssetStatus, Assets.MeterReading,
                    Assets.Criticality, Assets.CreateAssetDate, Assets.UpdateAssetDate, Assets.CreateByUser);
            }

            return null;
        }

        public clsAssets? Find(int ID)
        {
            var Assets =  _Context.Assets.AsNoTracking().FirstOrDefault(x => x.ID == ID);

            if (Assets is Asset)
            {
                return new clsAssets(Assets.ID, Assets.AssetName, Assets.AssetTagNumber, Assets.ManufactuerName
                    , Assets.ManufactuerModelNumber, Assets.PurchaseDate, Assets.PurchaseCost, Assets.WarrantyExpiryDate,
                    Assets.InstallationDate, Assets.AssetCategoryID, Assets.AssetLocationID, Assets.AssetStatus, Assets.MeterReading,
                    Assets.Criticality, Assets.CreateAssetDate, Assets.UpdateAssetDate, Assets.CreateByUser);
            }

            return null;
        }

        public bool AddNewAsset()
        {
            Asset asset = new()
            {
                ID = this.ID,
                AssetName = this.AssetName,
                AssetTagNumber = this.AssetTagNumber,
                ManufactuerName = this.ManufactuerName,
                ManufactuerModelNumber = this.ManufactuerModelNumber,
                PurchaseDate = this.PurchaseDate,
                PurchaseCost = this.PurchaseCost,
                WarrantyExpiryDate = this.WarrantyExpiryDate,
                InstallationDate = this.InstallationDate,
                AssetCategoryID = this.AssetCategoryID,
                AssetLocationID = this.AssetLocationID,
                AssetStatus = this.AssetStatus,
                MeterReading = this.MeterReading,
                Criticality = this.Criticality,
                CreateAssetDate = this.CreateAssetDate,
                CreateByUser = this.CreateByUser
            };

            _Context.Assets.Add(asset);

            return _Context.SaveChanges() > 0;
        }

        public async Task<bool> AddNewAssetAsync()
        {
            Asset asset = new()
            {
                ID = this.ID,
                AssetName = this.AssetName,
                AssetTagNumber = this.AssetTagNumber,
                ManufactuerName = this.ManufactuerName,
                ManufactuerModelNumber = this.ManufactuerModelNumber,
                PurchaseDate = this.PurchaseDate,
                PurchaseCost = this.PurchaseCost,
                WarrantyExpiryDate = this.WarrantyExpiryDate,
                InstallationDate = this.InstallationDate,
                AssetCategoryID = this.AssetCategoryID,
                AssetLocationID = this.AssetLocationID,
                AssetStatus = this.AssetStatus,
                MeterReading = this.MeterReading,
                Criticality = this.Criticality,
                CreateAssetDate = this.CreateAssetDate,
                CreateByUser = this.CreateByUser
            };

           await _Context.Assets.AddAsync(asset);

            return await _Context.SaveChangesAsync() > 0;
        }

        public bool UpdateAsset()
        {
            Asset asset = new()
            {
                ID = this.ID,
                AssetName = this.AssetName,
                AssetTagNumber = this.AssetTagNumber,
                ManufactuerName = this.ManufactuerName,
                ManufactuerModelNumber = this.ManufactuerModelNumber,
                PurchaseDate = this.PurchaseDate,
                PurchaseCost = this.PurchaseCost,
                WarrantyExpiryDate = this.WarrantyExpiryDate,
                InstallationDate = this.InstallationDate,
                AssetCategoryID = this.AssetCategoryID,
                AssetLocationID = this.AssetLocationID,
                AssetStatus = this.AssetStatus,
                MeterReading = this.MeterReading,
                Criticality = this.Criticality,
                CreateAssetDate = this.CreateAssetDate,
                CreateByUser = this.CreateByUser
            };

            _Context.Assets.Update(asset);


            return _Context.SaveChanges() > 0;
        }

        public async Task<bool> UpdateAssetAsync()
        {
            Asset asset = new()
            {
                ID = this.ID,
                AssetName = this.AssetName,
                AssetTagNumber = this.AssetTagNumber,
                ManufactuerName = this.ManufactuerName,
                ManufactuerModelNumber = this.ManufactuerModelNumber,
                PurchaseDate = this.PurchaseDate,
                PurchaseCost = this.PurchaseCost,
                WarrantyExpiryDate = this.WarrantyExpiryDate,
                InstallationDate = this.InstallationDate,
                AssetCategoryID = this.AssetCategoryID,
                AssetLocationID = this.AssetLocationID,
                AssetStatus = this.AssetStatus,
                MeterReading = this.MeterReading,
                Criticality = this.Criticality,
                CreateAssetDate = this.CreateAssetDate,
                CreateByUser = this.CreateByUser
            };

            _Context.Assets.Update(asset);


            return  await _Context.SaveChangesAsync() > 0;
        }


        public bool DeleteAsset()
        {
            Asset asset = new()
            { ID = this.ID };

            _Context.Assets.Remove(asset);

            return _Context.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAssetAsync()
        {
            Asset asset = new()
            { ID = this.ID };

            _Context.Assets.Remove(asset);

            return await _Context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Get each Assets on Data source,
        /// </summary>
        /// <returns>Return :Iquerable Asset Dbset </returns>
        public IQueryable<Asset> GetAllAssets()
        {
            return _Context.Assets.AsNoTracking();
        }
            

    }
}
