using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsDepartments
    {
        public int? DepartmentID { get; private set; }
        public string? DepartmentName { get;set; }


        private ClsDepartments(DepartmentTableDto dto)
        {
            this.DepartmentID = dto.DepartmentID;
            this.DepartmentName = dto.DepartmentName;
        }

        public static ClsDepartments? Find(int? ID)
        {
            DepartmentTableDto dto = new DepartmentTableDto();

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

        public IEnumerable<DepartmentTableDto>? GetAll()
        {
            return DataAccesDepartment.GetAll();
        }
    }
}
