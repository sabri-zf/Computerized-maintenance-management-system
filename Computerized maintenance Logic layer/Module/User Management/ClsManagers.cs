using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
using computrized_maintenance_Data_Access.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public  class ClsManagers
    {
        public CRUDmode.Mode_Save _Mode;
        public ClsManagers(ManagerDto dto,CRUDmode.Mode_Save Mode = CRUDmode.Mode_Save.AddNew)
        {
            this.ManagerID = dto.ManagerID;
            this.UserID = dto.UserID;
            this.Users = ClsUsers.FindUser(UserID);
            this.DepartmentID = dto.DepartmentID;
            //Department = department;
            this.ManagedBy = dto.ManagedBy;
            //Manager = manager;
            this.CreatedByAdmin = dto.CreatedByAdmin;
            this.Admin = ClsAdmins.Find(CreatedByAdmin);

            this.ManagerDto = dto; 
            this._Mode = Mode;
        }



        public int? ManagerID { get;  set; }
        public int? UserID { get; set; }
        public ClsUsers? Users { get;  set; }
        public int? DepartmentID { get; set; }
        public ClsDepartments? Department { get; set; }
        public int? ManagedBy { get; set; }
        public ClsManagers? Manager { get; set; }
        public int CreatedByAdmin { get; set; }
        public ClsAdmins? Admin {  get; set; }
        public ManagerDto ManagerDto { get; set; }



    }
}
