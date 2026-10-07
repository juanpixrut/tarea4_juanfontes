
using tarea4_juanfontes.Models;

namespace tarea4_juanfontes.Services
{
    public class RegistroService
    {
        private readonly List<Registro> registros = new();
        private readonly object bloqueo = new();

        public Registro Crear(string nombre, string email)
        {
            var registro = new Registro
            {
                Id = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString(),
                Nombre = nombre,
                Email = email,
                Verificado = false
            };

            lock (bloqueo)
            {
                registros.Add(registro);
            }

            return registro;
        }

        public Registro? BuscarPorId(string id)
        {
            lock (bloqueo)
            {
                return registros.FirstOrDefault(r => r.Id == id);
            }
        }

        public Registro? Verificar(string token)
        {
            lock (bloqueo)
            {
                var registro = registros.FirstOrDefault(
                    r => r.Token == token
                );

                if (registro != null)
                {
                    registro.Verificado = true;
                }

                return registro;
            }
        }

        public bool EstaVerificado(string id)
        {
            lock (bloqueo)
            {
                return registros.Any(
                    r => r.Id == id && r.Verificado
                );
            }
        }
    }
}
