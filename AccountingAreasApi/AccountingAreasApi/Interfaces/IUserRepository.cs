using AccountingAreasApi.Dto;
using AccountingAreasApi.Models;

namespace AccountingAreasApi.Interfaces;

public interface IUserRepository : IRepository<User>
{
    bool UserExists(UserDto user);
    User GetUserByUsername(UserDto user);
}