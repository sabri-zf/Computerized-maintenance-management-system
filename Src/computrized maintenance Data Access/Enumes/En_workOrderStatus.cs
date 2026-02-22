using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.Enumes
{
    public enum En_workOrderStatus
    {
        Open = 1,       //The work order is created but not yet started or assigned.
        Assigned,       //The work order has been assigned to a technician but work hasn’t started yet.
        In_progress,    //The technician is currently working on it.
        On_Hold,        //Work is temporarily stopped due to missing parts, waiting for approval, or other issues.
        Completed,      //The technician finished the work and updated the order.
        Closed,         //The manager or admin reviewed the work and confirmed completion — officially finished.
        Cancelled       //The work order was cancelled before completion.
    }
}
