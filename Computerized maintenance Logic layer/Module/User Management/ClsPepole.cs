using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
using Computerized_maintenance_Logic_layer.Module.User_Management.Interface;
using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsPepole:Iperson
    {
       public  PersonDto? Dto {  get; set; }

        protected CRUDmode.Mode_Save _eMode;
        public  int? PersonID { get; private set; }

        public string? First_Name { get;set; }

        public string? Last_Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateTime? BithDay {  get; set; } 
        
        public string? Address {  get; set; }

        
        public ClsPepole(PersonDto peopleDto, CRUDmode.Mode_Save eMode = CRUDmode.Mode_Save.AddNew)
        { 
            _eMode = eMode;
            this.PersonID = peopleDto.PersonID;
            this.First_Name = peopleDto.FirstName;
            this.Last_Name = peopleDto.LastName;
            this.Email = peopleDto.Email;
            this.Phone = peopleDto.Phone;
            this.BithDay = peopleDto.BirthDay;
            this.Address = peopleDto.Email;
            this.Dto = peopleDto;
        }

        protected virtual bool AddNewPerson()
        {
            this.PersonID = DataAccessPeople.AddNewPerson(Dto);

            return(this.PersonID.HasValue && this.PersonID > 0 );
        }

        protected virtual  bool UpdatePerson()
        {
           return DataAccessPeople.UpdatePerson(Dto);
        }

        public static bool DeletePerson(int? ID)
        {
            return DataAccessPeople.DeletePerson(ID);
        }

        public virtual bool DeletePerson()
        {
            return DeletePerson(this.PersonID);
        }
       

        public static bool IsExist(int? ID)
        {
            return DataAccessPeople.IsExistPerson(ID);
        }

        public virtual bool IsExist()
        {
            return IsExist(this.PersonID);
        }

        public static List<PersonDto> GetAllPeople()
        {
            return DataAccessPeople.GetPeople();
        }

        public static async Task<List<PersonDto>> GetAllPeopleAsync()
        {
            return await DataAccessPeople.GetPeopleAsync();
        }

        public bool Save()
        {
            switch ( _eMode)
            {
                case CRUDmode.Mode_Save.AddNew:

                    if (AddNewPerson())
                    {
                        _eMode = CRUDmode.Mode_Save.Update;
                        return true;
                    }else
                    {
                        return false;
                    }
                case CRUDmode.Mode_Save.Update:
                    return UpdatePerson();
            }

            return false;
        }
    }
}
