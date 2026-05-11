using Computerized_maintenance_Logic_layer.Module.DTO.WorkOrderDto;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Computerized_maintenance_Logic_layer.Module.workOrderManagement
{
    public class ClsWorkorders(AppDbContext _context)
    {



        public async Task<IEnumerable<WorkOredrResponseDto>?> GetAllWorkOrders()
        {
            var List = await _context.WorkOrders
                .AsNoTracking()
                .Select(workOrder =>
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
                     )).ToListAsync();
                

            if (List is null) return null;


            return List;
        }


        
        public async Task<IEnumerable<WorkOredrList_view>?> GetByPage(short page)
        {

            ICollection<WorkOredrList_view>? list = new List<WorkOredrList_view>();
           
            var connection = _context.Database.GetDbConnection();
            try
            {

                await connection.OpenAsync();

                var query = "exec sp_Retrieve_workOrders @PageNumber";
                //var PageNumParam = new DbParameter();

                var command = connection.CreateCommand();

                command.CommandText = query;
                var PageNumParam = command.CreateParameter();
                PageNumParam.Value = page;
                PageNumParam.DbType = DbType.Int16;
                PageNumParam.ParameterName = "@PageNumber";
                command.Parameters.Add(PageNumParam);

                var reader = await command.ExecuteReaderAsync();


                while ( await reader.ReadAsync())
                {
                   var value = new WorkOredrList_view(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)
                       , reader.GetString(5), reader.GetString(6), reader.GetDateTime(7), reader.GetDateTime(8), reader.GetString(9));

                    list.Add(value);
                }

            } catch (SqlException sqlex) {
                Console.WriteLine(sqlex.Message);
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }


           

            return list;    
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
