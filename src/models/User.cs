using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra1.src.models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Rut {get;set;} = string.Empty;
        
        [Required]
        [StringLength (100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo electrónico no es válido")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(masculino|femenino|otro|prefiero no decirlo)$", ErrorMessage = "Los generos permitidos son: 'masculino', 'femenino' o 'otro'.")]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public DateOnly Birthdate { get; set; } = new DateOnly();
    }
}