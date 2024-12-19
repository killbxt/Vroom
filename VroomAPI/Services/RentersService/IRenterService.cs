using VroomAPI.Models.Renters;
using VroomAPI.Services.RentersService.RentersDTO;

namespace VroomAPI.Services.RentersService
{
    public interface IRenterService
    {
        public Task<IResult> AddRenterAsync(RenterCreateDTO renter);
        public Task<RenterDTO>? GetRenterAsync(int id);
        public Task<IEnumerable<RenterDTO>>? GetRentersAsync();
        public Task<IResult> UpdateRenterAsync(int id, RenterUpdateDTO renterUpdateDTO);
        public Task<IResult> RemoveRenterAsync(int id);
    }
}
