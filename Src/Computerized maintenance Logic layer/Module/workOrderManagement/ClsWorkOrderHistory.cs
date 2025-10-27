using Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.workOrderManagement
{
    public class ClsWorkOrderHistory(AppDbContext _context)
    {




        public async Task<IEnumerable<WorkOrderHistoryResponseDto>?> GetAllHistory()
        {
            var List = await _context.WorkOrderHistories
                .AsNoTracking()
                .ToListAsync();

            if(List is null) return null;

            return List.Select(x => new WorkOrderHistoryResponseDto(x.WO_ID,x.Action.ToString(),x.ActionDate,x.PerformedActionByID)
            );
        }

        public async Task<bool> AddNewHistory(WorkOrderHistoryResponseDto workOrderHistory)
        {
            if (workOrderHistory is not WorkOrderHistoryResponseDto) return false;

            var workOrderHistoryEntity = new computrized_maintenance_Data_Access.Entites.WorkOrderManagement.WorkOrderHistory
            {
                WO_ID = workOrderHistory.WorkOrderID,
                Action =  Enum.Parse<WorkOrderHistoryActionStatus>(workOrderHistory.Action,true),
                ActionDate = workOrderHistory.ActionDate,
                PerformedActionByID = workOrderHistory.PerformedActionByID
            };
            await _context.WorkOrderHistories.AddAsync(workOrderHistoryEntity);

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<WorkOrderHistoryResponseDto?> FindByID(int Id)
        {
            if (Id < 1) return null;
            var Result = await _context.WorkOrderHistories
                                       .AsNoTracking()
                                       .SingleOrDefaultAsync(x => x.ID == Id);

            if (Result is not WorkOrderHistory) return null;
            return new WorkOrderHistoryResponseDto
                (
                Result.WO_ID,
                Result.Action.ToString(),
                Result.ActionDate,
                Result.PerformedActionByID
                );
        }

        public async Task<bool> UpdateHistory(WorkOrderHistoryRequestDto requestDto)
        {
            if (requestDto is not WorkOrderHistoryRequestDto && requestDto.ID < 1) return false;


            return await _context.WorkOrderHistories
                                 .Where(x => x.ID == requestDto.ID)
                                 .ExecuteUpdateAsync(setting => setting
                                 .SetProperty(x => x.WO_ID, requestDto.WorkOrderID)
                                 .SetProperty(x => x.Action, requestDto.Action)
                                 .SetProperty(x => x.ActionDate, requestDto.ActionDate)
                                 ) > 0;
        }


        public async Task<bool> DeleteHistory(int Id)
        {
            if (Id < 1) return false;
            return await _context.WorkOrderHistories
                                 .Where(x => x.ID == Id)
                                 .ExecuteDeleteAsync() > 0;
        }
    }
}
