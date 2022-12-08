using BusTickets.Models.CustomValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTickets.Models.ViewModel
{
    public class BookingViewModel
    {
        [CustomDate(ErrorMessage="Select other date")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        public string BusNumber { get; set; }
        public List<SelectListItem> SeatBooking{ get; set; }

    }
}
