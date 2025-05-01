namespace CMMS_Api.DTO
{
    public class Api_UserLoginDto
    {
        public string UserName { get; set; }
        public string DateLogin => DateTime.Now.ToString();
        public string Token { get; set; }
    }
}
