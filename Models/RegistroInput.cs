
using System.ComponentModel.DataAnnotations;

namespace tarea4_juanfontes.Models
{
    public class RegistroInput
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Email { get; set; } = string.Empty;
    }
}
