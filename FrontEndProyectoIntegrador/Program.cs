using Microsoft.Extensions.Configuration;
using PRY.DataAcces.Interfaces;
using PRY.DataAcces.Servicios;
using PRY.Domain.Context;
using PRY.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddSingleton<Connection>();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddScoped<IRestauranteService, RestauranteService>();
builder.Services.AddScoped<IInteresadosService, InteresadosService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                    builder => builder.AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()));

var app = builder.Build();




app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("AllowWebapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
