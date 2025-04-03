using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsManagers
    {
        private Mode_Save _Mode;
        public ClsManagers(ManagerDto dto, Mode_Save Mode = Mode_Save.AddNew)
        {
            this.ManagerID = dto.ManagerID;
            this.UserID = dto.UserID;
            this.DepartmentID = dto.DepartmentID;
            this.ManagedBy = dto.ManagedBy;
            this.CreatedByAdmin = dto.CreatedByAdmin;

            this.ManagerDto = dto;
            this._Mode = Mode;

            if (this._Mode == Mode_Save.Update)
            {
                this.Admin = ClsAdmins.Find(CreatedByAdmin);
                this.Department = ClsDepartments.Find(this.DepartmentID);
                this.Users = ClsUsers.FindUser(UserID);
                this.Managed = ClsManagers.Find(this.ManagedBy);
            }
        }

        public static ClsManagers? Find(int? ID)
        {
            ManagerDto dto = new ManagerDto();

            if (DataAccessManager.Find(ID, ref dto))
            {
                return new ClsManagers(dto);
            }
            return null;
        }

        private  bool AddNewManager()
        {
            this.ManagerID = DataAccessManager.AddNewManager(this.ManagerDto);

            return (this.ManagerID != null || this.ManagerID < 0);
        }

        private  bool UpdateManager()
        {
            return DataAccessManager.UpdateManger(this.ManagerDto);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode_Save.AddNew:
                    if (AddNewManager())
                    {
                        _Mode = Mode_Save.Update;
                        return true;
                    }
                    return false;
                case Mode_Save.Update:
                    return UpdateManager();
            }

            return false;
        }

        public static bool DeleteManager(ManagerDto managerDto)
        {
            return DataAccessManager.DeleteManager(managerDto);
        }

        public  bool DeleteManager()
        {
            return DeleteManager(this.ManagerDto);
        }
        public static bool IsExist(int ID)
        {
            return DataAccessManager.IsExistManager(ID);
        }
        public static IEnumerable<ManagerViewDto>? GetAllManager()
        {
            return DataAccessManager.GetAllManager();
        }


        public int? ManagerID { get; set; }
        public int? UserID { get; set; }
        public ClsUsers? Users { get; set; }
        public int? DepartmentID { get; set; }
        public ClsDepartments? Department { get; set; }
        public int? ManagedBy { get; set; }
        public ClsManagers? Managed { get; set; }
        public int CreatedByAdmin { get; set; }
        public ClsAdmins? Admin { get; set; }
        public ManagerDto ManagerDto { get; set; }
    }
}
