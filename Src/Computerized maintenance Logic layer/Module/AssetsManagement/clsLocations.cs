using CMMS_Api.DTO;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Misc;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    /// <summary>
    /// Logic-layer class for managing Location entity operations (CRUD).
    /// </summary>
    public sealed class clsLocations(AppDbContext _Context)
    {
        

        #region CRUD Operations

        /// <summary>
        /// Retrieve a Location as data transfer object by its unique identifier.
        /// </summary>
        /// <param name="Id">unique identifier of <see cref="Location"/></param>
        /// <returns><see cref="LocationResponseDto"/>, otherwise <see langword="null"/></returns>
        public async Task<LocationResponseDto?> FindAsync(int Id)
        {
            try
            {
                if(Id < 1) return null;

                var entity = await _Context.Locations
                                           .AsNoTracking()
                                           .SingleOrDefaultAsync(x => x.ID == Id);

                return entity is Location ? new LocationResponseDto(entity.LocationName)
                                          : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding Location: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// add new Location to the data source asynchronously
        /// </summary>
        /// <param name="responseDto">Data transfer object <see cref="LocationResponseDto"/></param>
        /// <returns><see langword="true"/> if save entity has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> AddNewLocationAsync(LocationResponseDto responseDto)
        {
            try
            {
                if(!_IsInputDataValidate(responseDto)) return false;

                var location = new Location
                {
                    LocationName = responseDto.LocationName
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
        /// Update a Location into the data source asynchronously
        /// </summary>
        /// <param name="requestDto">Data transfer object <see cref="LocationRequestDto"/></param>
        /// <returns><see langword="true"/> if update entity has been done, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateLocationAsync(LocationRequestDto requestDto)
        {
            try
            {
                if(!_IsInputDataValidate(new LocationResponseDto(requestDto.LocationName), true, requestDto.ID)) 
                    return false;

                var result = await _Context.Locations
                                           .Where(x => x.ID == requestDto.ID)
                                           .ExecuteUpdateAsync(u => u
                                           .SetProperty(p => p.LocationName, requestDto.LocationName));

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Location: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Delete a Location from the data source asynchronously
        /// </summary>
        /// <param name="ID">Unique identifier of <see cref="Location"/></param>
        /// <returns><see langword="true"/> if Delete entity has been done, otherwise <see langword="false"/></returns>

        public async Task<bool> DeleteLocationAsync(int ID)
        {
            try
            {
                if (ID < 1) return false;

                var result = await _Context.Locations
                                           .Where(x => x.ID == ID)
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
        /// retrieve all Locations as data transfer object
        /// </summary>
        /// <returns>list of <see cref="IEnumerable{LocationResponseDto}"/>, otherwise <see langword="null"/></returns>
        public async Task<IEnumerable<LocationResponseDto>?> GetAllLocations()
        {
        var list = await _Context.Locations
                              .AsNoTracking()
                              .Select(x => new LocationResponseDto(x.LocationName))
                              .ToListAsync();

            if (list.Count < 0) return null;

            return list.AsEnumerable();
        }

        #endregion

        #region Additional Methods

       
        #endregion

       private bool _IsInputDataValidate(LocationResponseDto responseDto,bool IsRequest= false, int Id = 0)
        {
            if (IsRequest)
            {
                if (Id < 1) return false;   
            }
            if (string.IsNullOrEmpty(responseDto.LocationName)) return false;
            return true;
        }
    }
}
