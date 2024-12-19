using VroomAPI.Models.Renters;

namespace VroomAPI.Services.RentersService.RentersDTO
{
    public class RenterCreateDTO
    {
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        public bool HasDriverLicence { get; set; }
        public string? DriverLicenseNumber { get; set; }

        public static explicit operator Renter(RenterCreateDTO renterCreateDTO)
        {
            return new Renter
            {
                FirstName = renterCreateDTO.FirstName,
                MiddleName = renterCreateDTO.MiddleName,
                LastName = renterCreateDTO.LastName,
                Email = renterCreateDTO.Email,
                Phone = renterCreateDTO.Phone,
                Password = renterCreateDTO.Password,
                HasDriverLicence = renterCreateDTO.HasDriverLicence,
                DriverLicenseNumber = renterCreateDTO.DriverLicenseNumber,
                DateOfBirth = renterCreateDTO.DateOfBirth,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
            };
        }   
    }
}
