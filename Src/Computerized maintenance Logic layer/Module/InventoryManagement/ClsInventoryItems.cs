using Computerized_maintenance_Logic_layer.Module.DTO.InventoryDto;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.InventoryManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.InventoryManagement
{
    public sealed class ClsInventoryItems(AppDbContext _Context)
    {


        /// <summary>
        /// Retrieve List Of Part Items 
        /// </summary>
        /// <returns>List of <see cref="IEnumerable<InventoryItemResponseDto>" if find it, otherwise <see cref="Nullable"/>NUll</see> </returns>
        public async Task<IEnumerable<InventoryItemResponseDto>?> GetAllInventoryItems()
        {
            var List = await _Context.inventoryItems
                                     .AsNoTracking()
                                     .ToListAsync();

            if (List.Count < 0 || List is not IEnumerable<InventoryItem>) return null;

            return List.Select(x => new InventoryItemResponseDto
                              (x.ItemName, x.PartNumber, x.Description, x.Quintity, x.ReorderLevel, x.UnitCost, x.LocationID, x.IsActive));
        }

        /// <summary>
        /// Retrieve <see cref="InventoryItemResponseDto"/> as Response Data from server
        /// </summary>
        /// <param name="ID">Unique identifier of PartItem </param>
        /// <returns>Object <see cref="InventoryItemResponseDto"/> if exist, otherwise null</returns>
        public async Task<InventoryItemResponseDto?> GetById(int ID)
        {
            if (ID < 1) return null;

            var Result = await _Context.inventoryItems
                                       .AsNoTracking() 
                                       .SingleOrDefaultAsync(x => x.ID == ID);

            if(Result is not InventoryItem) return null;

            

            return new InventoryItemResponseDto 
                   (
                       Result.ItemName,
                       Result.PartNumber,
                       Result.Description,
                       Result.Quintity,
                       Result.ReorderLevel,
                       Result.UnitCost,
                       Result.LocationID,
                       Result.IsActive
                   );
        }

        /// <summary>
        /// Insert new item into dataset of <see cref="InventoryItem"/>s
        /// </summary>
        /// <param name="responseDto">data transfer object <see cref="InventoryItemResponseDto"/> contain a necessary data to save it on system</param>
        /// <returns><see langword="true"/> if Save has been Succeed, otherwise <see langword="false"/> </returns>
        public async Task<bool> AddNewInventoryItem(InventoryItemResponseDto responseDto)

        {
            if(!CheckOutValidateInput(responseDto)) return false;
            //manual mapping
            var ItemEntity = new InventoryItem()
            {
                ItemName = responseDto.ItemName,
                PartNumber = responseDto.PartNumber,
                Description = responseDto.Description,
                Quintity = responseDto.Quintity,
                ReorderLevel = responseDto.ReorderLevel,
                UnitCost = responseDto.UnitCost,
                LocationID = responseDto.LocationID,
                IsActive = responseDto.IsActive
            };


            var Is_Inserted = await _Context.inventoryItems
                                            .AddAsync(ItemEntity);

            return await _Context.SaveChangesAsync() > 0;

        }


        /// <summary>
        /// Update Part-Item into dataset of <see cref="InventoryItem"/>s
        /// </summary>
        /// <param name="requestDto">data transfer object <see cref="InventoryItemRequestDto"/> contain a necessary data to edit it on system</param>
        /// <returns><see langword="true"/> if update has been Succeed, otherwise <see langword="false"/> </returns>
        public async Task<bool> UpdateInventoryItem(InventoryItemRequestDto requestDto)
        {
            var ResponseInventory = new InventoryItemResponseDto(requestDto.ItemName, requestDto.PartNumber, requestDto.Description, requestDto.Quintity, requestDto.ReorderLevel, requestDto.UnitCost, requestDto.LocationID, requestDto.IsActive);
            if (!CheckOutValidateInput(ResponseInventory, true, requestDto.ID)) return false;

            return await _Context.inventoryItems
                                 .Where(x => x.ID == requestDto.ID)
                                 .ExecuteUpdateAsync(setting =>setting
                                 .SetProperty(x => x.ItemName, requestDto.ItemName)
                                 .SetProperty(x => x.PartNumber, requestDto.PartNumber)
                                 .SetProperty(x => x.Description, requestDto.Description)
                                 .SetProperty(x => x.Quintity,requestDto.Quintity)
                                 .SetProperty(x => x.ReorderLevel, requestDto.ReorderLevel)
                                 .SetProperty(x => x.UnitCost,requestDto.UnitCost)
                                 .SetProperty(x => x.LocationID,requestDto.LocationID)
                                 .SetProperty(x => x.IsActive,requestDto.IsActive)) > 0;
        }

        /// <summary>
        /// Delete Part-Item from inventory dataset 
        /// </summary>
        /// <param name="Id">unique Identifier of Part-Item </param>
        /// <returns><see langword="true"/> if Delete operation has been Done, otherwise return <see langword="false"/> </returns>
        public async Task<bool> DeleteInventoryItem(int Id)
        {
            if(Id <1) return false;

            return await _Context.inventoryItems
                           .Where(x => x.ID == Id)
                           .ExecuteDeleteAsync() > 0;
        }


        private  bool CheckOutValidateInput(InventoryItemResponseDto responseDto ,bool IsItRequest= false,int ID =0)
        {
            if(IsItRequest)
            {
                if(ID < 1) return false;
            }

            if (responseDto == null) return false;
            if (string.IsNullOrEmpty(responseDto.ItemName)) return false;
            if (string.IsNullOrEmpty(responseDto.PartNumber)) return false;
            if (string.IsNullOrEmpty(responseDto.Description)) return false;
            if (responseDto.Quintity < 0) return false;
            if (responseDto.ReorderLevel < 0) return false;
            if (responseDto.UnitCost < 0) return false;
            if (responseDto.LocationID < 0) return false;

            return true;
        }


    }
}
