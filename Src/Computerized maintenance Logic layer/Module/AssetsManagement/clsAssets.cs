using CMMS_Api.DTO;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    public sealed class clsAssets(AppDbContext _Context)
    {

        /// <summary>
        ///  Retrieve Object's Assets from Data store as <see cref="AssetResponseDto"/>"/>
        /// </summary>
        /// <param name="ID">Unique identifier of Asset</param>
        /// <returns><see cref="AssetResponseDto"/> if Data is found, otherwise <see langword="null"/></returns>
        public async Task<AssetResponseDto?> FindAsync(int ID)
        {
            var Assets = await _Context.Assets
                                       .AsNoTracking()
                                       .Where(x => x.ID == ID)
                                       .Select(
                                        x => new AssetResponseDto
                                        (
                                            x.AssetName,
                                            x.AssetTagNumber,
                                            x.ManufactuerName,
                                            x.ManufactuerModelNumber,
                                            x.PurchaseDate,
                                            x.PurchaseCost,
                                            x.WarrantyExpiryDate,
                                            x.InstallationDate,
                                            x.AssetCategoryID,
                                            x.AssetLocationID,
                                            x.AssetStatus,
                                            x.MeterReading,
                                            x.Criticality,
                                            x.CreateAssetDate,
                                            x.UpdateAssetDate,
                                            x.CreateByUser
                                        )
                                       )
                                       .SingleOrDefaultAsync();


            return Assets;
        }


        /// <summary>
        /// Retrieve Object's Assets from Data store 
        /// </summary>
        /// <param name="Name"> name of Asset you looking for </param>
        /// <returns>object <see cref="AssetResponseDto"/>,otherwise <see langword="null"/></returns>
        public async Task<AssetResponseDto?> FindByAssetNameAsync(string Name)
        {
            if (string.IsNullOrEmpty(Name)) return null;

            var Asset = await _Context.Assets
                                      .AsNoTracking()
                                      .Where(x => x.AssetName == Name)
                                      .Select(x => new AssetResponseDto
                                      (
                                            x.AssetName,
                                            x.AssetTagNumber,
                                            x.ManufactuerName,
                                            x.ManufactuerModelNumber,
                                            x.PurchaseDate,
                                            x.PurchaseCost,
                                            x.WarrantyExpiryDate,
                                            x.InstallationDate,
                                            x.AssetCategoryID,
                                            x.AssetLocationID,
                                            x.AssetStatus,
                                            x.MeterReading,
                                            x.Criticality,
                                            x.CreateAssetDate,
                                            x.UpdateAssetDate,
                                            x.CreateByUser
                                      )
                                      ).FirstOrDefaultAsync();

            return Asset;
        }


        /// <summary>
        /// Add new <see cref="Asset"/> entity and save it on dataset
        /// </summary>
        /// <param name="responseDto">Data transfer Object <see cref="AssetImageResponseDto"/> dealing with Response data</param>
        /// <returns ><see langword="true"/> if Add Entity was successful, otherwise <see langword="false"/> </returns>
        public async Task<bool> AddNewAssetAsync(AssetResponseDto responseDto)
        {
            if(!_checkOutValidatationOfinputData(responseDto)) return false;

            var AssetEntity = new Asset()
            {
                AssetName = responseDto.AssetName,
                AssetTagNumber = responseDto.AssetTagNumber,
                ManufactuerName= responseDto.ManufactuerName,
                ManufactuerModelNumber= responseDto.ManufactuerModelNumber,
                PurchaseDate= responseDto.PurchaseDate,
                PurchaseCost= responseDto.PurchaseCost,
                WarrantyExpiryDate = responseDto.WarrantyExpiryDate,
                InstallationDate= responseDto.InstallationDate,
                AssetCategoryID = responseDto.AssetCategoryID,
                AssetLocationID = responseDto.AssetLocationID,
                AssetStatus= responseDto.AssetStatus,
                MeterReading= responseDto.MeterReading,
                Criticality= responseDto.Criticality,
                CreateAssetDate = responseDto.CreateAssetDate,
                UpdateAssetDate = responseDto.UpdateAssetDate,
                CreateByUser = responseDto.CreateByUser
            };

            await _Context.Assets.AddAsync(AssetEntity);

            return await _Context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Add new <see cref="Asset"/> entity and save it on dataset
        /// </summary>
        /// <param name="requestDto">Data transfer Object <see cref="AssetImageResponseDto"/> dealing with Response data</param>
        /// <returns ><see langword="true"/> if Add Entity was successful, otherwise <see langword="false"/> </returns>
        public async Task<bool> UpdateAssetAsync(AssetRequestDto requestDto)
        {

            /*
             * High performence to Update entity Without unnecessary Tracking
             */

            var ResponseDto = new AssetResponseDto (
                                            requestDto.AssetName,
                                            requestDto.AssetTagNumber,
                                            requestDto.ManufactuerName,
                                            requestDto.ManufactuerModelNumber,
                                            requestDto.PurchaseDate,
                                            requestDto.PurchaseCost,
                                            requestDto.WarrantyExpiryDate,
                                            requestDto.InstallationDate,
                                            requestDto.AssetCategoryID,
                                            requestDto.AssetLocationID,
                                            requestDto.AssetStatus,
                                            requestDto.MeterReading,
                                            requestDto.Criticality,
                                            requestDto.CreateAssetDate,
                                            requestDto.UpdateAssetDate,
                                            requestDto.CreateByUser
                                      );
            if (!_checkOutValidatationOfinputData(ResponseDto,true,requestDto.ID)) return false;

            return await _Context.Assets
                                 .Where(x => x.ID == requestDto.ID)
                                 .ExecuteUpdateAsync(A => A
                                 .SetProperty(p => p.AssetName, requestDto.AssetName)
                                 .SetProperty(p => p.AssetTagNumber, requestDto.AssetTagNumber)
                                 .SetProperty(p => p.ManufactuerName, requestDto.ManufactuerName)
                                 .SetProperty(p => p.ManufactuerModelNumber, requestDto.ManufactuerModelNumber)
                                 .SetProperty(p => p.PurchaseDate, requestDto.PurchaseDate)
                                 .SetProperty(p => p.PurchaseCost, requestDto.PurchaseCost)
                                 .SetProperty(p => p.WarrantyExpiryDate, requestDto.WarrantyExpiryDate)
                                 .SetProperty(p => p.InstallationDate, requestDto.InstallationDate)
                                 .SetProperty(p => p.AssetCategoryID, requestDto.AssetCategoryID)
                                 .SetProperty(p => p.AssetLocationID, requestDto.AssetLocationID)
                                 .SetProperty(p => p.AssetStatus, requestDto.AssetStatus)
                                 .SetProperty(p => p.MeterReading, requestDto.MeterReading)
                                 .SetProperty(p => p.Criticality, requestDto.Criticality)
                                 .SetProperty(p => p.CreateAssetDate, requestDto.CreateAssetDate)
                                 .SetProperty(p => p.CreateByUser, requestDto.CreateByUser)
                                 ) > 0;

        }

       
        /// <summary>
        ///  Delete <see cref="Asset"/> into dataset
        /// </summary>
        /// <param name="ID">Unique identifier of <see cref="Asset"/></param>
        /// <returns> <see langword="true"/> if Delete was successful, otherwise <see langword="false"/></returns>
        public async static Task<bool> DeleteAssetAsync(int ID)
        {
            return await _Context.Assets
                                 .Where(x => x.ID == ID)
                                 .ExecuteDeleteAsync() > 0;
        }

        /// <summary>
        /// Retrieve whole <see cref="Asset"/> Entity, from Dataset
        /// </summary>
        /// <returns> <see cref="IEnumerable{AssetResponseDto}"/>,otherwise <see langword="null"/> </returns>
        public async Task<IEnumerable<AssetResponseDto>?> GetAllAssetsAsync()
        {
            var List = await _Context.Assets
                           .AsNoTracking()
                           .Select(x => new AssetResponseDto
                                      (
                                            x.AssetName,
                                            x.AssetTagNumber,
                                            x.ManufactuerName,
                                            x.ManufactuerModelNumber,
                                            x.PurchaseDate,
                                            x.PurchaseCost,
                                            x.WarrantyExpiryDate,
                                            x.InstallationDate,
                                            x.AssetCategoryID,
                                            x.AssetLocationID,
                                            x.AssetStatus,
                                            x.MeterReading,
                                            x.Criticality,
                                            x.CreateAssetDate,
                                            x.UpdateAssetDate,
                                            x.CreateByUser
                                      )
                                   )
                           .ToListAsync();

            if (List.Count <= 0) return null;

            return List.AsEnumerable();
        }

        /// <summary>
        /// scan if the input data is valid to use it or not
        /// </summary>
        /// <param name="responseDto">Data transfer Object <see cref="AssetImageResponseDto"/> dealing with Response data</param>
        /// <param name="IsRequerd">check if the states is OnRequset or not</param>
        /// <param name="ID">Unique identifier of <see cref="Asset"/></param>
        /// <returns><see langword="true"/> if data is valid,otherwise <see langword="false"/> </returns>
        private bool _checkOutValidatationOfinputData(AssetResponseDto responseDto, bool IsRequerd = false, int ID = 0)
        {
            if (IsRequerd)
            {
                if (ID < 1) return false;
            }
            if (string.IsNullOrEmpty(responseDto.AssetName)) return false;
            if (string.IsNullOrEmpty(responseDto.AssetTagNumber)) return false;
            if (string.IsNullOrEmpty(responseDto.ManufactuerName)) return false;
            if (string.IsNullOrEmpty(responseDto.ManufactuerModelNumber)) return false;
            if (responseDto.PurchaseCost < 0) return false;
            if (responseDto.AssetCategoryID < 1) return false;
            if (responseDto.AssetLocationID < 1) return false;
            if (responseDto.CreateByUser < 1) return false;

            return true;
        }
    }
}
