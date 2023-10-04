using EFCorePeliculas;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

//!=>Referencia [1] [Secci�n 02, 022. Almacenando la Ubicaci�n de un Cine - Datos Espaciales]
//!=>Referencia [2] [Secci�n 03, 036. Un Simple Query]
//!=>Referencia [3] [Secci�n 03, 037. Queries M�s R�pidos con AsNoTracking]
//!=>Referencia [4] [Secci�n 03, 043. Ah�rrate el Select con AutoMapper]
//!=>Referencia [5] [Secci�n 03, 046. Eager Loading - Include - Cargando Data Relacionada]
//!=>Referencia [6] [Secci�n 03, 052. Lazy Loading - Carga Perezosa]

var builder = WebApplication.CreateBuilder(args);

//! Add services to the container.

builder.Services.AddControllers()
    //!=>[5] SOLUCIONA EL CICLO INFINITO QUE SE GENERA AL CARGAR DATOS RELACIONADOS CON EAGER LOADING
    .AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//!=>[2] REGISTRA UNA INSTANCIA DEL ApplicationDbContext EN EL SISTEMA DE INYECCI�N DE DEPENDENCIAS
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
	opciones.UseSqlServer(connectionString,
        //!=>[1] SE CONFIGURA EFCore PARA QUE UTILICE NetTopologySuite
        sqlServer => sqlServer.UseNetTopologySuite()
	);

    //!=>[3] CONFIGURAMOS DE MANERA GLOBAL QUERIES DE SOLO LECTURA
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    //!=>[6] LE INDICAMOS A LA APLICACI�N QUE UTILIZAR� LAZY LOADING PARA CARGAR DATA RELACIONADA
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
