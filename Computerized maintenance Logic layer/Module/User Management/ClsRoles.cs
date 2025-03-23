using computrized_maintenance_Data_Access.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsRoles
    {

        public int? RoleID { get; private set; }
        public string? RoleName { get;}

        public ClsRoles(RoleDto? dto)
        {
            this.RoleID = dto.RoleID;
            this.RoleName = dto.RoleName;
        }

    }
}
