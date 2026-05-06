using Fiap.Banco.API.Data;
using Fiap.Banco.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("Oracle")
    )
);

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IContratacaoService, ContratacaoService>();
builder.Services.AddScoped<IAgenciaService, AgenciaService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();