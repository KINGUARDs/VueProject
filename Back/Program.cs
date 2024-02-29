
using Back.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Back
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var MyAllowSpecifications = "AllowAny";
            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                        name: MyAllowSpecifications,
                        policy => policy.WithOrigins("https://localhost:7113").WithHeaders("*").WithMethods("*")
                    );
            });
            builder.Services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Northwind")));

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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
                RequestPath = "/StaticFiles"

            });
            app.MapControllers();

            app.Run();
        }
    }
}
