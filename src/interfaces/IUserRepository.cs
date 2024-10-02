using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.DTOs;
namespace Catedra1.src.interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task CreateUser(UserDto userDto);

        Task DeleteUser(string rut);

        Task UpdateUser(string Rut, UserDto userDto);

        Task<bool> ExistAsync(string rut);
    }   
}