using Microsoft.EntityFrameworkCore;

namespace computrized_maintenance_Data_Access.Entites.ReportsAndAnalysis
{
    //[Keyless]
    public class Report
    {
        public int ID { get; set; }
        public int AssetID { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public float Interval_Running_machine { get; set; }
        public float MTBF { get; set; }
        public float MTTR { get; set; }
        public float Availability { get; set; }
        public float MDT { get; set; }
        public DateTime CreateAt { get; set; }


        public AssetsManagment.Asset Asset { get; set; }
    }
}
