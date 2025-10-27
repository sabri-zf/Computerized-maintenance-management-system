using Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.workOrderManagement
{
    public sealed class ClsWorkOrderParts(AppDbContext _context)
    {



        public async Task<WorkOrderPartResponseDto?> FindByID(int Id)
        {
            if (Id < 0) return null;

            var Result = await _context.workOrderParts
                                       .AsNoTracking()
                                       .SingleOrDefaultAsync(x => x.ID == Id);

            if(Result is not WorkOrderPart) return null;

            return new WorkOrderPartResponseDto
                (
                Result.WO_ID,
                Result.PartItemID,
                Result.QuantityUsed
                );
        }


        public async Task<IEnumerable<WorkOrderPartResponseDto>?> GetAllItems()
        {
            var List = await _context.workOrderParts
                                      .AsNoTracking()
                                      .ToListAsync();

            return List.Select(x => new WorkOrderPartResponseDto(x.WO_ID, x.PartItemID, x.QuantityUsed));
        }

        public async Task<bool> AddNewItem(WorkOrderPart WO_part)
        {
            if(WO_part is not WorkOrderPart) return false;

            await _context.workOrderParts.AddAsync(WO_part);

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> UpdateItem(WorkOrderPartRequestDto requestDto)
        {
            if (requestDto is not WorkOrderPartRequestDto && requestDto.ID < 1) return false;

            return await _context.workOrderParts
                                 .Where(x => x.ID == requestDto.ID)
                                 .ExecuteUpdateAsync(setting => setting
                                 .SetProperty(x => x.WO_ID, requestDto.WorkOrderID)
                                 .SetProperty(x => x.PartItemID, requestDto.PartItemID)
                                 .SetProperty(x => x.QuantityUsed, requestDto.QuantityUsed)
                                 ) > 0;
        }

        public async Task<bool> DeleteItem(int Id)
        {
            if (Id < 1) return false;

            return await _context.workOrderParts
                                 .Where(x => x.ID == Id)
                                 .ExecuteDeleteAsync() > 0;
        }

    }
}
