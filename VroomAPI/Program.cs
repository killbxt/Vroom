
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
        }
    }
}
 