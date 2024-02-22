using HostelBookingSystem.DataAccess.Interfaces;
using HostelBookingSystem.Domain.Models;
using HostelBookingSystem.DTOs.User;
using HostelBookingSystem.Mappers;
using HostelBookingSystem.Services.Interfaces;
using HostelBookingSystem.Shared.CustomExceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using XSystem.Security.Cryptography;

namespace HostelBookingSystem.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private IUsersRepository _usersRepository;
        private readonly IConfiguration _config; 
        private readonly ILogger<UsersService> _logger;


        public UsersService(IUsersRepository usersRepository, IConfiguration config, ILogger<UsersService> logger)
        {
            _usersRepository = usersRepository;
            _config = config;
            _logger = logger;
        }

        public LoggedUserDataDto LoginUser(LoginUserDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            User userDb = _usersRepository.GetUserByUsername(loginDto.Username);

            if (userDb == null)
            {
                throw new UserNotFoundException("User not found");
            }

            string hashedPassword = HashPassword(loginDto.Password, userDb.PasswordSalt);

            // Convert both hashed passwords to strings
            string hashedPasswordFromDb = Convert.ToBase64String(userDb.PasswordHash);
            string hashedPasswordFromInput = Convert.ToBase64String(Encoding.ASCII.GetBytes(hashedPassword));

            if (hashedPasswordFromDb != hashedPasswordFromInput)
            {
                throw new UserDataException("Incorrect password");
            }

            //JWT 
            string jwt = GetJWT(userDb);

            return userDb.ToLoggedUserDataDto(jwt);
        }

        public LoggedUserDataDto RegisterUser(RegisterUserDto registerUserDto)
        {
            try
            {
                ValidateUser(registerUserDto);

                byte[] salt = GenerateSalt();
                string hashedPassword = HashPassword(registerUserDto.Password, salt);

                var user = registerUserDto.ToRegisteredUser(hashedPassword, salt);

                _usersRepository.Add(user);

                var userDb = _usersRepository.GetUserByUsername(registerUserDto.Username);

                if (userDb == null)
                {
                    throw new UserNotFoundException("User not found");
                }

                string jwt = GetJWT(userDb);

                return userDb.ToLoggedUserDataDto(jwt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
                throw; // Rethrow the exception for handling at a higher level
            }
        }

        public string GetJWT(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_config["AppSettings:SecretKey"]);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                   SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                   new[]
                   {
                       new Claim(ClaimTypes.Name, user.Username),
                       new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                       //new Claim(ClaimTypes.Role, user.Role)
                   }
               )
            };

            SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public void ValidateUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }
            if (string.IsNullOrEmpty(registerUserDto.ConfirmPassword))
            {
                throw new UserDataException("Please confirm password!");
            }
            if (registerUserDto.Username.Length > 40)
            {
                throw new UserDataException("Username: Maximum length for username is 40 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.FirstName) && registerUserDto.FirstName.Length > 50)
            {
                throw new UserDataException("Maximum length for FirstName is 50 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.LastName) && registerUserDto.LastName.Length > 50)
            {
                throw new UserDataException("Maximum length for LastName is 50 characters");
            }
            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new UserDataException("Passwords must match!");
            }

            var userDb = _usersRepository.GetUserByUsername(registerUserDto.Username);

            if (userDb != null)
            {
                throw new UserDataException($"Username:{registerUserDto.Username} is already in use!");
            }

        }

        public string HashPassword(string password, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[16]; // You can adjust the size of the salt as needed
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public List<User> GetAllUsers()
        {
            return _usersRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            var user = _usersRepository.GetById(id);
            if (user == null)
            {
                throw new UserNotFoundException($"User with id {id} does not exist!");
            }
            return user;
        }
    }
}
