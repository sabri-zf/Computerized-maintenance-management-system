namespace computrized_maintenance_Data_Access.DTO
{
    public sealed record ProcessToCreateTechnicianDto
    (
        string UserName,
        string Password,
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        string Address,
        DateTime BirthDay,
        string RoleName,
        short permission,
        bool IsActive,
        DateTime createAt,
        string DepartmentName,
        string ManagedBy,
        string CreatedBy
    );
}
