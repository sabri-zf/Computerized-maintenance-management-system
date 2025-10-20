namespace CMMS_Api.DTO
{
    public sealed record AssetImageRequestDto
   (
        int ID,
        string ImagePath,
        short Imagewidth,
        short ImageHight,
        int AssetID
   );
}
