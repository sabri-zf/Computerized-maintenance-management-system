using Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.workOrderManagement
{
    public class ClsWorkorders(AppDbContext _context)
    {



        public async Task<IEnumerable<WorkOredrResponseDto>?> GetAllWorkOrders()
        {
            var List = await _context.WorkOrders
                .AsNoTracking()
                .ToListAsync();

            if (List is null) return null;

            var workOrderDtos = List.Select(workOrder =>
                                            new WorkOredrResponseDto
                                         (
                                             workOrder.WorkOrderNumber,
                                             workOrder.Description,
                                             workOrder.AssetID,
                                             workOrder.Status.ToString(),
                                             workOrder.CreatedByID,
                                             workOrder.AssignedToID,
                                             workOrder.CreatedDate,
                                             workOrder.StartDate,
                                             workOrder.DueDate,
                                             workOrder.CompeletedDate,
                                             workOrder.Note
                                         ));
            return workOrderDtos;
        }
        public async Task<WorkOredrResponseDto?> FindByID(int workOrderId)
        {
            if (workOrderId < 1) return null;

            var workOrder = await _context.WorkOrders
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == workOrderId);

            if (workOrder == null)
            {
                return null;
            }

            return new WorkOredrResponseDto
            (
                workOrder.WorkOrderNumber,
                workOrder.Description,
                workOrder.AssetID,
                workOrder.Status.ToString(),
                workOrder.CreatedByID,
                workOrder.AssignedToID,
                workOrder.CreatedDate,
                workOrder.StartDate,
                workOrder.DueDate,
                workOrder.CompeletedDate,
                workOrder.Note
            );
        }

        public async Task<WorkOredrResponseDto?> FindByWorkorderNumber(string WorkOrderNumber)
        {
            if (string.IsNullOrEmpty(WorkOrderNumber) || string.IsNullOrWhiteSpace(WorkOrderNumber)) return null;

            var workOrder = await _context.WorkOrders
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.WorkOrderNumber == WorkOrderNumber);

            if (workOrder == null)
            {
                throw new KeyNotFoundException($"Work-order Number not found.");
            }
            return new WorkOredrResponseDto
            (
                workOrder.WorkOrderNumber,
                workOrder.Description,
                workOrder.AssetID,
                workOrder.Status.ToString(),
                workOrder.CreatedByID,
                workOrder.AssignedToID,
                workOrder.CreatedDate,
                workOrder.StartDate,
                workOrder.DueDate,
                workOrder.CompeletedDate,
                workOrder.Note
            );
        }


        public async Task<bool> AddNewWorkOrder(WorkOrder newWorkOrder)
        {
            if (newWorkOrder is not WorkOrder) return false;

            await _context.WorkOrders.AddAsync(newWorkOrder);

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateWorkOrder(WorkOredrRequestDto updatedWorkOrder)
        {
            if (updatedWorkOrder is not WorkOredrRequestDto) return false;
            return await _context.WorkOrders
                                 .Where(x => x.ID == updatedWorkOrder.ID)
                                 .ExecuteUpdateAsync(setting =>
                                  setting.SetProperty(x => x.WorkOrderNumber, updatedWorkOrder.WorkOrderNumber)
                                     .SetProperty(x => x.Description, updatedWorkOrder.Description)
                                     .SetProperty(x => x.AssignedToID, updatedWorkOrder.AssignedToID)
                                     .SetProperty(x => x.AssetID, updatedWorkOrder.AssetID)
                                     .SetProperty(x => x.Status, updatedWorkOrder.Status)
                                     .SetProperty(x => x.StartDate, updatedWorkOrder.StartDate)
                                     .SetProperty(x => x.DueDate, updatedWorkOrder.DueDate)
                                     .SetProperty(x => x.CompeletedDate, updatedWorkOrder.CompeletedDate)
                                     .SetProperty(x => x.Note, updatedWorkOrder.Note)) > 0;
        }


        public async Task<bool> DeleteWorkOrder(int workOrderId)
        {
            if (workOrderId < 1) return false;


            return await _context.WorkOrders
                                 .Where(x => x.ID == workOrderId)
                                 .ExecuteDeleteAsync() > 0;

        }
    }
}
