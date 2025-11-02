using Computerized_maintenance_Logic_layer.Module.DTO.DownTimeTrackingDto;
using Computerized_maintenance_Logic_layer.Services;
using computrized_maintenance_Data_Access.Data;
using computrized_maintenance_Data_Access.Entites.DownTimeTracking;
using computrized_maintenance_Data_Access.Enumes;
using Microsoft.EntityFrameworkCore;

namespace Computerized_maintenance_Logic_layer.Module.DownTimeTracking
{
    public sealed class ClsDownTimeEvents
    {

        private readonly AppDbContext _Context;
        private readonly CalculateService _calculateService;

        /// <summary>
        /// Inject copule of objects on constractor <see cref="AppDbContext"/> as well as <see cref="CalculateService"/>
         /// </summary>
        /// <param name="context"></param>
        /// <param name="calculateService"></param>
        /// <exception cref="ArgumentNullException">throw exception if <see cref="AppDbContext"/> is <see langword="null"/>, as well as <see cref="CalculateService"/></exception>
        public ClsDownTimeEvents(AppDbContext context, CalculateService calculateService)
        {
            if(context is null ) throw new ArgumentNullException("App DbContext doesn't make a instance");
            if(calculateService is null ) throw new ArgumentNullException("Calculate Service doesn't make a instance");

            _Context = context;
            _calculateService = calculateService;
        }

        /// <summary>
        /// Retrieve Object Of Downtime has details of it for <see cref="DownTimeEventResponseDto"/>
        /// </summary>
        /// <param name="Id">Unique identifier of <see cref="DownTimeEvent"/></param>
        /// <returns>Data transfer Object <see cref="DownTimeEventResponseDto\"/>,otherwise <see langword="null"/></returns>
        public async Task<DownTimeEventResponseDto?> FindById(int Id)
        {
            if (Id < 1) return null; 

            var DownTime_object = await _Context.DownTimeEvents
                                                .AsNoTracking()
                                                .SingleOrDefaultAsync(x => x.ID == Id);

            if(DownTime_object is not DownTimeEvent) return null;

            var DowntimeDuration = _calculateService.CalculateDownTimePerHour(DownTime_object.StartDownTimeEvent, DownTime_object.EndDownTimeEvent);


            return new DownTimeEventResponseDto
                       (
                         DownTime_object.AssetID,
                         DownTime_object.WO_ID,
                         DownTime_object.StartDownTimeEvent,
                         DownTime_object.EndDownTimeEvent,
                         DowntimeDuration,
                         DownTime_object.DownTimeType.ToString(),
                         DownTime_object.Reason,
                         DownTime_object.ActionTaken,
                         DownTime_object.PerformedByID,
                         DownTime_object.CreateAt
                       );

        }

        /// <summary>
        /// Retrieve All Downtime History of Assets as <see cref="DownTimeEventResponseDto"/>
        /// </summary>
        /// <returns><see cref="IEnumerable{DownTimeEventResponseDto}"/> if data exist, otherwise <see langword="null"/></returns>
        public async Task<IEnumerable<DownTimeEventResponseDto>?> GetAllDownTimes()
        {

            try
            {
                var List = await _Context.DownTimeEvents
                             .AsNoTracking()
                             .ToListAsync();

                if (List is null || List.Count <= 0) return null;


                return List.Select
                    (x => new DownTimeEventResponseDto
                           (
                             x.AssetID,
                             x.WO_ID,
                             x.StartDownTimeEvent,
                             x.EndDownTimeEvent,
                             _calculateService.CalculateDownTimePerHour(x.StartDownTimeEvent, x.EndDownTimeEvent),
                             x.DownTimeType.ToString(),
                             x.Reason,
                             x.ActionTaken,
                             x.PerformedByID,
                             x.CreateAt
                           )
                    );

            }
            catch (Exception ex)
            {
            }

            return null;
        }
       

