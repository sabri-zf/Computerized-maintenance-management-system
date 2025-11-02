using Computerized_maintenance_Logic_layer.Module.Tools;
using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
using computrized_maintenance_Data_Access.DTO;
using computrized_maintenance_Data_Access.DTO.DtoWrite;
using computrized_maintenance_Data_Access.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsUsers : ClsPepole
    {
        private Mode_Save _eMode;
        public  int UserID {  get; private set; }
        public string? UserName { get;  set; }
        public string? Password { get;  set; }
        public int? RoleID { get;set; }
        public ClsRoles? Role { get; set; }
        public short Permisson {  get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserTabledto dto { get; set; }

        


        public ClsUsers(PersonTableDto? personDto,UserTabledto userDto,Mode_Save mode = Mode_Save.AddNew) :base(personDto,mode)
        {
            this.UserID = userDto.UserID;
            this.UserName = userDto.UserName;
            this.Password = userDto.Password;
            this.RoleID = userDto.RoleID;
            this.Permisson = userDto.permission;
            this.IsActive = userDto.IsActive;
            this.CreatedAt = userDto.createAt;

            this.dto = userDto;
            this._eMode = mode;

            if(this._eMode == Mode_Save.Update)
            this.Role = Role!.Find(RoleID);

        }

        public ClsUsers(UserWriteData userWrite,Mode_Save mode = Mode_Save.AddNew)
            :base(userWrite.FirstName!, userWrite.LastName!, userWrite.Email!, userWrite.Phone!, userWrite.BirthDay, userWrite.Addrees!, mode)
        {
            this.UserName = userWrite.UserName;
            this.Password = userWrite.Password;
            this.RoleID = userWrite.RoleID;
            this.Permisson = userWrite.permission;
            this.IsActive = userWrite.IsActive;
            this.CreatedAt = userWrite.createAt;

            this.dto = new UserTabledto();
            this.dto.UserName = userWrite.UserName;
            this.dto.Password = userWrite.Password;
            this.dto.RoleID = userWrite.RoleID;
            this.dto.permission = userWrite.permission;
            this.dto.IsActive = userWrite.IsActive;
            this.dto.createAt = userWrite.createAt;

            this._eMode = mode;

        }

        public static ClsUsers? FindUser (int? UserID)
        {
            var UserDto = new UserTabledto();

            if (DataAccessUser.Find(UserID, ref UserDto))
            {
                PersonTableDto? personDto = ClsPepole.Find(UserDto.personID)?.Dto;

                if (personDto != null)
                {
                    return new ClsUsers(personDto, UserDto, Mode_Save.Update);
                }

            }

            return null;

        }

        public static ClsUsers? FindUser(string UserName)
        {
            var UserDto = new UserTabledto();

            if (DataAccessUser.Find_By_UserName(UserName, ref UserDto))
            {
                PersonTableDto? personDto = ClsPepole.Find(UserDto.personID)?.Dto;

                if (personDto != null)
                {
                    return new ClsUsers(personDto, UserDto, Mode_Save.Update);
                }

            }

            return null;

        }



        protected  bool AddNewUser()
        {
            this.dto.personID = base.PersonID;
            this.dto.Password = Security.HashEncrypt(this.Password);

            this.UserID = DataAccessUser.AddNewUser(dto);
            return (UserID > 0);
        }

        protected  bool UpdateUser()
        {
            return DataAccessUser.UpdateUser(dto);   
        }

        public static bool DeleteUser(int? UserID,int? PersonID)
        {
            return DataAccessUser.DeleteUser(UserID, PersonID);
        }

        public override bool Delete()
        {
            return DeleteUser(this.UserID, this.PersonID);
        }

        /// <summary>
        /// checking about User Exist on system or not 
        /// </summary>
        /// <param name="Userid">ID Matched User Id on system</param>
        /// <returns>return : true if exist , otherwise false</returns>
        public static bool IsExistUser(int? Userid)
        {
            return DataAccessUser.IsExistUser(Userid);
        }

        /// <summary>
        /// checking about User Exist on system or not 
        /// </summary>
        /// <returns>return : true if exist , otherwise false</returns>
        public override bool IsExist()
        {
            return IsExistUser(this.UserID);
        }

        /// <summary>
        /// Get all user to view details 
        /// </summary>
        /// <returns>return : collection of user data</returns>
        public static List<UserTableViewDto> GetAllUsers()
        {
            return DataAccessUser.GetAllUsers();
        }

        /// <summary>
        /// to save  new user or update, according to situation
        /// </summary>
        /// <returns>return : true if Added or updated is has been successed ,otherwise false</returns>
        public override bool Save()
        {
                    // Consentrate about check save Person First of all and if return True go on  check secend Condition
                    // if everything is done convert mode and reture clear (true)

            switch (this._eMode)
            {

                case Mode_Save.AddNew:
                    if (base.Save())
                    {
                        if (AddNewUser())
                        {
                            _eMode = Mode_Save.Update;
                            return true;
                        }

                    }
                    return false;

                case Mode_Save.Update:

                    if (base.Save())
                    {
                      return UpdateUser();
                    }
                    return false;
            }

            return false;
        }


        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("_________ User Data __________");
            str.AppendLine($" ID : {this.UserID}");
            str.AppendLine($" First_Name : {this.First_Name}");
            str.AppendLine($" Last_Name : {this.Last_Name}");
            str.AppendLine($" Email : {this.Email}");
            str.AppendLine($" Phone : {this.Phone}");
            str.AppendLine($" Address : {this.Address}");
            str.AppendLine($" Birth Day : {this.BithDay!.Value.ToShortDateString()}");
            str.AppendLine($" UserName : {this.UserName}");
            str.AppendLine($" password : {this.Password}");
            str.AppendLine($" Permission : {this.Permisson}");
            str.AppendLine($" Is Active : {this.IsActive}");
            str.AppendLine("________________________________________");

            return str.ToString();
        }
    }
}
