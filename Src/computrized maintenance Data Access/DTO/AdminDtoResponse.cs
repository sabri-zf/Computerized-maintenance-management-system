namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// this record is used for response after adding new admin
    /// </summary>
    public sealed record AdminDtoResponse
    {
      public int UserID           {get;set;}
      public string UserName      {get;set;}
      public string FirstName     {get;set;}
      public string LastName      {get;set;}
      public string Email         {get;set;}
      public string Phone         {get;set;}
      public DateTime BirthDay    {get;set;}
      public string RoleName      {get;set;}
      public short Permission { get;set;}
      public bool IsActive        {get;set;}
      public DateTime createAt { get; set; }
    };
}
