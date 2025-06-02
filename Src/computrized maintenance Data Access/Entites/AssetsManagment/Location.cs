using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Entites.AssetsManagment
{
    public  class Location
    {
        public int ID {  get; set; }
        public string LocationName { get; set; } = null!;
        public Asset Asset { get; set; } = null!;
    }
}
