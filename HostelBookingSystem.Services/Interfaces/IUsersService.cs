using HostelBookingSystem.Domain.Models;
using HostelBookingSystem.DTOs.User;

namespace HostelBookingSystem.Services.Interfaces
{
    public interface IUsersService
    {
        List<User> GetAllUsers();
        LoggedUserDataDto RegisterUser(RegisterUserDto registerUserDto);
        LoggedUserDataDto LoginUser(LoginUserDto loginDto);
        string GetJWT(User user);
        void ValidateUser(RegisterUserDto registerUserDto);
        string HashPassword(string password, byte[] salt);
        User GetUserById(int id);
    }
}
