using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// object Data Transfer Object for Role Request
    /// </summary>
    public sealed record RoleDtoRequest
    (
     int? RoleID,
     string? RoleName
    );
}