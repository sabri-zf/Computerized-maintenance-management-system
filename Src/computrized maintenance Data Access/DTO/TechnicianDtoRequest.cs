namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// object Data Transfer Object for Technician Request
    /// </summary>
    public sealed record TechnicianDtoRequest
    (
         int TechnicianID,
         int UserID,
         int DepartmentID,
         int ManagedBy,
         int CreatedByAdmin
    );
}
