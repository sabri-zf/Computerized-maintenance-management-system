namespace computrized_maintenance_Data_Access.DTO
{

    /// <summary>
    /// data transfer object to contain all DataView of user
    /// </summary>
    public sealed record UserTableViewDto
    {
         public int UserID        {get;set;}
         public string UserName   {get;set;}
         public string FirstName  {get;set;}
         public string LastName   {get;set;}
         public string Email      {get;set;}
         public string Phone      {get;set;}
         public string Addrees    {get;set;}
         public DateTime BirthDay {get;set;}
         public short Permission  {get;set;}
         public string RoleName   {get;set;}
         public bool IsActive     {get;set;}
         public DateTime CreateAt { get; set; }
    }
}
