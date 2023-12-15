using HostelBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetByUsername(string username);
        void Add(User entity);
        void Update(User entity);
        void Delete(User entity);
    }
}
