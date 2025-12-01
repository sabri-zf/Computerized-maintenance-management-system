namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    ///  Recored Data transfer object treating of request
    /// </summary>
    public sealed record ProcessUpdateUserDto
    {
       public int UserId { get; set; }
        public ProcessAddUserDto userDto { get; set; }
    };

}
