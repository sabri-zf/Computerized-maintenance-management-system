namespace CMMS_Api.DTO
{
    public class Api_AdminDto
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? RoleName { get; set; }
        public short Persmision { get; set; }
        public bool IsActive { get; set; }
    }
}
