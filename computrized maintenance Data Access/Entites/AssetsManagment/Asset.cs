using computrized_maintenance_Data_Access.Enumes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Entites.AssetsManagment
{
    public class Asset
    {
        public  int ID { get; set; }
        public  string AssetName { get; set; } = null!;
        public  string AssetTagNumber { get; set; } = null!;
        public  string ManufactuerName { get; set; } = null!;
        public  string ManufactuerModelNumber { get; set; } = null!;
        public  DateTime PurchaseDate { get; set; } // date of Purachase 
        public decimal PurchaseCost { get;set; }
        public DateTime WarrantyExpiryDate { get; set; } // 10-10-2035
        public DateTime? InstallationDate { get; set; }
        public int AssetCategoryID { get; set; }
        public int AssetLocationID { get; set; }
        public Asset_Status_Type AssetStatus {  get; set; }
        public MeterReading MeterReading {  get; set; }
        public Criticality_Rating Criticality { get; set; }
        public DateTime CreateAssetDate { get; set; }
        public DateTime? UpdateAssetDate { get; set; }
        public int CreateByUser {  get; set; }

        // RelationShip Details 
        public virtual Location Location { get; set; } = null!;
        public virtual Category Category { get; set; }= null!;
        public virtual ICollection<AssetImage> AssetImages { get; set; }= new List<AssetImage>();
    }
}
