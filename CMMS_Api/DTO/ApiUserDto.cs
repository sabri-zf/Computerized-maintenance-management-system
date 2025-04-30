namespace CMMS_Api.DTO
{
    public class ApiUserDto
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? personID { get; set; }
        public int? RoleID { get; set; }
        public short permission { get; set; }
        public bool IsActive { get; set; }
        public DateTime createAt { get; set; }
    }
}
