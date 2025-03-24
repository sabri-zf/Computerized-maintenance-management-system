using computrized_maintenance_Data_Access;
using computrized_maintenance_Data_Access.DTO;

namespace Computerized_maintenance_Logic_layer.Module.User_Management
{
    public static class ManagerManiuplateDataExtension
    {
        public static ClsManagers? Find(this ClsManagers manager, int? ID)
        {
            ManagerDto dto = new ManagerDto();

            if (DataAccessManager.Find(ID, ref dto))
            {
                return new ClsManagers(dto);
            }
            return null;
        }

        private static bool AddNewManager(this ClsManagers managers)
        {
            managers.ManagerID = DataAccessManager.AddNewManager(managers.ManagerDto);

            return (managers.ManagerID != null || managers.ManagerID < 0);
        }

        private static bool UpdateManager(this ClsManagers managers)
        {
            return DataAccessManager.UpdateManger(managers.ManagerDto);
        }

        public static bool Save(this ClsManagers manager)
        {
            switch (manager._Mode)
            {
                case Enums.CRUDmode.Mode_Save.AddNew:
                    if (manager.AddNewManager())
                    {
                        manager._Mode = Enums.CRUDmode.Mode_Save.Update;
                        return true;
                    }
                    return false;
                case Enums.CRUDmode.Mode_Save.Update:
                    return manager.UpdateManager();
            }

            return false;
        }

        public static bool DeleteManager(this ClsManagers manager,int? ID)
        {
            return DataAccessManager.DeleteManager(manager.ManagerDto);
        }

        public static IEnumerable<ManagerViewDto>? GetAllManager(this ClsManagers manager)
        {
            return DataAccessManager.GetAllManager();
        }

    }
}
