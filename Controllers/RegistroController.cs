
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using tarea4_juanfontes.Hubs;
using tarea4_juanfontes.Models;
using tarea4_juanfontes.Services;

namespace tarea4_juanfontes.Controllers
{
    public class RegistroController : Controller
    {
        private readonly RegistroService registroService;
        private readonly IHubContext<VerificacionHub> hubContext;

        public RegistroController(
            RegistroService registroService,
            IHubContext<VerificacionHub> hubContext)
        {
            this.registroService = registroService;
            this.hubContext = hubContext;
        }

        // Muestra el formulario de registro
        [HttpGet]
        public IActionResult Index()
        {
            return View(new RegistroInput());
        }

        // Procesa el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(RegistroInput datos)
        {
            if (!ModelState.IsValid)
            {
                return View(datos);
            }

            var registro = registroService.Crear(
                datos.Nombre,
                datos.Email
            );

            return RedirectToAction(
                nameof(Esperando),
                new { id = registro.Id }
            );
        }

        // Página que espera la verificación
        [HttpGet]
        public IActionResult Esperando(string id)
        {
            var registro = registroService.BuscarPorId(id);

            if (registro == null)
            {
                return NotFound();
            }

            return View(registro);
        }

        // Simula el enlace enviado por correo
        [HttpGet]
        public async Task<IActionResult> Confirmar(string token)
        {
            var registro = registroService.Verificar(token);

            if (registro == null)
            {
                return NotFound();
            }

            await hubContext.Clients
                .Group(registro.Id)
                .SendAsync("EmailVerificado");

            return View();
        }

        // Comprueba que la verificación existe
        [HttpGet]
        public IActionResult Bienvenido(string id)
        {
            var registro = registroService.BuscarPorId(id);

            if (registro == null || !registro.Verificado)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(registro);
        }
    }
}
