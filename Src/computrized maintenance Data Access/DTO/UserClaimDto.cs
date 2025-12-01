namespace computrized_maintenance_Data_Access.DTO
{
    public sealed record UserClaimDto
   (
        int UserID,
        byte Role,
        string UserName,
        string Email
   );
}
