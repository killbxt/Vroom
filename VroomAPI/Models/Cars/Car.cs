using System.ComponentModel.DataAnnotations;

namespace VroomAPI.Models.Cars
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public required string LicensePlate {  get; set; }
        public required string Make { get; set; } //марка
        public required string Model { get; set; }
        public double? Mileage { get; set; }
        public double? Fuel { get; set; }
        public required double Tank { get; set; }
        public required decimal DailyRate {  get; set; }
        public required decimal HourlyRate { get; set; }
        public required bool IsAvailable { get; set; }
    }
}
