using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Misc;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    /// <summary>
    /// Logic-layer class for handling CRUD operations of Category entity.
    /// </summary>
    public sealed class clsCategories
    {
        #region Private Fields & Singleton

        private static AppDbContext? _Context;
        private readonly static clsCategories _Instance = new();

        public static clsCategories Instance
        {
            get
            {
                _Context = ClsUtility.ImplementDbContextService();
                return _Instance;
            }
        }

        #endregion

        public int ID { get; set; }
        public string Category_Name { get; set; } = null!;

        private clsCategories() { }

        private clsCategories(int id, string categoryName)
        {
            ID = id;
            Category_Name = categoryName;
        }


        #region CRUD Operations

        /// <summary>
        /// Asynchronously finds a Category by its unique ID.
        /// </summary>
        public async Task<clsCategories?> FindAsync(int id)
        {
            try
            {
                var entity = await _Context.Categories
                                           .AsNoTracking()
                                           .SingleOrDefaultAsync(x => x.ID == id);

                return entity is not null
                    ? new clsCategories(entity.ID, entity.Category_Name)
                    : null;
            }
            catch (Exception ex)
            {
                // TODO: Add proper logging service here
                Console.WriteLine($"Error finding Category: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Asynchronously adds a new Category to the data source.
        /// </summary>
        public async Task<bool> AddNewCategoryAsync()
        {
            try
            {
                var category = new Category
                {
                    Category_Name = this.Category_Name
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
        /// Asynchronously updates an existing Category in the data source.
        /// </summary>
        public async Task<bool> UpdateCategoryAsync()
        {
            try
            {
                var result = await _Context.Categories
                                           .Where(x => x.ID == this.ID)
                                           .ExecuteUpdateAsync(u => u
                                           .SetProperty(p => p.Category_Name, this.Category_Name));

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Category: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Asynchronously deletes a Category from the data source.
        /// </summary>
        public async Task<bool> DeleteCategoryAsync()
        {
            try
            {
                var result = await _Context.Categories
                                           .Where(x => x.ID == this.ID)
                                           .ExecuteDeleteAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Category: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> DeleteCategoryAsync(int ID)
        {
            try
            {
                var result = await _Context.Categories
                                           .Where(x => x.ID == ID)
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
        /// Asynchronously retrieves all Categories.
        /// </summary>
        public async Task<IEnumerable<Category>?> GetAllCategories()
        {
            try
            {
                return await _Context.Categories
                          .AsNoTracking()
                          .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
           
                           
        }

        #endregion




        ~clsCategories()
        {
            if (_Context is not null)
                _Context.DisposeAsync();
        }

    }
}

