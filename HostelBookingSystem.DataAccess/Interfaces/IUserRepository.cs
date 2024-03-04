using HostelBookingSystem.Domain.Models;

namespace HostelBookingSystem.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User LoginUser(string username, string hashedPassword);
        User GetUserByUsername(string username);
    }
}
