namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// data transfer object for Manager Request
    /// </summary>
    public sealed record ManagerDtoRequest
    (
        int ManagerID,
        int UserID,
        int DepartmentID,
        int? ManagedBy,
        int CreatedByAdmin

    );
}