using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsCountries
    {
        public int CountryID { get; set; }
        public string? CountryName { get; set; }


        ClsCountries(CountryTableDto dto)
        {
            this.CountryID = dto.CountryID;
            this.CountryName = dto.CountryName;
        }

        public static ClsCountries? Find(int? ID)
        {
            CountryTableDto dto = new CountryTableDto();

            if(DataAccessCountry.Find(ID,ref dto))
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

        public static IEnumerable<CountryTableDto>? GetAllCountry()
        {
            return DataAccessCountry.GetAll();
        }
    }
}
