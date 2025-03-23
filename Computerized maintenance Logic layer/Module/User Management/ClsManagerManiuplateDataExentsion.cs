using Computerized_maintenance_Logic_layer.Module.User_Management.Enums;
using computrized_maintenance_Data_Access.DTO;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public static class ClsManagerManiuplateDataExentsion
    {
        public static ClsManagers? Find(this ClsManagers manager, int? ID)
        {
            ManagerDto dto = new ManagerDto();

            return null;
        }

        private static bool Addnew()
        {
            return true;
        }

        private static bool Update()
        {
            return true;
        }

        public static bool Delete(this ClsManagers manager, int? ID)
        {
            return false;
        }

        public static bool Save(this ClsManagers manager)
        {
            switch (manager._mode)
            {
                case CRUDmode.Mode_Save.AddNew:
                    return true;
                case CRUDmode.Mode_Save.Update: 
                    return true;
            }

            return false;
        }

    }
}
