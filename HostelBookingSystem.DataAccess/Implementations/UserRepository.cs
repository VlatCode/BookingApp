using HostelBookingSystem.DataAccess.Interfaces;
using HostelBookingSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelBookingSystem.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private DataContext _bookingAppDbContext;

        public UserRepository (DataContext bookingAppDbContext)
        {
            _bookingAppDbContext = bookingAppDbContext;
        }

        public void Add(User entity)
        {
            _bookingAppDbContext.Users.Add(entity);
            _bookingAppDbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _bookingAppDbContext.Users.Remove(entity);
            _bookingAppDbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _bookingAppDbContext.Users
                .Include(x => x.Reservations)
                .ToList();
        }

        public User GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
