namespace computrized_maintenance_Data_Access.DTO
{
    /// <summary>
    /// Object Data Transfer Object for Country Request
    /// </summary>
    /// <remarks>This DTO is typically used to encapsulate country-related data in requests.</remarks>
    public sealed record CountryDtoResquest
    {
        public int CountryID { get; set; }
        public string? CountryName { get; set; }
    }
}
