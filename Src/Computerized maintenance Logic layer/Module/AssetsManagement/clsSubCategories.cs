using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Misc;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    /// <summary>
    /// Logic-layer class for managing SubCategory entity operations (CRUD).
    /// </summary>
    public sealed class clsSubCategories
    {
        #region Private Fields & Singleton

        private static AppDbContext? _Context;
        private readonly static clsSubCategories _Instance = new();

        public static clsSubCategories Instance
        {
            get
            {
                _Context = ClsUtility.ImplementDbContextService();
                return _Instance;
            }
        }

        #endregion

        #region Properties

        public int ID { get; set; }
        public string Sub_Category_Name { get; set; } = null!;
        public int CategoryID { get; set; }

        #endregion

        #region Constructors

        private clsSubCategories() { }

        private clsSubCategories(int id, string subCategoryName, int categoryId)
        {
            ID = id;
            Sub_Category_Name = subCategoryName;
            CategoryID = categoryId;
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Asynchronously finds a SubCategory by ID.
        /// </summary>
        public async Task<clsSubCategories?> FindAsync(int id)
        {
            try
            {
                var entity = await _Context.Set<SubCategory>()
                                           .AsNoTracking()
                                           .SingleOrDefaultAsync(x => x.ID == id);

                return entity is not null
                    ? new clsSubCategories(entity.ID, entity.Sub_Category_Name, entity.CategoryID)
                    : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding SubCategory: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Asynchronously adds a new SubCategory to the database.
        /// </summary>
        public async Task<bool> AddNewSubCategoryAsync()
        {
            try
            {
                var subCategory = new SubCategory
                {
                    Sub_Category_Name = this.Sub_Category_Name,
                    CategoryID = this.CategoryID
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
        /// Asynchronously updates an existing SubCategory.
        /// </summary>
        public async Task<bool> UpdateSubCategoryAsync()
        {
            try
            {
                var result = await _Context.Set<SubCategory>()
                                           .Where(x => x.ID == this.ID)
                                           .ExecuteUpdateAsync(u => u
                                               .SetProperty(p => p.Sub_Category_Name, this.Sub_Category_Name)
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
        /// Asynchronously deletes a SubCategory by ID.
        /// </summary>
        public async Task<bool> DeleteSubCategoryAsync()
        {
            try
            {
                var result = await _Context.Set<SubCategory>()
                                           .Where(x => x.ID == this.ID)
                                           .ExecuteDeleteAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting SubCategory: {ex.Message}");
                return false;
            }
        }


        public static async Task<bool> DeleteSubCategoryAsync(int ID)
        {
            try
            {
                var result = await _Context.Set<SubCategory>()
                                           .Where(x => x.ID == this.ID)
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
        /// Retrieves all SubCategories (read-only query).
        /// </summary>
        public async Task<IEnumerable<SubCategory>> GetAllSubCategories()
        {
            return await _Context.Set<SubCategory>()
                           .Include(s => s.Category)
                           .AsNoTracking()
                           .ToListAsync();
        }

        #endregion

       
        #region Destructor

        ~clsSubCategories()
        {
            if (_Context is not null)
                _Context.DisposeAsync();
        }

        #endregion
    }
}
