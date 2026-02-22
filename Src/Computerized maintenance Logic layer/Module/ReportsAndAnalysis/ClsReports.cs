using Computerized_maintenance_Logic_layer.Module.DTO.ReportsAndAnalysis;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.ReportsAndAnalysis
{
    public sealed class ClsReports(AppDbContext _context)
    {
        /// <summary>
        ///  retrive one instance of Report analysis from dataset of particular asset
        /// </summary>
        /// <param name="AssetID">unique identifier of <see cref="Asset"/></param>
        /// <returns>data transfer object <see cref="ReportEquipmentDto"/>, otherwise <see langword="null"/></returns>
        public async Task<ReportEquipmentDto?> FindAsync(int AssetID)
        {
            if (AssetID < 1) return null;

            try
            {
               var FindOut = await _context.Reports
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.AssetID == AssetID);

                if (FindOut == null) return null;

                return new ReportEquipmentDto(FindOut.AssetID,FindOut.StartPeriod,FindOut.EndPeriod
                                              ,FindOut.MTTR,FindOut.MTBF,FindOut.MDT,FindOut.Availability
                                              ,FindOut.Interval_Running_machine,DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //public async Task<>

        ///// <summary>
        ///// Insert new instanse of report 
        ///// </summary>
        ///// <param name="reportDto"></param>
        ///// <returns></returns>
        //public async Task<bool> AddAsync(ReportEquipmentDto reportDto)
        //{
        //    if(reportDto == null) return false;
        //    if(reportDto.AsseID < 1) return false;
        //    //if(reportDto.EndPeriod) return false;
        //    if(reportDto.MDT < 0) return false;
        //    if(reportDto.MTTR < 0) return false;
        //    if(reportDto.MTBF < 0) return false;
        //    if(reportDto.Availability < 0) return false;
        //    if(reportDto.Interval_Running_Machine < 0) return false;

        //    try
        //    {

        //        var instance = new Report
        //        {
        //            AssetID = reportDto.AsseID,
        //            Availability = reportDto.Availability,
        //            EndPeriod = reportDto.EndPeriod,
        //            StartPeriod = reportDto.StartPeriod,
        //            MTTR = reportDto.MTTR,
        //            MTBF = reportDto.MTBF,
        //            MDT = reportDto.MDT,
        //            Interval_Running_machine = reportDto.Interval_Running_Machine,
        //            CreateAt = reportDto.CreateAt
        //        };

        //        await _context.Reports.AddAsync(instance);

        //        return await _context.SaveChangesAsync() > 0;

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}

        //public async Task<bool> UpdateAsync(ReportEquipmentDto requestDto)
        //{
        //    if (requestDto == null) return false;
        //    if (requestDto.AsseID < 1) return false;
        //    //if(requestDtoo.EndPeriod) return false;
        //    if (requestDto.MDT < 0) return false;
        //    if (requestDto.MTTR < 0) return false;
        //    if (requestDto.MTBF < 0) return false;
        //    if (requestDto.Availability < 0) return false;
        //    if (requestDto.Interval_Running_Machine < 0) return false;

        //    try
        //    {

        //        return await _context.Reports
        //                             .Where(x => x.AssetID == requestDto.AsseID)
        //                             .ExecuteUpdateAsync(s =>
        //                             s.SetProperty(x => x.MTTR, requestDto.MTTR)
        //                              .SetProperty(x => x.MTBF, requestDto.MTBF)
        //                              .SetProperty(x => x.Availability, requestDto.Availability)
        //                              .SetProperty(x => x.MDT, requestDto.MDT)
        //                              .SetProperty(x => x.Interval_Running_machine, requestDto.Interval_Running_Machine)
        //                              .SetProperty(x => x.StartPeriod, requestDto.StartPeriod)
        //                              .SetProperty(x => x.EndPeriod, requestDto.EndPeriod)
        //                                                   ) > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}

        //public async Task<bool> DeleteAsync(int assetID)
        //{
        //    if(assetID < 1) return false;

        //    try
        //    {
        //        return await _context.Reports
        //                        .Where(x => x.AssetID == assetID)
        //                       .ExecuteDeleteAsync() > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}
    }
}
