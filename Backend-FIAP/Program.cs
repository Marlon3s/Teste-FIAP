using Backend_FIAP.Data;
using Backend_FIAP.Repository.Alunos;
using Backend_FIAP.Repository.Relacao;
using Backend_FIAP.Repository.Turmas;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<FiapDbContext>((services, options) =>
{
    IConfiguration configuration = services.GetRequiredService<IConfiguration>();
    string connectionString = configuration.GetConnectionString("DataBase");
    options.UseSqlServer(connectionString);
});
builder.Services.AddTransient<IDbConnection>(provider =>
{
    IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
    string connectionString = configuration.GetConnectionString("DataBase");
    return new SqlConnection(connectionString);
});
builder.Services.AddScoped<IAlunos, AlunosRepository>();
builder.Services.AddScoped<ITurmas, TurmaRepository>();
builder.Services.AddScoped<IRelacao, RelacaoRepository>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
