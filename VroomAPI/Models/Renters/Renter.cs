using System.ComponentModel.DataAnnotations;

namespace VroomAPI.Models.Renters
{
    public class Renter
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public decimal? Rating { get; set; }
        public bool HasDriverLicence { get; set; }
        public string? DriverLicenseNumber {  get; set; }
        public bool IsVerified { get; set; }
    }
}
