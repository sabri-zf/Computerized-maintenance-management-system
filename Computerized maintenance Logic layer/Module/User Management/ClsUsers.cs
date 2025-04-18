﻿using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public short? Permisson {  get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public UserDto dto { get; set; }

        


        public ClsUsers(PersonDto? personDto,UserDto userDto,Mode_Save mode = Mode_Save.AddNew) :base(personDto,mode)
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
            this.Role = Role.Find(RoleID);

        }

        public static ClsUsers? FindUser (int? UserID)
        {
            var UserDto = new UserDto();

            if (DataAccessUser.Find(UserID, ref UserDto))
            {
                PersonDto? personDto = ClsPepole.Find(UserDto.personID)?.Dto;

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
            this.UserID = DataAccessUser.AddNewUser(dto);
            return (UserID > 0);
        }

        protected  bool UpdateUser()
        {
            return DataAccessUser.UpdateUser(dto);   
        }

        public static bool Delete(int? UserID,int? PersonID)
        {
            return DataAccessUser.DeleteUser(UserID, PersonID);
        }

        public override bool Delete()
        {
            return Delete(this.UserID, this.PersonID);
        }


        public static List<UserViewDto> GetAllUsers()
        {
            return DataAccessUser.GetAllUsers();
        }

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
