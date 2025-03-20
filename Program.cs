using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculas.Application.Mappers;
using SistemaMatriculas.Application.Services;
using SistemaMatriculas.Application.Validators;
using SistemaMatriculas.Domain.Cursos;
using SistemaMatriculas.Infraestructure;
using SistemaMatriculas.Infraestructure.Repository;

var builder = WebApplication.CreateBuilder(args);

/* DbContext */
builder.Services.AddDbContext<ApplicationDbContext>( options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});

/* Dependency inyection (services)*/
builder.Services.AddKeyedScoped<IService<CursoDto>, CursoService>("cursoService");

/* Repository */
builder.Services.AddScoped<IRepository<Curso>, CursoRepository>();

/* Mapper */
builder.Services.AddAutoMapper(typeof(MappingProfile));

/* Validator */
builder.Services.AddScoped<IValidator<CursoDto>, CursoValidation>();

// Add services to the container.

builder.Services.AddControllers();
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
