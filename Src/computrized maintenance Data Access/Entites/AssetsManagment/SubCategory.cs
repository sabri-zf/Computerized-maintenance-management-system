using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Entites.AssetsManagment
{
    public  class SubCategory
    {
        public int ID { get; set; }
        public string Sub_Category_Name { get; set; } = null!;
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; } = null!;
    }
}
