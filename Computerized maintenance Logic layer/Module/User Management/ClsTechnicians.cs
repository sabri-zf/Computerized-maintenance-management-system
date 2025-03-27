using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
using computrized_maintenance_Data_Access.DTO;
using System.Xml.Linq;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsTechnicians
    {
        public CRUDmode.Mode_Save _mode;
        public int TechnicianID { get; private set; }
        public int UserID { get; set; }
        public ClsUsers? User { get; set; }
        public int DepartmentID { get; set; }
        public ClsDepartments? Department { get; set; }
        public int ManagedBy { get; set; }
        public ClsManagers? ManagerBy { get; set; }
        public int CreatedByAdmin { get; set; }
        public ClsAdmins? AdminCreated { get; set; }

        public ClsTechnicians(TechnicianDto Dto, CRUDmode.Mode_Save Mode = CRUDmode.Mode_Save.AddNew)
        {
            this.TechnicianID = Dto.TechnicianID;
            this.UserID = Dto.UserID;
            this.DepartmentID = Dto.DepartmentID;
            this.ManagedBy = Dto.ManagedBy;
            this.CreatedByAdmin = Dto.CreatedByAdmin;

            this._mode = Mode;
            if(_mode == CRUDmode.Mode_Save.Update)
            {
                this.User = ClsUsers.FindUser(this.UserID);
                this.Department = null;
                this.ManagerBy = ManagerBy.Find(this.ManagedBy);
                this.AdminCreated = ClsAdmins.Find(this.CreatedByAdmin);
            }

        }


        public ClsTechnicians? Find(int? ID)
        {
            return null;
        }

        public static bool Delete (int? ID)
        {
            return false; 
        }

        public bool Delete()
        {
            return Delete(this.TechnicianID);
        }

        private bool Update()
        {
            return false;
        }

        private bool AddNew()
        {
            return false;
        }

        public bool Save()
        {
            switch (this._mode)
            {
                case CRUDmode.Mode_Save.AddNew:
                    if (AddNew())
                    {
                        _mode = CRUDmode.Mode_Save.Update;
                        return true;
                    }
                    return false;
                case CRUDmode.Mode_Save.Update:
                    return true;
            }
            return false;
        }


        public IEnumerable<TechnicianDto>? GetAllTechnicians()
        {
            return null;
        }
    }
}
