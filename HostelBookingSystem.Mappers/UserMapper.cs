using HostelBookingSystem.Domain.Models;
using HostelBookingSystem.DTOs;
using HostelBookingSystem.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Mappers
{
    public static class UserMapper
    {
        public static User ToRegisteredUser(this RegisterUserDto registerUserDto, string hashedPassword, byte[] passwordSalt)
        {
            return new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                PasswordHash = Encoding.ASCII.GetBytes(hashedPassword),
                DateOfBirth = registerUserDto.DateOfBirth,
                EmailAddress = registerUserDto.EmailAddress,
                PasswordSalt = passwordSalt,
            };
        }

        public static LoggedUserDataDto ToLoggedUserDataDto(this User user, string token)
        {
            return new LoggedUserDataDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                //Role = user.Role,
                Token = token,
            };
        }
    }
}
