using CMMS_Api.DTO;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Misc;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    /// <summary>
    /// Logic-layer class for handling CRUD operations of Category entity.
    /// </summary>
    public sealed class clsCategories(AppDbContext _Context)
    {
        #region CRUD Operations

        /// <summary>
        /// Retrieve a Category as data transfer object by its unique identifier.
        /// </summary>
        /// <param name="id">unique identifier of <see cref="Category"/></param>
        /// <returns><see cref="CategoryResponseDto"/> if find has been succeed, otherwise <see langword="null"/></returns>
        public async Task<CategoryResponseDto?> FindAsync(int id)
        {
            try
            {
                if(id < 1) return null; 


                var entity = await _Context.Categories
                                           .AsNoTracking()
                                           .SingleOrDefaultAsync(x => x.ID == id);

                return entity is Category ? new CategoryResponseDto(entity.Category_Name): null;
            } 
            catch (Exception ex)
            {
                // TODO: Add proper logging service here
                Console.WriteLine($"Error finding Category: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Add new Category to the data source asynchronously
        /// </summary>
        /// <param name="responseDto">Data transfer object of <see cref="CategoryResponseDto"/></param>
        /// <returns><see langword="true"/> if add entity has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> AddNewCategoryAsync(CategoryResponseDto responseDto)
        {
            try
            {

                if(!_IsDataInputValidate(responseDto)) return  false;

                var category = new Category
                {
                    Category_Name = responseDto.CategoryName
                };

                await _Context.Categories.AddAsync(category);
                return await _Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Category: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Add new Category to the data source asynchronously
        /// </summary>
        /// <param name="requestDto">Data transfer object of <see cref="CategoryRequestDto"/></param>
        /// <returns><see langword="true"/> if update entity has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateCategoryAsync(CategoryRequestDto requestDto)
        {
            try
            {

                if(!_IsDataInputValidate(new CategoryResponseDto(requestDto.CategoryName), true, requestDto.ID)) 
                    return  false;


                var result = await _Context.Categories
                                           .Where(x => x.ID == requestDto.ID)
                                           .ExecuteUpdateAsync(u => u
                                           .SetProperty(p => p.Category_Name, requestDto.CategoryName)
                                           );

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Category: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Delete Category from the data source asynchronously
        /// </summary>
        /// <param name="Id">unique identifier of <see cref="Category"/></param>
        /// <returns><see langword="true"/> if delete entity has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> DeleteCategoryAsync(int Id)
        {
            try
            {
                var result = await _Context.Categories
                                           .Where(x => x.ID == Id)
                                           .ExecuteDeleteAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Category: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// retrieve all Categories as data transfer object
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryResponseDto>?> GetAllCategories()
        {
            try
            {
                var List = await _Context.Categories
                           .AsNoTracking()
                           .Select(x => new CategoryResponseDto(x.Category_Name))
                           .ToListAsync();

                if(List.Count < 0) return null;

                return List.AsEnumerable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
           
                           
        }

        #endregion

        /// <summary>
        /// Check if input data is valid when sending it to update or add new <see cref="Category"/> entity
        /// </summary>
        /// <param name="responseDto">Data transfer object of <see cref="CategoryResponseDto"/></param>
        /// <param name="isRequired">Check if is it ready to be OnRequest</param>
        /// <param name="id">unique identifier of <see cref="Category"/></param>
        /// <returns><see langword="true"/> if data is valid, otherwise <see langword="false"/> </returns>
        private bool _IsDataInputValidate(CategoryResponseDto responseDto, bool isRequired = false, int id = 0)
        {
            if (isRequired)
            {
                if (id < 1) return false;
            }
            if (string.IsNullOrEmpty(responseDto.CategoryName)) return false;

            return true;
        }


    }
}

