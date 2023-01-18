using HostelBookingSystem.DataAccess;
using HostelBookingSystem.DataAccess.Implementations;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Implementations;
using HostelBookingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HostelBookingSystem.HelpersClassLib
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x =>
            x.UseSqlServer("Server=.\\SQLExpress;Database=BookingAppDb;TrustServerCertificate=True;Trusted_Connection=True"));
        }

        // We need to inject the repositories
        public static void InjectRepositories(IServiceCollection services)
        {
            // .AddTransient does the following:
            // When the app needs to use the methods in the generic interface IRepository,
            // it will receive the appropriate implementation for each class
            services.AddTransient<IRepository<Hostel>, HostelRepository>();
            services.AddTransient<IRepository<Reservation>, ReservationRepository>();
            services.AddTransient<IRepository<Room>, RoomRepository>();
        }

        // Injecting the services
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IHostelService, HostelService>();
            services.AddTransient<IRoomService, RoomService>();
            //services.AddTransient<IReservationService, ReservationService>();
        }
    }
}
