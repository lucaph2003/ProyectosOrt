using Aplicacion.CUCabania;
using Aplicacion.CUMantenimiento;
using Aplicacion.CUTipo;
using Aplicacion.CUUsuario;
using Datos.EF;
using Datos.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Negocio.InterfacesRepositorios;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////////////////// JWT ///////////////////////////////////
var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE="; 

builder.Services.AddAuthentication(aut =>
{
    aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(aut =>
{
    aut.RequireHttpsMetadata = false;
    aut.SaveToken = true;
    aut.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
//////////////////// FIN JWT ////////////////////////


//Automatizamos la inyeccion de dependencias
builder.Services.AddScoped<IRepositorioCabania, RepositorioCabania>();
builder.Services.AddScoped<IRepositorioMantenimiento, RepositorioMantenimiento>();
builder.Services.AddScoped<IRepositorioTipo, RepositorioTipo>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioParametro, RepositorioParametro>();

builder.Services.AddScoped<IAltaCabania, AltaCabania>();
builder.Services.AddScoped<IBuscarCabania, BuscarCabania>();
builder.Services.AddScoped<IListadoCabanias, ListadoCabanias>();
builder.Services.AddScoped<IAltaMantenimiento, AltaMantenimiento>();
builder.Services.AddScoped<IListadoMantenimientos, ListadoMantenimientos>();
builder.Services.AddScoped<IAltaTipo, AltaTipo>();
builder.Services.AddScoped<IBuscarTipo, BuscarTipo>();
builder.Services.AddScoped<IEliminarTipo, EliminarTipo>();
builder.Services.AddScoped<IListadoTipos, ListadoTipos>();
builder.Services.AddScoped<IModificarTipo, ModificarTipo>();
builder.Services.AddScoped<IAltaUsuario, AltaUsuario>();
builder.Services.AddScoped<ILoginUsuario, LoginUsuario>();


//Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//Configuramos la conexion a la base de datos
var configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddJsonFile("appsettings.json", false, true);
var configuration = configurationBuilder.Build();

string strCon = configuration.GetConnectionString("MiConexion");
builder.Services.AddDbContext<HotelContext>(options => options.UseSqlServer(strCon));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Usar la Session
app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
