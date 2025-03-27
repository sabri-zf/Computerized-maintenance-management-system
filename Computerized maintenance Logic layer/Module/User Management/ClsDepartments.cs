using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsDepartments
    {
        public int? DepartmentID { get; private set; }
        public string? DepartmentName { get;set; }


        private ClsDepartments(DepartmentDto dto)
        {
            this.DepartmentID = dto.DepartmentID;
            this.DepartmentName = dto.DepartmentName;
        }

        public static ClsDepartments? Find(int? ID)
        {
            DepartmentDto dto = new DepartmentDto();

            if (DataAccesDepartment.Find(ID, ref dto))
            {
                return new ClsDepartments(dto);
            }
            return null;
        }
        public static string? GetDepartmentName(int? departmentID)
        {
            return DataAccesDepartment.GetDepartmentName(departmentID);
        } 

        public IEnumerable<DepartmentDto>? GetAll()
        {
            return DataAccesDepartment.GetAll();
        }
    }
}