        /// <summary>
        /// Add new a Entity of <see cref="DownTimeEvent"/> on data-set 
        /// </summary>
        /// <param name="responseDto">Data transfer object of <see cref="DownTimeEventResponseDto"/></param>
        /// <returns><see langword="true"/> if add entity has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> AddNewDownTimeEvent(DownTimeEventResponseDto responseDto)
        {

            try
            {

                if(!_CheckValidInputData(responseDto)) return false;

                var DownTimeEntity = new DownTimeEvent()
                {
                    AssetID = responseDto.AssetID,
                    WO_ID = responseDto.WO_ID,
                    StartDownTimeEvent = responseDto.StartDownTimeEvent,
                    EndDownTimeEvent = responseDto.EndDownTimeEvent,
                    DownTimeType = (EnDownTimeType)Enum.Parse(typeof(EnDownTimeType), responseDto.DownTimeType),
                    Reason = responseDto.Reason,
                    ActionTaken = responseDto.ActionTaken,
                    PerformedByID = responseDto.PerformedByID,
                    CreateAt = responseDto.CreateAt,
                };


                var IsAdded = await _Context.DownTimeEvents
                                            .AddAsync(DownTimeEntity);


                return await _Context.SaveChangesAsync() > 0;

            }
            catch
            (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Update old a Entity of <see cref="DownTimeEvent"/> on data-set 
        /// </summary>
        /// <param name="requestDto">Data transfer object of <see cref="DownTimeEventRequestDto"/></param>
        /// <returns><see langword="true"/> if update entity has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> UpdateDownTimeEvent(DownTimeEventRequestDto requestDto)
        {
            try
            {
                var ResponsDto = new DownTimeEventResponseDto
                           (
                             requestDto.AssetID,
                             requestDto.WO_ID,
                             requestDto.StartDownTimeEvent,
                             requestDto.EndDownTimeEvent,
                             null,
                             requestDto.DownTimeType.ToString(),
                             requestDto.Reason,
                             requestDto.ActionTaken,
                             requestDto.PerformedByID,
                             requestDto.CreateAt
                           );

                if (!_CheckValidInputData(ResponsDto,true,requestDto.ID)) return false;


                return await _Context.DownTimeEvents
                                     .Where(x => x.ID == requestDto.ID)
                                     .ExecuteUpdateAsync(Setting => Setting
                                     .SetProperty(x => x.AssetID, requestDto.AssetID)
                                     .SetProperty(x => x.WO_ID, requestDto.WO_ID)
                                     .SetProperty(x => x.StartDownTimeEvent, requestDto.StartDownTimeEvent)
                                     .SetProperty(x => x.EndDownTimeEvent, requestDto.EndDownTimeEvent)
                                     .SetProperty(x => x.DownTimeType, (EnDownTimeType)Enum.Parse(typeof(EnDownTimeType), requestDto.DownTimeType))
                                     .SetProperty(x => x.Reason, requestDto.Reason)
                                     .SetProperty(x => x.ActionTaken, requestDto.ActionTaken)
                                     .SetProperty(x => x.PerformedByID, requestDto.PerformedByID)
                                     ) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete old a Entity of <see cref="DownTimeEvent"/> on data-set 
        /// </summary>
        /// <param name="Id">Unique identifier of <see cref="DownTimeEvent"/></param>
        /// <returns><see langword="true"/> if delete entity has been succeed, otherwise <see langword="false"/></returns>
        public async Task<bool> DeleteDownTimeEvent(int Id)
        {
            try
            {
                if (Id < 1) return false;

                return await _Context.DownTimeEvents
                                     .Where(x => x.ID == Id)
                                     .ExecuteDeleteAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// Check if input data were valid or not 
        /// </summary>
        /// <returns><see langword="true"/> if data has been validated, otherwise <see langword="false"/></returns>
        private bool _CheckValidInputData(DownTimeEventResponseDto responseDto,bool IsRequest = false, int ID = 0)
        {
            if(IsRequest)
            {
                if(ID < 1) return false;
            }

            if(responseDto is not DownTimeEventResponseDto) return false;
            if (responseDto.AssetID < 1) return false;
            if (responseDto.WO_ID is not null)
            {
                if(responseDto.WO_ID < 1) return false;
            }
            if(string.IsNullOrEmpty(responseDto.DownTimeType)) return false;
            if(string.IsNullOrEmpty(responseDto.Reason)) return false;
            if(string.IsNullOrEmpty(responseDto.ActionTaken)) return false;
            if (responseDto.PerformedByID < 1) return false;

            return true;
        }
    }
}
