using Computerized_maintenance_Logic_layer.Module.DTO.InventoryDto;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.InventoryManagement
{
    public sealed class ClsInventoryTransactions(AppDbContext _context)
    {


        /// <summary>
        /// Retrieve Whole Inventory Transaction events
        /// </summary>
        /// <returns> <see cref="IEnumerable{InventoryTrasactionsResponseDto}"/> has List of Transaction Data, otherwise return <see langword="null"/> </returns>
        public async Task<IEnumerable<InventoryTrasactionsResponseDto>?> GetAllTransaction()
        {
            var List = await _context.InventoryTransactions
                               .AsNoTracking()
                               .ToListAsync();

            if (List.Count < 0 || List is not IEnumerable<InventoryTrasactionsResponseDto>) return null;

            return List.Select(x => new InventoryTrasactionsResponseDto(x.InventoryItemID, x.Type.ToString(), x.Quntity, x.TransactionDate, x.Reference));
        }

        /// <summary>
        /// Get Data Transfaer Object Of Transaction 
        /// </summary>
        /// <param name="Id">Unique Identifier of Inventory Transaction</param>
        /// <returns>Record <see cref="InventoryTrasactionsResponseDto"/> , otherwise return <see langword="null"/></returns>
        public async Task<InventoryTrasactionsResponseDto?> GetTransactionById(int Id)
        {
            if (Id < 1) return null;

            var Response_obj = await _context.InventoryTransactions
                                             .AsNoTracking()
                                             .SingleOrDefaultAsync(x => x.ID == Id);

            if (Response_obj is not InventoryTransaction) return null;

            return new InventoryTrasactionsResponseDto(Response_obj.InventoryItemID, Response_obj.Type.ToString(), Response_obj.Quntity, Response_obj.TransactionDate, Response_obj.Reference);
        }


        /// <summary>
        /// Insert new object of Inventory Transaction 
        /// </summary>
        /// <param name="responseDto">Data Transfer object <see cref="InventoryTrasactionsResponseDto"/></param>
        /// <returns><see langword="true"/> if Add Successfully,otherwise <see  langword="false"/></returns>
        public async Task<bool> AddNewTransaction(InventoryTrasactionsResponseDto responseDto)
        {
            if (!CheckOutValidationOfInput(responseDto)) return false;

            InventoryTransaction TransactionEntity = new()
            {
                InventoryItemID = responseDto.InventoryItemID,
                Quntity = responseDto.Quntity,
                Type = (En_TransactionType)Enum.Parse(typeof(En_TransactionType), responseDto.Type),
                TransactionDate = responseDto.TransactionDate,
                Reference = responseDto.Reference
            };


            await _context.InventoryTransactions
                          .AddAsync(TransactionEntity);


            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Update Inventory Transaction Entity 
        /// </summary>
        /// <param name="requestDto">Data Transfer object <see cref="InventoryTrasactionsRequestDto"/> represent requset data form client </param>
        /// <returns><see langword="true"/> if Update has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateTransaction(InventoryTrasactionsRequestDto requestDto)
        {
            InventoryTrasactionsResponseDto ResponseDto = new
                (requestDto.InventoryItemID, requestDto.Type.ToString(), requestDto.Quntity, requestDto.TransactionDate, requestDto.Reference);
            if (!CheckOutValidationOfInput(ResponseDto, true, requestDto.ID)) return false;

            return await _context.InventoryTransactions
                           .Where(x => x.ID == requestDto.ID)
                           .ExecuteUpdateAsync(setting => setting
                           .SetProperty(x => x.InventoryItemID, requestDto.InventoryItemID)
                           .SetProperty(x => x.Type, (En_TransactionType)Enum.Parse(typeof(En_TransactionType), requestDto.Type))
                           .SetProperty(x => x.Quntity, requestDto.Quntity)
                           .SetProperty(x => x.TransactionDate, requestDto.TransactionDate)
                           .SetProperty(x => x.Reference, requestDto.Reference)
                           ) > 0;
        }

        /// <summary>
        /// Delete inventory Transaction <see cref="InventoryTransaction" />
        /// </summary>
        /// <param name="Id">Unique identifier of <see cref="InventoryTransaction"/></param>
        /// <returns><see langword="true"/> If delete has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> DeleteTransaction(int Id)
        {
            if (Id < 1) return false;

            return await _context.InventoryTransactions
                                 .Where(x => x.ID == Id)
                                 .ExecuteDeleteAsync() > 0;
        }

        /// <summary>
        /// check-out If the data is valid or not when send request or receive response
        /// </summary>
        /// <param name="responseDto">Data transfer object <see cref="InventoryTrasactionsResponseDto"/></param>
        /// <param name="IsRequset">check-in if the state is OnRequest or No</param>
        /// <param name="Id">Unique identifier of <see cref="InventoryTransaction"/></param>
        /// <returns><see langword="true"/> If data is valid, otherwise <see langword="false"/></returns>
        private bool CheckOutValidationOfInput(InventoryTrasactionsResponseDto responseDto, bool IsRequset = false, int Id = 0)
        {

            if (IsRequset)
            {
                if (Id < 1) return false;
            }

            if (responseDto is not InventoryTrasactionsResponseDto) return false;
            if (responseDto.InventoryItemID < 1) return false;
            if (responseDto.Quntity < 0) return false;
            if (!DateTime.TryParse(responseDto.TransactionDate.ToShortDateString(), out var value)) return false;
            if (string.IsNullOrEmpty(responseDto.Reference)) return false;

            return true;
        }


    }
}
