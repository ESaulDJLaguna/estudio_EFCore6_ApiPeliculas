using EFCorePeliculas;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

//!=>Referencia [1] [Sección 02, 022. Almacenando la Ubicación de un Cine - Datos Espaciales]
//!=>Referencia [2] [Sección 03, 036. Un Simple Query]
//!=>Referencia [3] [Sección 03, 037. Queries Más Rápidos con AsNoTracking]
//!=>Referencia [4] [Sección 03, 043. Ahórrate el Select con AutoMapper]
//!=>Referencia [5] [Sección 03, 046. Eager Loading - Include - Cargando Data Relacionada]
//!=>Referencia [6] [Sección 03, 052. Lazy Loading - Carga Perezosa]

var builder = WebApplication.CreateBuilder(args);

//! Add services to the container.

builder.Services.AddControllers()
    //!=>[5] SOLUCIONA EL CICLO INFINITO QUE SE GENERA AL CARGAR DATOS RELACIONADOS CON EAGER LOADING
    .AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//!=>[2] REGISTRA UNA INSTANCIA DEL ApplicationDbContext EN EL SISTEMA DE INYECCIÓN DE DEPENDENCIAS
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
	opciones.UseSqlServer(connectionString,
        //!=>[1] SE CONFIGURA EFCore PARA QUE UTILICE NetTopologySuite
        sqlServer => sqlServer.UseNetTopologySuite()
	);

    //!=>[3] CONFIGURAMOS DE MANERA GLOBAL QUERIES DE SOLO LECTURA
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    //!=>[6] LE INDICAMOS A LA APLICACIÓN QUE UTILIZARÁ LAZY LOADING PARA CARGAR DATA RELACIONADA
    //[6] opciones.UseLazyLoadingProxies();
});

//!=>[4] CONFIGURAMOS AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

//! Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
