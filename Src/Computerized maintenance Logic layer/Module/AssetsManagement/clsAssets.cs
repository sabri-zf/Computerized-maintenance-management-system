using Computerized_maintenance_Logic_layer.Module.DTO;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Enumes;
using computrized_maintenance_Data_Access.Misc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    public class clsAssets
    {

        private  static AppDbContext? _Context;
        private readonly static clsAssets _Instance = new();

        public static clsAssets Instance
        {
            get 
            {
                _Context = ClsUtility.ImplementDbContextService();
                return _Instance; 
            }
        }
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

        //public clsAssets(AppDbContext dbContext)
        //{
        //    = dbContext;
        //}

        private clsAssets()
        {
            
        }

        private clsAssets(int id, string assetName, string assetTagNumber, string manufactuerName,
            string manufactuerModelNumber, DateTime purchaseDate, decimal purchaseCost,
            DateTime warrantyExpiryDate, DateTime? installationDate, int assetCategoryID, 
            int assetLocationID,Asset_Status_Type assetStatus, MeterReading meterReading,
            Criticality_Rating criticality, DateTime createAssetDate, 
            DateTime? updateAssetDate, int createByUser)
        {
            this.ID = id;
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

        /// <summary>
        /// Asynchronous Entity Asset From Data source 
        /// </summary>
        /// <returns> Return object , otherwise null</returns>
        public async Task<clsAssets?> FindAsync(int ID)
        {
            var Assets = await _Context.Assets
                                       .AsNoTracking()
                                       .SingleOrDefaultAsync(x => x.ID == ID);

            if(Assets is Asset)
            {
                return new clsAssets(Assets.ID, Assets.AssetName, Assets.AssetTagNumber, Assets.ManufactuerName
                    , Assets.ManufactuerModelNumber, Assets.PurchaseDate, Assets.PurchaseCost, Assets.WarrantyExpiryDate,
                    Assets.InstallationDate, Assets.AssetCategoryID, Assets.AssetLocationID, Assets.AssetStatus, Assets.MeterReading,
                    Assets.Criticality, Assets.CreateAssetDate, Assets.UpdateAssetDate, Assets.CreateByUser);
            }

            return null;
        }


        /// <summary>
        /// Retrieve Object's Assets from Data store 
        /// </summary>
        /// <param name="Name"> name of Asset you looking for </param>
        /// <returns>object <see cref="clsAssets"/> or null</returns>
        public  async Task<Assetdto?> FindByAssetNameAsync(string Name)
        {
            if(string.IsNullOrEmpty(Name)) return null;

            var Asset = await _Context.Assets
                                      .AsNoTracking()
                                      .Where(x => x.AssetName == Name)
                                      .Select(x => new Assetdto
                                      (
                                         x.AssetName,
                                         x.AssetTagNumber,
                                         x.ManufactuerName,
                                         x.ManufactuerModelNumber,
                                         x.PurchaseDate,
                                         x.PurchaseCost,
                                         x.WarrantyExpiryDate,
                                         x.InstallationDate,
                                         x.CreateAssetDate,
                                         x.CreateByUser
                                      )
                                      ).FirstOrDefaultAsync();

                                      

            return Asset;
        }

        /// <summary>
        ///  Find Entity Asset From Data source 
        /// </summary>
        /// <returns> Return object , otherwise null</returns>
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

        /// <summary>
        ///  Adding New Entity Asset On Data source
        /// </summary>
        /// <returns "name= bool"> Return True if Add Entity was successful, otherwise False </returns>
        public  bool AddNewAsset()
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

            return  _Context.SaveChanges() > 0;
        }

        /// <summary>
        /// Asynchronous Adding New Entity Asset On Data source
        /// </summary>
        /// <returns "name= bool"> Return True if Add Entity was successful, otherwise False </returns>
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

        /// <summary>
        /// Update Entity Asset On Data source
        /// </summary>
        /// <returns "name= bool"> Return True if Update successful, otherwise False </returns>
        public bool UpdateAsset()
        {
            return  _Context.Assets.Where(x => x.ID == this.ID)
               .ExecuteUpdate(A => A

               .SetProperty(p => p.AssetName, this.AssetName)
               .SetProperty(p => p.AssetTagNumber, this.AssetTagNumber)
               .SetProperty(p => p.ManufactuerName, this.ManufactuerName)
               .SetProperty(p => p.ManufactuerModelNumber, this.ManufactuerModelNumber)
               .SetProperty(p => p.PurchaseDate, this.PurchaseDate)
               .SetProperty(p => p.PurchaseCost, this.PurchaseCost)
               .SetProperty(p => p.WarrantyExpiryDate, this.WarrantyExpiryDate)
               .SetProperty(p => p.InstallationDate, this.InstallationDate)
               .SetProperty(p => p.AssetCategoryID, this.AssetCategoryID)
               .SetProperty(p => p.AssetLocationID, this.AssetLocationID)
               .SetProperty(p => p.AssetStatus, this.AssetStatus)
               .SetProperty(p => p.MeterReading, this.MeterReading)
               .SetProperty(p => p.Criticality, this.Criticality)
               .SetProperty(p => p.CreateAssetDate, this.CreateAssetDate)
               .SetProperty(p => p.CreateByUser, this.CreateByUser)

           ) > 0;
        }

        /// <summary>
        /// Asynchronous Update Entity Asset On Data source
        /// </summary>
        /// <returns "name= bool"> Return True if Update successful, otherwise False </returns>
        public async Task<bool> UpdateAssetAsync()
        {
           
            /*
             * High performence to Update entity Without unnecessary Tracking
             */

            return await _Context.Assets
                                 .Where(x => x.ID == this.ID)
                                 .ExecuteUpdateAsync(A => A

                                 .SetProperty(p => p.AssetName, this.AssetName)
                                 .SetProperty(p => p.AssetTagNumber, this.AssetTagNumber)
                                 .SetProperty(p => p.ManufactuerName, this.ManufactuerName)
                                 .SetProperty(p => p.ManufactuerModelNumber, this.ManufactuerModelNumber)
                                 .SetProperty(p => p.PurchaseDate, this.PurchaseDate)
                                 .SetProperty(p => p.PurchaseCost, this.PurchaseCost)
                                 .SetProperty(p => p.WarrantyExpiryDate, this.WarrantyExpiryDate)
                                 .SetProperty(p => p.InstallationDate, this.InstallationDate)
                                 .SetProperty(p => p.AssetCategoryID, this.AssetCategoryID)
                                 .SetProperty(p => p.AssetLocationID, this.AssetLocationID)
                                 .SetProperty(p => p.AssetStatus, this.AssetStatus)
                                 .SetProperty(p => p.MeterReading, this.MeterReading)
                                 .SetProperty(p => p.Criticality, this.Criticality)
                                 .SetProperty(p => p.CreateAssetDate, this.CreateAssetDate)
                                 .SetProperty(p => p.CreateByUser, this.CreateByUser)
                                 ) > 0;

        }

        /// <summary>
        /// Delete Asset On Data source
        /// </summary>
        /// <returns "name= bool"> Return True if Delete successful, otherwise False </returns>
        public bool DeleteAsset()
        {
            return _Context.Assets
                .Where(x => x.ID == this.ID)
                .ExecuteDelete() > 0;
        }

        /// <summary>
        /// Asynchronous Deleting Asset On Data source
        /// </summary>
        /// <returns "name= bool"> Return True if Delete successful, otherwise False </returns>
        public async static Task<bool> DeleteAssetAsync(int ID)
        {
            return await _Context.Assets
                                 .Where(x => x.ID == ID)
                                 .ExecuteDeleteAsync() > 0;
        }

        /// <summary>
        /// Get each Assets on Data source,
        /// </summary>
        /// <returns>Return :Iquerable Asset Dbset </returns>
        public async Task<IEnumerable<Asset>> GetAllAssets()
        {
            return await _Context.Assets
                           .AsNoTracking()
                           .ToListAsync();
        }


        ~clsAssets()
        {
            if( _Context != null )
             _Context.DisposeAsync();
        }

    }
}
