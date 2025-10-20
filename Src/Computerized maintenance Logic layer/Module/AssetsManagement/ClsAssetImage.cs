using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Misc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.AssetsManagement
{
    public class ClsAssetImage
    {
        private static AppDbContext? _Context;
        private readonly static ClsAssetImage _Instance = new();

        public static ClsAssetImage? Instance
        {
            get
            {
                if(_Instance is not null)
                {
                     _Context = ClsUtility.ImplementDbContextService();
                     return _Instance;
                }

                return null;
            }
        }

        public int ID { get; set; }
        public string ImagePath { get; set; } = null!;
        public short ImageWidth { get; set; }
        public short ImageHeight { get; set; }
        public int AssetID { get; set; }

        private ClsAssetImage() { }

        private ClsAssetImage(int id, string imagePath, short imageWidth, short imageHeight, int assetId)
        {
            this.ID = id;
            this.ImagePath = imagePath;
            this.ImageWidth = imageWidth;
            this.ImageHeight = imageHeight;
            this.AssetID = assetId;
        }

        /// <summary>
        /// Asynchronously find an AssetImage by ID.
        /// </summary>
        public async Task<ClsAssetImage?> FindAsync(int id)
        {
            var entity = await _Context.Set<AssetImage>()
                                       .AsNoTracking()
                                       .SingleOrDefaultAsync(x => x.ID == id);

            if (entity is AssetImage)
            {
                return new ClsAssetImage(entity.ID, entity.ImagePath, entity.ImageWidth, entity.ImageHeight, entity.AssetID);
            }
            return null;
        }

        /// <summary>
        /// Asynchronously add a new AssetImage to the database.
        /// </summary>
        public async Task<bool> AddNewImageAsync()
        {
            AssetImage image = new()
            {
                ID = this.ID,
                ImagePath = this.ImagePath,
                ImageWidth = this.ImageWidth,
                ImageHeight = this.ImageHeight,
                AssetID = this.AssetID
            };

            await _Context.Set<AssetImage>().AddAsync(image);
            return await _Context.SaveChangesAsync() > 0;
        }

      
        /// <summary>
        /// Asynchronously update an existing AssetImage.
        /// </summary>
        public async Task<bool> UpdateImageAsync()
        {
            return await _Context.Set<AssetImage>()
                                 .Where(x => x.ID == this.ID)
                                 .ExecuteUpdateAsync(u => u
                                     .SetProperty(p => p.ImagePath, this.ImagePath)
                                     .SetProperty(p => p.ImageWidth, this.ImageWidth)
                                     .SetProperty(p => p.ImageHeight, this.ImageHeight)
                                     .SetProperty(p => p.AssetID, this.AssetID)
                                 ) > 0;
        }

        /// <summary>
        /// Asynchronously delete an AssetImage by ID.
        /// </summary>
        public async Task<bool> DeleteImageAsync()
        {
            return await _Context.Set<AssetImage>()
                                 .Where(x => x.ID == this.ID)
                                 .ExecuteDeleteAsync() > 0;
        }

        public static async Task<bool> DeleteImageAsync(int id)
        {
            return await _Context.Set<AssetImage>()
                                 .Where(x => x.ID == id)
                                 .ExecuteDeleteAsync() > 0;
        }

        /// <summary>
        /// Asynchronously retrieve all AssetImages (read-only).
        /// </summary>
        public async Task<IEnumerable<AssetImage>> GetAllImages()
        {
            return await _Context.Set<AssetImage>()
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        ~ClsAssetImage()
        {
            if (_Context != null)
                _Context.DisposeAsync();
        }
    }
}

