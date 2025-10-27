namespace CMMS_Api.DTO
{
    public sealed record AssetImageResponseDto
  (
        string ImagePath,
        short Imagewidth,
        short ImageHight,
        int AssetID
        );
}
