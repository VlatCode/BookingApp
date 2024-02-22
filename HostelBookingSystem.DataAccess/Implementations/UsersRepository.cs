using HostelBookingSystem.DataAccess.Interfaces;
using HostelBookingSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HostelBookingSystem.DataAccess.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private DataContext _bookingAppDbContext;

        public UsersRepository(DataContext bookingAppDbContext)
        {
            _bookingAppDbContext = bookingAppDbContext;
        }

        public User Add(User entity)
        {
            _bookingAppDbContext.Users.Add(entity);
            _bookingAppDbContext.SaveChanges();

            return entity;
        }

        public void Delete(User entity)
        {
            _bookingAppDbContext.Users.Remove(entity);
            _bookingAppDbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _bookingAppDbContext.Users
                //.Include(x => x.Reservations)
                .ToList();
        }

        public User GetById(int id)
        {
            return _bookingAppDbContext.Users
                .FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _bookingAppDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return _bookingAppDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.PasswordHash == Encoding.ASCII.GetBytes(hashedPassword));
        }

        public User Update(User entity)
        {
            _bookingAppDbContext.Users.Update(entity);
            _bookingAppDbContext.SaveChanges();

            return entity;
        }
    }
}
