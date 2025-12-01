namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// object Data Transfer Object for Technician Request
    /// </summary>
    public sealed record TechnicianDtoRepose
    (
         UserDtoResponse User,
         DepartmentDtoReponse Department,
         ManagerDtoResponse ManagedBy,
         AdminDtoResponse CreatedByAdmin
    );
}
