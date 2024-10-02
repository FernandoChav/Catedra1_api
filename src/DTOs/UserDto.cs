using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Catedra1.src.DTOs
{
    public class UserDto
    {
        [Required]
        public string Rut {get;set;} = string.Empty;

        [Required]
        [StringLength (100, MinimumLength = 3, ErrorMessage = "El nombre debe tener minimo 3 y maximo 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo electrónico no es válido")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(masculino|femenino|otro)$", ErrorMessage = "Los generos permitidos son: 'masculino', 'femenino' o 'otro'.")]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public DateOnly Birthdate { get; set; } = new DateOnly();
    }
}