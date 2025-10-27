using computrized_maintenance_Data_Access.Enumes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto
{
    public sealed record WorkOrderHistoryResponseDto
   (
        int WorkOrderID,
        string Action,
        DateTime ActionDate,
        int PerformedActionByID
        );
}
