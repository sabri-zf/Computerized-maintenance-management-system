using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.DTO.DtoWrite
{
    public class UserWriteData
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Addrees { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public int? RoleID { get; set; }
        public short permission { get; set; }
        public bool IsActive { get; set; }
        public DateTime createAt { get; set; } = DateTime.UtcNow;
    }
}
