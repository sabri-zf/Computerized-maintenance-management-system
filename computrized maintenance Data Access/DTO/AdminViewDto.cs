using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.DTO
{
    public class AdminViewDto
    {
        public int AdminID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Addrees { get; set; }
        public DateTime BirthDay { get; set; }
        public string RoleName { get; set; }
        public short Persmision {  get; set; }
        public bool IsActive { get; set; }
    }
}
