
using tarea4_juanfontes.Hubs;
using tarea4_juanfontes.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar MVC
builder.Services.AddControllersWithViews();

// Agregar SignalR
builder.Services.AddSignalR();

// Servicio para guardar registros en memoria
builder.Services.AddSingleton<RegistroService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Registro}/{action=Index}/{id?}"
);

// Ruta del Hub
app.MapHub<VerificacionHub>("/verificacionHub");

app.Run();
