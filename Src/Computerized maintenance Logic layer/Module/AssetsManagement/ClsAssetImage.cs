using CMMS_Api.DTO;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    public class ClsAssetImage(AppDbContext _Context)
    {

        /// <summary>
        /// Retrieve a <see cref="AssetImageResponseDto"/> record 
        /// </summary>
        /// <param name="id">Unique identitfier of <see cref="AssetImage"/> </param>
        /// <returns>Data transfer object <see cref="AssetImageResponseDto"/> record, otherwise <see langword="null"/></returns>
        public async Task<AssetImageResponseDto?> FindAsync(int Id)
        {
            if(Id < 1) return null;

            var entity = await _Context.Set<AssetImage>()
                                       .AsNoTracking()
                                       .Where(x => x.ID == Id)
                                       .Select(x => new AssetImageResponseDto
                                              (
                                                  x.ImagePath,
                                                  x.ImageWidth,
                                                  x.ImageHeight,
                                                  x.AssetID
                                              ))
                                     .SingleOrDefaultAsync();


            return entity;
        }


        /// <summary>
        /// Retrieve list for <see cref="AssetImageResponseDto"/>
        /// </summary>
        /// <returns>list of data transfer object <see cref="IEnumerable{AssetImageResponseDto}"/>, otherwise <see langword="null"/> </returns>
        public async Task<IEnumerable<AssetImageResponseDto>?> GetAllAssetImage()
        {
            var List = await _Context.Set<AssetImage>()
                                     .AsNoTracking()
                                     .Select(x => new AssetImageResponseDto
                                     (
                                         x.ImagePath,
                                         x.ImageWidth,
                                         x.ImageHeight,
                                         x.AssetID
                                     ))
                                     .ToListAsync();

            if (List.Count < 0) return null;

            return List.AsEnumerable();
        }

        /// <summary>
        /// Insert new instance of <see cref="AssetImage"/>, then remain it on dataset
        /// </summary>
        /// <param name="responseDto">Data transfer object of <see cref="AssetImageResponseDto"/>, dealing with response data</param>
        /// <returns><see langword="true"> if add <see cref="AssetImage"/> has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> AddNewImageAsync(AssetImageResponseDto responseDto)
        {

            if(!_checkOutValidatationOfinputData(responseDto)) return false;


            AssetImage image = new()
            {
                ImagePath = responseDto.ImagePath,
                ImageWidth = responseDto.Imagewidth,
                ImageHeight = responseDto.ImageHight,
                AssetID = responseDto.AssetID
            };

            await _Context.Set<AssetImage>().AddAsync(image);

            return await _Context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Update <see cref="AssetImage"/>, then remain it on dataset
        /// </summary>
        /// <param name="requestDto">Data transfer object of <see cref="AssetImageRequestDto"/>, dealing with request data</param>
        /// <returns><see langword="true"> if modify <see cref="AssetImage"/> has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateImageAsync(AssetImageRequestDto requestDto)
        {
            var Dto = new AssetImageResponseDto(requestDto.ImagePath, requestDto.Imagewidth, requestDto.ImageHight, requestDto.AssetID);

            if (!_checkOutValidatationOfinputData(Dto, true, requestDto.ID)) return false;

            return await _Context.Set<AssetImage>()
                                 .Where(x => x.ID == requestDto.ID)
                                 .ExecuteUpdateAsync(u => u
                                     .SetProperty(p => p.ImagePath, requestDto.ImagePath)
                                     .SetProperty(p => p.ImageWidth, requestDto.Imagewidth)
                                     .SetProperty(p => p.ImageHeight, requestDto.ImageHight)
                                     .SetProperty(p => p.AssetID, requestDto.AssetID)
                                 ) > 0;
        }

        /// <summary>
        /// Omit <see cref="AssetImage"/> entity from Dataset
        /// </summary>
        /// <param name="Id">Unique identifier of <see cref="AssetImage"/> entity </param>
        /// <returns></returns>
        public async Task<bool> DeleteImageAsync(int Id)
        {
            if(Id < 1) return false;

            return await _Context.Set<AssetImage>()
                                 .Where(x => x.ID == Id)
                                 .ExecuteDeleteAsync() > 0;
        }


        /// <summary>
        /// check if data input is valid when send it to update or add new entity <see cref="AssetImage"/>
        /// </summary>
        /// <param name="responseDto">Data transfer object of <see cref="AssetImageRequestDto"/>, dealing with request data</param>
        /// <param name="IsRequerd">Check if OnRequset state</param>
        /// <param name="ID">Unique identifier of <see cref="AssetImage"/> entity</param>
        /// <returns><see langword="true"/> if data is valid,otherwise <see langword="false"/></returns>
        private bool _checkOutValidatationOfinputData(AssetImageResponseDto responseDto,bool IsRequerd =false ,int ID = 0)
        {
            if(IsRequerd)
            {
                if(ID <1) return false;
            }

            if(string.IsNullOrEmpty(responseDto.ImagePath)) return false;
            if(responseDto.Imagewidth < 150) return false;
            if(responseDto.ImageHight < 150) return false;  
            if(responseDto.AssetID < 1) return false;

            return true;
        }
    }
}

