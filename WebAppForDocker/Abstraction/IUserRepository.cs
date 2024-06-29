using WebAppForDocker.Dtos;
using WebAppForDocker.Models;

namespace WebAppForDocker.Abstraction
{
    public interface IUserRepository
    {
        int AddUser(UserDto user);
        RoleIdDto CheckUser(LoginDto login);
    }
}
