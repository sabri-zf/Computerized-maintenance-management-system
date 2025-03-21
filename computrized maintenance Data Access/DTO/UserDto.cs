using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.DTO
{
    public class UserDto
    {

        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? personID { get; set; }
        public int? RoleID { get; set; }
        public short permission {  get; set; }
        public bool IsActive { get; set; }
        public DateTime createAt { get; set; }
    }
}
