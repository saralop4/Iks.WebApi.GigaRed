using WatchDog;
using WatchDog.src.Enums;
namespace Iks.WebApi.Modules.WatchDog;

public static class WatchDogExtensions
{
    public static IServiceCollection AddWatchDog(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddWatchDogServices(opt =>
        {
            opt.SetExternalDbConnString = configuration.GetConnectionString("WatchDogLog");
            opt.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
            opt.IsAutoClear = true;
            opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Monthly; //que se limpien cada mes
        });
        return services;
    }  
}
