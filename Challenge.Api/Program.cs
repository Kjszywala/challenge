using Challenge.DbServices.Models;
using Challenge.BusinessLogic.Interfaces;
using Challenge.BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DatabaseContext>(
                options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("EskinsDbContext"))
                );

            // Dependency injection here.
            builder.Services.AddScoped<IPostCodeLogic, PostCodeLogic>();

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
        }
    }
}