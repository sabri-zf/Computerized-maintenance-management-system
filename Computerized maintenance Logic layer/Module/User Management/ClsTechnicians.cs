using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public class ClsTechnicians
    {
        public Mode_Save _mode;
        public int TechnicianID { get; private set; }
        public int UserID { get; set; }
        public ClsUsers? User { get; set; }
        public int DepartmentID { get; set; }
        public ClsDepartments? Department { get; set; }
        public int ManagedByID { get; set; }
        public ClsManagers? ManagerBy { get; set; }
        public int CreatedByAdmin { get; set; }
        public ClsAdmins? AdminCreated { get; set; }

        public TechnicianDto Dto { get; set; }

        public ClsTechnicians(TechnicianDto Dto, Mode_Save Mode = Mode_Save.AddNew)
        {
            this.TechnicianID = Dto.TechnicianID;
            this.UserID = Dto.UserID;
            this.DepartmentID = Dto.DepartmentID;
            this.ManagedByID = Dto.ManagedBy;
            this.CreatedByAdmin = Dto.CreatedByAdmin;
            this.Dto = Dto;

            this._mode = Mode;
            if(_mode == Mode_Save.Update)
            {
                this.User = ClsUsers.FindUser(this.UserID);
                this.Department = null;
                this.ManagerBy = ClsManagers.Find(this.ManagedByID);
                this.AdminCreated = ClsAdmins.Find(this.CreatedByAdmin);
            }

        }


        public static ClsTechnicians? Find(int? ID)
        {
            TechnicianDto technicianDto  = new TechnicianDto();

            if(DataAccessTechnician.Find(ID, ref technicianDto))
            {
                return new ClsTechnicians(technicianDto);
            }

            return null;
        }

        public static bool Delete (int ID)
        {
            return DataAccessTechnician.DeleteTechnician(ID); 
        }

        public bool Delete()
        {
            return Delete(this.TechnicianID);
        }

        private bool Update()
        {
            return DataAccessTechnician.UpdateTechnician(Dto);
        }

        private bool AddNew()
        {
            this.TechnicianID = DataAccessTechnician.AddNewTechnician(this.Dto);

            return (this.TechnicianID > 0);
        }

        public bool Save()
        {
            switch (this._mode)
            {
                case Mode_Save.AddNew:
                    if (AddNew())
                    {
                        _mode = Mode_Save.Update;
                        return true;
                    }
                    return false;
                case Mode_Save.Update:
                    return Update();
            }
            return false;
        }


        public List<TechnicianViewDto>? GetAllTechnicians()
        {
            return DataAccessTechnician.GetAll();
        }
    }
}
