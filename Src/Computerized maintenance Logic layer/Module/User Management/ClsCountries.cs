using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.UserManagement;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsCountries
    {
        public int CountryID { get; set; }
        public string? CountryName { get; set; }


        ClsCountries(CountryDtoResquest dto)
        {
            this.CountryID = dto.CountryID;
            this.CountryName = dto.CountryName;
        }

        public static ClsCountries? Find(int? ID)
        {
            CountryDtoResquest dto = new CountryDtoResquest();

            if (DataAccessCountry.Find(ID, ref dto))
            {
                return new ClsCountries(dto);
            }

            return null;
        }
        public static string? GetCountryName(int countryID)
        {
            return DataAccessCountry.GetDepartmentName(countryID);
        }

        public string? GetCountryName()
        {
            return GetCountryName(this.CountryID);
        }

        public static IEnumerable<CountryDtoResquest>? GetAllCountry()
        {
            return DataAccessCountry.GetAll();
        }
    }
}
