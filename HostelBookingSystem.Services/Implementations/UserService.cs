using HostelBookingSystem.DataAccess;
using HostelBookingSystem.DataAccess.Implementations;
using HostelBookingSystem.DataAccess.Interfaces;
using HostelBookingSystem.Domain.Models;
using HostelBookingSystem.DTOs;
using HostelBookingSystem.Mappers;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Interfaces;
using HostelBookingSystem.Shared.CustomExceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        // At first, we need to make an instance of the repository
        // because it needs to be a given parameter for the service
        // because the service is needed for instantiating the controller
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetAllUsers()
        {
            var usersDb = _userRepository.GetAll();
            return usersDb.Select(x => x.ToUserDto()).ToList();
        }

        public UserDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto GetByUsername(string username)
        {
            User userDb = _userRepository.GetByUsername(username);
            if (userDb == null)
            {
                throw new NotFoundException($"Username '{ username }' was not found!");
            }
            if (username == null)
            {
                throw new InvalidEntryException("Username is required");
            }

            UserDto userDto = userDb.ToUserDto();
            return userDto;
        }

        public void Register(AddUserDto user)
        {
            // 1. Validate the data that we receive
            if (string.IsNullOrEmpty(user.FirstName) || 
                string.IsNullOrEmpty(user.LastName) || 
                user.DateOfBirth == null || 
                string.IsNullOrEmpty(user.EmailAddress) || 
                string.IsNullOrEmpty(user.Username) ||  
                string.IsNullOrEmpty(user.Password))
            {
                throw new NotFoundException("All fields are required!");
            }
            if (user.FirstName.Length > 100)
            {
                throw new InvalidEntryException("Invalid entry. Try again.");
            }

            // 2. Hash the password
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // 3. Map to domain model
            User createdUser = user.ToUser(); // Use the extension method

            createdUser.FirstName = user.FirstName;
            createdUser.LastName = user.LastName;
            createdUser.DateOfBirth = user.DateOfBirth;
            createdUser.EmailAddress = user.EmailAddress;
            createdUser.Username = user.Username;
            // Set the hashed password and salt to the User entity
            createdUser.PasswordHash = passwordHash;
            createdUser.PasswordSalt = passwordSalt;

            // 4. Add to db
            _userRepository.Add(createdUser);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
