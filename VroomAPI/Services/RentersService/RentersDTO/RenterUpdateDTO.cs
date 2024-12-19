using VroomAPI.Models.Renters;

namespace VroomAPI.Services.RentersService.RentersDTO
{
    public class RenterUpdateDTO
    {
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public bool HasDriverLicence { get; set; }
        public string? DriverLicenseNumber { get; set; }
    }
}