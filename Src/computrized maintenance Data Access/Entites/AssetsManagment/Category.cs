using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Entites.AssetsManagment
{
    public class Category
    {
        public int ID { get; set; }
        public string Category_Name { get; set; } = null!;
        public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public virtual Asset Asset { get; set; }= null!;
    }
}
