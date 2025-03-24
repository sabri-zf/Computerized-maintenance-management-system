using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.DTO
{
    public  class ManagerViewDto
    {

        public int? ManagerID { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public short? Permmission { get; set; }
        public string? DepartmentName { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive {  get; set; }

        public DateTime? CreateAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedByAdmin { get; set; }
        
     }
}
