using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.DTO
{
    public class TechnicianDto
    {
        public int TechnicianID { get; private set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public int ManagedBy { get; set; }
        public int CreatedByAdmin { get; set; }
    }
}
