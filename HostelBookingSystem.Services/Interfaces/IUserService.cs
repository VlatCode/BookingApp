using HostelBookingSystem.Domain.Models;
using HostelBookingSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto GetById(int id);
        UserDto GetByUsername(string username);
        public void Register(AddUserDto user);
        void DeleteUser(int id);
    }
}
