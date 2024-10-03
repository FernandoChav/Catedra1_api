using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.interfaces;
using Microsoft.AspNetCore.Mvc;
using Catedra1.src.repositories;
using Catedra1.src.DTOs;
namespace Catedra1.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Devuelve 400 Bad Request si el modelo no es válido
            }

            try
            {
                // Verificar si el RUT ya existe en la base de datos
                if (await _userRepository.ExistAsync(userDto.Rut))
                {
                    return Conflict(new { message = "El RUT ya existe." });  // 409 Conflict si el RUT ya existe
                }

                // Validar la fecha de nacimiento
                if (userDto.Birthdate >= DateOnly.FromDateTime(DateTime.Now))
                {
                    return BadRequest(new { message = "La fecha de nacimiento debe ser anterior a la fecha actual." });
                }

                // Llama al repositorio para crear el usuario
                var createdUser = await _userRepository.CreateUser(userDto);

                // Devuelve 201 Created con el usuario recién creado
                return StatusCode(201, new { message = "Usuario creado exitosamente", user = createdUser });
            }
                catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });  // 400 Bad Request para otros errores
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] string? sort = null, [FromQuery] string? gender = null)
        {
            try
            {
                // Obtiene la lista de usuarios del repositorio
                var users = await _userRepository.GetUsers();

                // Filtrar por género si se proporciona
                if (!string.IsNullOrEmpty(gender))
                {
                    var allowedGenders = new[] { "masculino", "femenino", "otro", "prefiero no decirlo" };
                    if (!allowedGenders.Contains(gender))
                    {
                        return BadRequest("El género proporcionado no es válido.");  // 400 Bad Request
                    }
                    users = users.Where(u => u.Gender == gender).ToList(); // Asegúrate de llamar a ToList()
                }

                // Ordenar si se proporciona el parámetro de ordenación
                if (!string.IsNullOrEmpty(sort))
                {
                    if (sort.Equals("asc", StringComparison.OrdinalIgnoreCase))
                    {
                        users = users.OrderBy(u => u.Name).ToList();
                    }
                    else if (sort.Equals("desc", StringComparison.OrdinalIgnoreCase))
                    {
                        users = users.OrderByDescending(u => u.Name).ToList();
                    }
                    else
                    {
                        return BadRequest("El parámetro de orden no es válido. Use 'asc' o 'desc'.");  
                    }
                }

                // Devuelve los usuarios obtenidos con éxito
                return Ok(users);  // 200 OK
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });  // Si hay algún error inesperado
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            try
            {
                // Llama al método de actualización del repositorio
                await _userRepository.UpdateUser(id, userDto);
                return Ok(new { message = "Usuario actualizado exitosamente." });  
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.Contains("no encontrado"))
                {
                    return NotFound();  
                }
                return BadRequest(new { message = ex.Message });  
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Verificar si el usuario existe
            if (!await _userRepository.ExistIDAsync(id))
            {
                return NotFound(new { message = "Usuario no encontrado." });  // Devuelve 404 Not Found si el usuario no existe
            }

            // Si el usuario existe, lo elimina
            await _userRepository.DeleteUser(id);
            return Ok(new { message = "Usuario eliminado exitosamente." });  // Devuelve 200 OK
        }
        
    }
}