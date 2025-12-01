namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// this record is used for adding new admin
    /// </summary>
    /// <param name="AdminID"></param>
    /// <param name="UserID"></param>
    public sealed record AdminDtoRequest
    (
       int AdminID,
       UserRequestDto User
    );
}
