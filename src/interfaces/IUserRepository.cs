using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.DTOs;
using Catedra1.src.models;
namespace Catedra1.src.interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task<User> CreateUser(UserDto userDto);

        Task DeleteUser(int id);

        Task UpdateUser(int id, UserDto userDto);

        Task<bool> ExistAsync(string rut);

        Task<bool> ExistIDAsync(int id);

        Task<UserDto> GetUserByRut(string rut);
    }   
}