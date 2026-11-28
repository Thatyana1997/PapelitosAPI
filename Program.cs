using Microsoft.EntityFrameworkCore;
using PapelitosAPI;
using PapelitosAPI.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionU")));

builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<TiposIDRepository>();
builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<FormasPagoRepository>();
builder.Services.AddScoped<DistritoRepository>();
builder.Services.AddScoped<RolRepository>();
builder.Services.AddScoped<ParametroRepository>();
builder.Services.AddScoped<CantonRepository>();
builder.Services.AddScoped<ProvinciaRepository>();
builder.Services.AddScoped<FacturaRepositorio>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<Telefonorepositorio>();
builder.Services.AddScoped<EmisorRepositorio>(); //tuve que completar 
builder.Services.AddScoped<InventarioRepository>();
builder.Services.AddScoped<DetalleFacturaRepository>();



builder.Services.AddControllers();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() // Permite cualquier origen
                   .AllowAnyMethod() // Permite cualquier método (GET, POST, etc.)
                   .AllowAnyHeader(); // Permite cualquier encabezado
        });
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
