namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// representation of admin with user details
    /// </summary>
    public sealed record AdminDtoRepresentation
        (
        int AdminID,
        UserDtoResponse User
        );
}
