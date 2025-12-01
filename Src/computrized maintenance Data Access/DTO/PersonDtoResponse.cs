namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// object Data Transfer Object for Person Response
    /// </summary>
    public sealed record PersonDtoResponse
    {
      public string FirstName { get; set; }
      public string LastName    {get;set;}
      public string Email       {get;set;}
      public string Phone       {get;set;}
      public DateTime BirthDay  {get;set;}
      public string Addrees     {get;set;}
  };
}
