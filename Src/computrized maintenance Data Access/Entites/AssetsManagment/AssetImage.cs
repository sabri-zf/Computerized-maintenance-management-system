using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Entites.AssetsManagment
{
    public class AssetImage
    {
        public int ID { get; set; }
        public string ImagePath { get; set; }= null!; 
        public short ImageWidth { get; set; }
        public short ImageHeight { get; set; }
        public int AssetID { get; set; }
        public Asset Asset { get; set; } = null!;

    }
}
