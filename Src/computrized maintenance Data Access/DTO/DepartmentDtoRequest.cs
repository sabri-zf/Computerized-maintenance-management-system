namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// Object Data Transfer Object for Department Request
    /// </summary>
    public sealed record DepartmentDtoRequest
    (
         int DepartmentID,
         string DepartmentName
    );
}
