using FluentValidation;
using VroomAPI.Models.Renters;
using VroomAPI.Repositories.RentersRepository;
using Microsoft.AspNetCore.Identity;
using VroomAPI.Services.RentersService.RentersDTO;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VroomAPI.Services.RentersService
{
    public class RenterService : IRenterService
    {
        private readonly IRenterRepository _renterRepository;
        private readonly IValidator<Renter> _renterValidator;
        private readonly ILogger<RenterService> _renterLogger;
        private readonly IPasswordHasher<Renter> _passwordHasher;
        public RenterService(IRenterRepository renterRepository, IValidator<Renter> renterValidator, ILogger<RenterService> renterLogger, IPasswordHasher<Renter> passwordHasher)
        {
            _renterRepository = renterRepository;
            _renterValidator = renterValidator;
            _renterLogger = renterLogger;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Асинхронный метод добавления сервиса валидирующий модель арендатора и хэширующий ее данные
        /// </summary>
        /// <param name="renterCreateDTO">Модель арендатора прилетевшая с приложения</param>
        /// <returns>Код http запроса</returns>
        /// 
        public async Task<IResult> AddRenterAsync(RenterCreateDTO renterCreateDTO)
        {
            var newRenter = (Renter)renterCreateDTO;

            newRenter.Password = _passwordHasher.HashPassword(null!, renterCreateDTO.Password);
            newRenter.Rating = 5.0m;
            var validationResult = await _renterValidator.ValidateAsync(newRenter);

            if(validationResult.IsValid)
            {
                try
                {
                    var createdRenter = await _renterRepository.AddRenterAsync(newRenter);
                    _renterLogger.LogInformation($"New renter with id {createdRenter.Id} was created.");
                    return Results.Created($"/api/renters/{createdRenter.Id}", createdRenter);
                }
                catch (Exception ex)
                {
                    _renterLogger.LogError(ex, $"Error while adding new renter.");
                    return Results.BadRequest($"Error while adding new renter.");
                }
            }
            else if(!validationResult.IsValid) 
            {
                Dictionary<string, string[]> validationErrors = new Dictionary<string, string[]>();
                
                foreach (var error in validationResult.Errors)
                {
                    _renterLogger.LogWarning($"Validation failed for new renter in property {error.PropertyName} with error: {error.ErrorMessage}");
                    validationErrors.Add(error.PropertyName, [error.ErrorCode, error.ErrorMessage]);
                }
                return Results.ValidationProblem(validationErrors);
            }
            return Results.BadRequest();
        }

        /// <summary>
        /// Асинхронный метод сервиса возвращающий арендатора по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор арендатора</param>
        /// <returns>Модель арендатора</returns>
        public async Task<RenterDTO>? GetRenterAsync(int id)
        {
            var renter = await _renterRepository.GetRenterAsync(id);

            if (renter!= null)
            {
               return (RenterDTO)renter;
            }
            else
            {
                return null!;
            }
        }

        /// <summary>
        /// Асинхронный метод сервиса возвращающий всех арендаторов
        /// </summary>
        /// <returns>Перечисление арендаторов</returns>
        public async Task<IEnumerable<RenterDTO>>? GetRentersAsync()
        {
            var renters = await _renterRepository.GetRentersAsync();
            if (renters != null)
            {
                return renters.Select(renter => (RenterDTO)renter);
            }
            else 
            {
                return Enumerable.Empty<RenterDTO>();
            }
        }

        /// <summary>
        /// Асинхронный метод сервиса удалющий арендатора
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> RemoveRenterAsync(int id)
        {
           
            var renter = await _renterRepository.RemoveRenterAsync(id);
            if (!renter)
            {
                return Results.NotFound();
            }
            else
            {
                _renterLogger.LogWarning("Renter has removed");
                return Results.Ok();
            }
        }

         /// <summary>
        /// Асинхронный метод сервиса обновляющий арендатора
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> UpdateRenterAsync(int id, RenterUpdateDTO renterUpdateDTO)
        {
            var result = await _renterRepository.UpdateRenterAsync(id, renterUpdateDTO);
            if (result)
            {
                _renterLogger.LogWarning("отработал в репозитории успешно");
                return Results.Ok(result);
            }
            else
            {
                _renterLogger.LogWarning("отработал в репозитории позорно");
                return Results.NotFound(result);
            }
        }
    }
}
