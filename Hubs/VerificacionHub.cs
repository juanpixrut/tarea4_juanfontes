
using Microsoft.AspNetCore.SignalR;
using tarea4_juanfontes.Services;

namespace tarea4_juanfontes.Hubs
{
    public class VerificacionHub : Hub
    {
        private readonly RegistroService registroService;

        public VerificacionHub(
            RegistroService registroService)
        {
            this.registroService = registroService;
        }

        public async Task<bool> UnirseAVerificacion(string id)
        {
            var registro = registroService.BuscarPorId(id);

            if (registro == null)
            {
                throw new HubException(
                    "No se encontró el registro"
                );
            }

            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                id
            );

            return registroService.EstaVerificado(id);
        }
    }
}
