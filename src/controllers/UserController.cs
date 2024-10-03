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
            try
            {
                // Llama al método CreateUser del repositorio
                await _userRepository.CreateUser(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = userDto.Rut }, userDto); // Devuelve 201 Created
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.Contains("El RUT ya existe"))
                {
                    return Conflict(new { message = ex.Message });  // 409 Conflict si el RUT ya existe
                }
                return BadRequest(new { message = ex.Message });  // 400 Bad Request si alguna validación falla
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] string sort, [FromQuery] string gender)
        {
            try
            {
                
                var users = await _userRepository.GetUsers();

                
                if (!string.IsNullOrEmpty(gender))
                {
                    var allowedGenders = new[] { "masculino", "femenino", "otro", "prefiero no decirlo" };
                    if (!allowedGenders.Contains(gender))
                    {
                        return BadRequest("El género proporcionado no es válido.");  // 400 Bad Request
                    }
                    users = users.Where(u => u.Gender == gender);
                }

                
                if (!string.IsNullOrEmpty(sort))
                {
                    if (sort == "asc")
                    {
                        users = users.OrderBy(u => u.Name);
                    }
                    else if (sort == "desc")
                    {
                        users = users.OrderByDescending(u => u.Name);
                    }
                    else
                    {
                        return BadRequest("El parámetro de orden no es válido. Use 'asc' o 'desc'.");  
                    }
                }

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
    }
}