using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using PF_Pach_OS.Services;
using System.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;
using PF_Pach_OS.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterApp",
        builder => builder.WithOrigins("http://localhost:55946"));
});

builder.Services.AddDbContext<Pach_OSContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Pach_OSContext>();

builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

// Configurar las peticiones HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Usa CORS
app.UseCors("AllowFlutterApp");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "generarInforme",
        pattern: "Estadisticas/GenerarInformeVentas",
        defaults: new { controller = "Estadisticas", action = "GenerarInformeVentas" });
});

// Ignora la autenticaci�n para la ruta /Compras/GetCompras
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/Compras/GetCompras"),
    appBuilder =>
    {
        appBuilder.UseAuthentication();
        appBuilder.UseAuthorization();
    });

// Ignora la autenticaci�n para la ruta /Compras/GetDetallesCompra
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/Compras/GetDetallesCompra"),
    appBuilder =>
    {
        appBuilder.UseAuthentication();
        appBuilder.UseAuthorization();
    });


// Ignora la autenticaci�n para la ruta /Compras/CompraApi
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/Compras/CompraApi"),
    appBuilder =>
    {
        appBuilder.UseAuthentication();
        appBuilder.UseAuthorization();
    });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

