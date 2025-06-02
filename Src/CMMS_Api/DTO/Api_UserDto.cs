namespace CMMS_Api.DTO
{
    public class Api_UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string  Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public short Permission { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
