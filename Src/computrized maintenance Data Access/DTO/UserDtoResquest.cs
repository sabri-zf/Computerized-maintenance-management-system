
namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// object Data Transfer Object for User Request
    /// </summary>
    public sealed record UserRequestDto
   (
        int UserID,
        string UserName,
        string Password,
        int PersonID,
        int RoleID,
        short permission,
        bool IsActive,
        DateTime createAt
   );
}
