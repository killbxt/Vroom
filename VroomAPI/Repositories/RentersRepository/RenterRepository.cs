using Microsoft.EntityFrameworkCore;
using VroomAPI.Data;
using VroomAPI.Models.Renters;
using VroomAPI.Services.RentersService.RentersDTO;

namespace VroomAPI.Repositories.RentersRepository
{
    public class RenterRepository : IRenterRepository
    {
        private readonly VroomDbContext _context;
        private readonly ILogger _logger;
        public RenterRepository(VroomDbContext context, ILogger<RenterRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Асинхронный метод репозитория добавляющий одного арендатора
        /// </summary>
        /// <param name="renter">Объект арендатора</param>
        /// <returns>Добавленный арендатор. Объект арендатора</returns>
        public async Task<Renter> AddRenterAsync(Renter renter)
        {
            await _context.Renters.AddAsync(renter);
            await _context.SaveChangesAsync();
            return renter;
        }
        /// <summary>
        /// Асинхронный метод репозитория возврающий одного арендатора по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор арендатора</param>
        /// <returns>Найденый по идентификатору арендатор</returns>
        public async Task<Renter?> GetRenterAsync(int id)
        {
            var renter = await _context.Renters.FindAsync(id);
            if (renter != null)
            {
                return renter;
            }
            else 
            { 
                _logger.LogWarning($"Renter with id {id} not found.");
                return null;
            }
        }
        /// <summary>
        /// Асинхронный метод репозитория возвращающий всех имеющихся в базе арендаторов
        /// </summary>
        /// <returns>Все арендаторы</returns>
        public async Task<IEnumerable<Renter>?> GetRentersAsync()
        {
            var renters = await _context.Renters.ToListAsync();
            if(renters != null)
            {
                return renters;
            }
            else
            { 
                _logger.LogWarning($"No renters in the database");
                return null;
            }
        }
        /// <summary>
        /// Асинхронный метод репозитория удаляющий арендатора
        /// </summary>
        /// <param name="id">Идентификатор удаляемого арендатора</param>
        /// <returns>True - при успешном удалении. False - при ошибке</returns>
        public async Task<bool> RemoveRenterAsync(int id)
        {
            var renter = await GetRenterAsync(id);
            if(renter != null)
            {
                _context.Renters.Remove(renter);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                _logger.LogError($"Renter with id {id} not found");
                return false;
            }
        }
        /// <summary>
        /// Асинхронный метод репозитория обновляющий арендатора
        /// </summary>
        /// <param name="id">Идентификатор арендатора которого надо обновить</param>
        /// <param name="renter">Объект арендатора поля которого будут скопированны в найденного по id арендатора с первого параметра</param>
        /// <returns>True - при успешном обновлении. False - при ошибке</returns>
        public async Task<bool> UpdateRenterAsync(int id, RenterUpdateDTO renterUpdateDTO)
        {
            var existingRentor = await GetRenterAsync(id);

            if(existingRentor != null)
            {
                existingRentor.FirstName = renterUpdateDTO.FirstName;
                existingRentor.MiddleName = renterUpdateDTO.MiddleName;
                existingRentor.LastName = renterUpdateDTO.LastName;
                existingRentor.Email = renterUpdateDTO.Email;
                existingRentor.Phone = renterUpdateDTO.Phone;
                existingRentor.HasDriverLicence = renterUpdateDTO.HasDriverLicence;
                existingRentor.DriverLicenseNumber = renterUpdateDTO.DriverLicenseNumber;
                _context.Renters.Update(existingRentor);
                await _context.SaveChangesAsync();
                _logger.LogWarning("отработал в репозитории успешно");
                return true;
            }
            _logger.LogWarning("отработал в репозитории неуспешно");
            return false;
        }
    }
}
