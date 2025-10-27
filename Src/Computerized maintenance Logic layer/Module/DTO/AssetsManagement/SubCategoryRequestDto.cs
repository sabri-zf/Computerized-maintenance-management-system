namespace CMMS_Api.DTO
{
    public sealed record SubCategoryRequestDto
    (
        int ID,
        string SubCategoryName,
        int CategoryID
    );
}
