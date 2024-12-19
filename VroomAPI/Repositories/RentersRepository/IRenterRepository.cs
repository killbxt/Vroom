using VroomAPI.Models.Renters;
using VroomAPI.Services.RentersService.RentersDTO;

namespace VroomAPI.Repositories.RentersRepository
{
    public interface IRenterRepository
    {
        public Task<Renter?> GetRenterAsync(int id);
        public Task<IEnumerable<Renter>?> GetRentersAsync();
        public Task<Renter> AddRenterAsync(Renter renter);
        public Task<bool> UpdateRenterAsync(int id,RenterUpdateDTO renter);
        public Task<bool> RemoveRenterAsync(int id);
    }
}
