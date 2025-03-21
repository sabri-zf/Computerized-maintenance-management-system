using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
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
        public virtual int? UserID {  get; protected set; }
        public string? UserName { get;  set; }
        public string? Password { get;  set; }
        public int? RoleID { get;set; }
        public ClsRoles? Role { get; set; }
        public short? Permisson {  get; set; }
        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }


        public ClsUsers(PersonDto personDto,UserDto userDto,CRUDmode.Mode_Save mode= CRUDmode.Mode_Save.AddNew) :base(personDto,mode)
        {
            this.UserID = userDto.UserID;
            this.UserName = userDto.UserName;
            this.Password = userDto.Password;
            this.RoleID = userDto.RoleID;
            this.Permisson = userDto.permission;
            this.IsActive = userDto.IsActive;
            this.CreatedAt = userDto.createAt;

            this._eMode = mode;
            this.Role = null;


        }

        protected override bool AddNew()
        {
            //return base.AddNewPerson();
            return false;
        }

        protected override bool Update()
        {
            //return base.UpdatePerson();
            return false;   
        }

        public override bool Delete()
        {
            //return base.DeletePerson();
            return false;
        }

        public override bool Save()
        {
            switch (this._eMode)
            {

                case CRUDmode.Mode_Save.AddNew:
                    if (AddNew())
                    {
                        _eMode = CRUDmode.Mode_Save.Update;
                        return true;

                    }
                    return false;

                case CRUDmode.Mode_Save.Update:
                    return Update();
            }

            return false;
        }
    }
}
