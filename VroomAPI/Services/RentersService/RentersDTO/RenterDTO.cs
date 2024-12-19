using VroomAPI.Models.Renters;

namespace VroomAPI.Services.RentersService.RentersDTO
{
    public class RenterDTO
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; }
        public decimal? Rating { get; set; }

        public static explicit operator RenterDTO(Renter renter)
        {
            return new RenterDTO
            {
                Rating = renter.Rating,
                FirstName = renter.FirstName,
                MiddleName = renter.MiddleName,
                LastName = renter.LastName,
                Email = renter.Email,
                DateOfBirth = renter.DateOfBirth,
            };
        }
    }
}
