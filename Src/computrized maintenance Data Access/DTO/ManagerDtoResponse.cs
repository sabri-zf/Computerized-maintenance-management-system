namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// data transfer object for Manager response
    /// </summary>
    public sealed record ManagerDtoResponse
    (
     int ManagerID,
     UserDtoResponse User,
     int DepartmentID,
     int? ManagedBy,
     int CreatedByAdmin
    );


}