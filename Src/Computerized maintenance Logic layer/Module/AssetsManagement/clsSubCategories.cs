using CMMS_Api.DTO;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    /// <summary>
    /// Logic-layer class for managing SubCategory entity operations (CRUD).
    /// </summary>
    public sealed class clsSubCategories(AppDbContext _Context)
    {
        #region CRUD Operations

        /// <summary>
        /// Retrieve <see cref="SubCategoryResponseDto"/> from a data store
        /// </summary>
        /// <param name="Id">Unique identifier of <see cref="SubCategory"/></param>
        /// <returns>Data transfer object of <see cref="SubCategoryResponseDto"/>,otherwise <see langword="null"/></returns>
        public async Task<SubCategoryResponseDto?> FindAsync(int Id)
        {
            try
            {
                if(Id < 1) return null;
                var entity = await _Context.Set<SubCategory>()
                                           .AsNoTracking()
                                           .SingleOrDefaultAsync(x => x.ID == id);

                return entity is not null
                    ? new SubCategoryResponseDto(entity.Sub_Category_Name, entity.CategoryID)
                    : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding SubCategory: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Add new a entity of <see cref="SubCategory"/> into data store
        /// </summary>
        /// <param name="responseDto">Data transfer object of <see cref="SubCategoryResponseDto"/></param>
        /// <returns><see langword="true"/> if add data has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> AddNewSubCategoryAsync(SubCategoryResponseDto responseDto)
        {
            try
            {
                if(!_IsDataInputValid(responseDto)) return false;


                var subCategory = new SubCategory
                {
                    Sub_Category_Name = responseDto.SubCategoryName,
                    CategoryID = responseDto.CategoryID
                };

                await _Context.Set<SubCategory>()
                              .AddAsync(subCategory);

                return await _Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding SubCategory: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Update a entity of <see cref="SubCategory"/> into data store
        /// </summary>
        /// <param name="requestDto">Data transfer object of <see cref="SubCategoryRequestDto"/></param>
        /// <returns><see langword="true"/> if add update has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateSubCategoryAsync(SubCategoryRequestDto requestDto)
        {
            try
            {

                if(!_IsDataInputValid(new SubCategoryResponseDto(requestDto.SubCategoryName, requestDto.CategoryID),true,requestDto.ID)) return false;
                var result = await _Context.Set<SubCategory>()
                                           .Where(x => x.ID == requestDto.ID)
                                           .ExecuteUpdateAsync(u => u
                                               .SetProperty(p => p.Sub_Category_Name, requestDto.SubCategoryName)
                                               .SetProperty(p => p.CategoryID, requestDto.CategoryID)
                                               );
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating SubCategory: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Delete a entity <see cref="SubCategory"/> from data store
        /// </summary>
        /// <param name="Id">Unique identifier of <see cref="SubCategory"/></param>
        /// <returns><see langword="true"/> if add delete has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> DeleteSubCategoryAsync(int Id)
        {
            try
            {
                if(Id < 1) return false;

                var result = await _Context.Set<SubCategory>()
                                           .Where(x => x.ID == Id)
                                           .ExecuteDeleteAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting SubCategory: {ex.Message}");
                return false;
            }
        }


       /// <summary>
       ///  Retrieve whole a record of <see cref="SubCategory"/> entity form data store
       /// </summary>
       /// <returns>Data transfer object <see cref="SubCategoryResponseDto"/>, otherwise <see langword="null"/></returns>
        public async Task<IEnumerable<SubCategoryResponseDto>> GetAllSubCategories()
        {
            var List = await _Context.Set<SubCategory>()
                                 .AsNoTracking()
                                 .Select( x => new SubCategoryResponseDto(x.Sub_Category_Name,x.CategoryID))
                                 .ToListAsync();


            return List.AsEnumerable();
        }

        #endregion
        /// <summary>
        /// check-out of Data input  is valid or not 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="IsRequest"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
       private bool _IsDataInputValid(SubCategoryResponseDto response,bool IsRequest=false,int Id=0)
        {
            if(IsRequest)
            {
                if(Id < 1) return false;
            }

            if(string.IsNullOrEmpty(response.SubCategoryName)) return false;
            if(response.CategoryID < 1) return false;


            return true;
        }

    }
}
