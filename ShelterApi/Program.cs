using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ShelterApi.Models;

namespace ShelterApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Adatbázis kapcsolat regisztrálása
            var connectionString = builder.Configuration.GetConnectionString("Mysql");
            builder.Services.AddDbContext<AnimalshelterContext>(options =>
                options.UseMySQL(connectionString!));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
