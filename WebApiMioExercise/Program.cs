using Microsoft.EntityFrameworkCore;
using WebApiMioExercise.Models;

namespace WebApiMioExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://example.com",
                            "http://www.contoso.com",
                            "http://localhost:3000");

                    });
            });
            // Add services to the container.

            builder.Services.AddDbContext<ProductsDb>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("productsDb")));

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

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}