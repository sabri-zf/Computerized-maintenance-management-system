
namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    ///  object Data Transfer Object for User Request
    /// </summary>
    public sealed record UserDtoResponse
    {
         public string UserName    {get;set;}
         public string FirstName   {get;set;}
         public string LastName    {get;set;}
         public string Email       {get;set;}
         public string Phone       {get;set;}
         public DateTime BirthDay  {get;set;}
         public string RoleName    {get;set;}
         public short permission   {get;set;}
         public bool IsActive      {get;set;}
         public DateTime createAt { get; set; }
    };
}
