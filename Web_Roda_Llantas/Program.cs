using Web_Roda_Llantas.Interfaces;
using Web_Roda_Llantas.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuarioModel, UsuariosModel>();
builder.Services.AddScoped<IVehiculosModel, VehiculosModel>();
builder.Services.AddScoped<IProductosModel, ProductosModel>();
builder.Services.AddScoped<ICarritoModel, CarritoModel>();
builder.Services.AddScoped<IFacturaModel, FacturaModel>();
builder.Services.AddScoped<ITipoProductoModel, TipoProductoModel>();
builder.Services.AddScoped<IUtilitariosModel, UtilitariosModel>();
builder.Services.AddScoped<IProveedoresModel, ProveedoresModel>();
builder.Services.AddScoped<IBitacoraModel, BitacoraModel>();
builder.Services.AddScoped<IServiciosModel, ServiciosModel>();
builder.Services.AddScoped<IReservacionesModel, ReservacionesModel>();
builder.Services.AddScoped<ICompras, ComprasModel>();


var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
