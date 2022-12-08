using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTickets.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public String? UserId { get; set; }
        public IdentityUser? User { get; set; }
        public DateTime BookingDate{ get; set; }
        public int BusId { get; set; }
        public Bus? Bus { get; set; }
        public SeatDetails? Seat { get; set; }

    }
}
