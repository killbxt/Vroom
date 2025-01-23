using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VroomWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpClient();

            // Add services to the container.
            // builder.Services.AddControllersWithViews(); // Эта строка, вероятно, не нужна, если вы используете только Razor Pages

            builder.Services.AddRazorPages(); // Эта строка необходима!

            // Настройка CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                    policyBuilder =>
                    {
                        policyBuilder.AllowAnyOrigin() // Разрешить с любого источника
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("MyAllowSpecificOrigins"); // Use Cors.
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            app.MapRazorPages();

            app.Run();
        }
    }
}