namespace CMMS_Api.DTO
{
    public sealed record UserLoginDto
    (
         string UserName,
         string DateLogin,
         string Token 
    );
}
