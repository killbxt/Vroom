
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VroomAPI.Data;
using VroomAPI.Models.Renters;
using VroomAPI.Repositories.RentersRepository;
using VroomAPI.Services.RentersService;
using VroomAPI.Services.Validators;

namespace VroomAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //бд
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<VroomDbContext>(options =>
              options.UseSqlServer(connectionString) 
              .EnableSensitiveDataLogging(false)
            );

            //зависимости для Renter
            builder.Services.AddScoped<IRenterRepository, RenterRepository>();
            builder.Services.AddScoped<IValidator<Renter>,RenterValidator>();
            builder.Services.AddScoped<IPasswordHasher<Renter>, PasswordHasher<Renter>>();
            builder.Services.AddScoped<IRenterService, RenterService>();
           
            //валидаторы
            builder.Services.AddValidatorsFromAssemblyContaining<RenterValidator>();

            // Настройка CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                    policyBuilder =>
                    {
                        policyBuilder.WithOrigins("https://192.168.1.42:7245", "http://localhost:5001", "https://localhost:7148")//Замените на ваш адрес
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });



            var app = builder.Build();
            app.UseRouting(); // Make sure this is before UseCors - помощь для него 
            app.UseCors("MyAllowSpecificOrigins");  //  Включаем CORS - штука для разрешения и переадресаций как я понял

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
 