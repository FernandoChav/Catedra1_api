using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.data;
using Catedra1.src.interfaces;
using Catedra1.src.models;
using Catedra1.src.DTOs;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
namespace Catedra1.src.repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(UserDto userDto)
        {

            if (await ExistAsync(userDto.Rut))
            {
                throw new ArgumentException("El RUT ya existe.");
            }

            if (userDto.Birthdate >= DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("La fecha de nacimiento debe ser anterior a la fecha actual.");
            }
            var userEntity = new User
            {
                Rut = userDto.Rut,
                Name = userDto.Name,
                Email = userDto.Email,
                Gender = userDto.Gender,
                Birthdate = userDto.Birthdate
            };
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _context.Users.Select(u => new UserDto
            {
                Rut = u.Rut,
                Name = u.Name,
                Email = u.Email,
                Gender = u.Gender,
                Birthdate = u.Birthdate
            }).ToListAsync();
            return users;
        }

        public async Task UpdateUser(int  id, UserDto userDto)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id  == id);

            if (user == null) return ;

            user.Rut = userDto.Rut;
            user.Name = userDto.Name;
            user.Gender = userDto.Gender;
            user.Birthdate = userDto.Birthdate;
            
            _context.Update(user);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteUser(int id)
        {
            if (!await ExistIDAsync(id))
            {
                return;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(); 
        }

        public async Task<bool> ExistAsync(string rut)
        {
            return await _context.Users.AnyAsync(u => u.Rut == rut);
        }

        public async Task<bool> ExistIDAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            return new UserDto
            {
                Rut = user.Rut,
                Name = user.Name,
                Email = user.Email,
                Gender = user.Gender,
                Birthdate = user.Birthdate
            };
        }
    }
}