using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Misc;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    /// <summary>
    /// Logic-layer class for managing Location entity operations (CRUD).
    /// </summary>
    public sealed class clsLocations
    {
        #region Private Fields & Singleton

        private static AppDbContext? _Context;
        private readonly static clsLocations _Instance = new();

        public static clsLocations Instance
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
        public string LocationName { get; set; } = null!;

        #endregion

        #region Constructors

        private clsLocations() { }

        private clsLocations(int id, string locationName)
        {
            ID = id;
            LocationName = locationName;
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Asynchronously find a Location by ID.
        /// </summary>
        public async Task<clsLocations?> FindAsync(int id)
        {
            try
            {
                var entity = await _Context.Locations
                                           .AsNoTracking()
                                           .SingleOrDefaultAsync(x => x.ID == id);

                return entity is not null
                    ? new clsLocations(entity.ID, entity.LocationName)
                    : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding Location: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Asynchronously add a new Location.
        /// </summary>
        public async Task<bool> AddNewLocationAsync()
        {
            try
            {
                var location = new Location
                {
                    LocationName = this.LocationName
                };

                await _Context.Locations.AddAsync(location);
                return await _Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Location: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Asynchronously update an existing Location.
        /// </summary>
        public async Task<bool> UpdateLocationAsync()
        {
            try
            {
                var result = await _Context.Locations
                                           .Where(x => x.ID == this.ID)
                                           .ExecuteUpdateAsync(u => u
                                           .SetProperty(p => p.LocationName, this.LocationName));

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Location: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Asynchronously delete a Location by ID.
        /// </summary>
        public async Task<bool> DeleteLocationAsync()
        {
            try
            {
                var result = await _Context.Locations
                                           .Where(x => x.ID == this.ID)
                                           .ExecuteDeleteAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Location: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Retrieve all Locations (read-only query).
        /// </summary>
        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return  await _Context.Locations
                           .Include(l => l.Asset)
                           .AsNoTracking()
                           .ToListAsync();
        }

        #endregion

        #region Additional Methods

        /// <summary>
        /// Asynchronously get a Location with its related Asset.
        /// </summary>
        //public async Task<Location?> GetLocationWithAssetAsync(int id)
        //{
        //    try
        //    {
        //        return await _Context.Locations
        //                             .Include(l => l.Asset)
        //                             .AsNoTracking()
        //                             .SingleOrDefaultAsync(l => l.ID == id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error loading related Asset: {ex.Message}");
        //        return null;
        //    }
        //}

        #endregion

        #region Destructor

        ~clsLocations()
        {
            if (_Context is not null)
                _Context.DisposeAsync();
        }

        #endregion
    }
}
