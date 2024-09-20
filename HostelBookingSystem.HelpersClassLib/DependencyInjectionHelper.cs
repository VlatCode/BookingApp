using HostelBookingSystem.DataAccess;
using HostelBookingSystem.DataAccess.Implementations;
using HostelBookingSystem.DataAccess.Interfaces;
using HostelBookingSystem.Domain.Models;
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
            x.UseSqlServer("Server=.\\SQLExpress;Database=BookingApp;TrustServerCertificate=True;Trusted_Connection=True"));
        }

        // Injecting the repositories
        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<DataAccess.Interfaces.IRepository<User>, UserRepository>();
            services.AddTransient<DataAccess.IRepository<Hostel>, HostelRepository>();
            services.AddTransient<DataAccess.IRepository<Reservation>, ReservationRepository>();
            services.AddTransient<DataAccess.IRepository<Room>, RoomRepository>();
        }

        // Injecting the services
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHostelService, HostelService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IReservationService, ReservationService>();
        }
    }
}
