using HostelBookingSystem.Domain.Models;

namespace HostelBookingSystem.DataAccess.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        User LoginUser(string username, string hashedPassword);
        User GetUserByUsername(string username);
    }
}
