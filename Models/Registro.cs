
namespace tarea4_juanfontes.Models
{
    public class Registro
    {
        public string Id { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool Verificado { get; set; }
    }
}
