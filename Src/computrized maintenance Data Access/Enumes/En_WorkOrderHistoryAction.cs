using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Enumes
{
    public enum En_WorkOrderHistoryActionStatus
    {
        Reopend = 1, //Work order reopened after review
        Created,     // Work order was made in the system
        Assgined,    // Manager assigned it to a technician
        Started,     // Technician began the work
        Completed,   // Technician marked the task as done
        Closed,      // Manager approved and closed it
        Cancelled   //Work order cancelled before completion
    }
}
