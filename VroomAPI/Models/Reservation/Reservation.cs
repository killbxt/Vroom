using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VroomAPI.Models.Cars;
using VroomAPI.Models.Renters;

namespace VroomAPI.Models.Reservation
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RenterId")]
        public required int RenterId { get; set; }
        public required Renter Renter { get; set; }

        [ForeignKey("CarId")]
        public required int CarId { get; set; }
        public required Car Car { get; set; }

        public required DateTime StartDate {  get; set; }
        public DateTime? EndDate { get; set; }

        public decimal RentalCost {  get; set; }

        public RentalStatus RentalStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
       
    }
    public enum RentalStatus
    {
        Active,
        Done,
        Declined
    }
    public enum PaymentStatus
    {
        Paid,
        WaitForPaid,
        NotPaid
    }
}
