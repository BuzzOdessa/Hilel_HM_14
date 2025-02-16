using System.Reflection;
using _5Layers.Animals.Persistence.EFCore.AnimalsDb;
using Animals.Application;
using Animals.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(
        options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Додати базові операції над власником тварини (Owner)",
                Description = "Домашка номер 14"
            });

            // using System.Reflection;
            // Где-то надо указать шоб студия генерила хмл по комментам из исходников
            //<GenerateDocumentationFile>true</GenerateDocumentationFile>  в Animals.Api.csproj
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            xmlFilename = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            if (File.Exists(xmlFilename))
                options.IncludeXmlComments(xmlFilename);
        }

);


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.RegisterAnimalsDbContext(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
