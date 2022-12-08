using BusTicket.DataAcess.Infrastructure;
using BusTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.DataAcess.Repositories
{
    public class  BookingRepository:GenericRepository<Booking>,IBookingRepository
    {
        private readonly BusApplicationDBContext _context;

        public BookingRepository(BusApplicationDBContext context) : base(context)
        {
            _context = context;

        }
        public void Update(Booking book)
        {
            var BookFromDb = _context.Booking.FirstOrDefault(x => x.Id == book.Id);
            if (BookFromDb != null)
            {
                BookFromDb.SeatId = book.SeatId;
                BookFromDb.BusId = book.BusId;
                BookFromDb.BookingDate = book.BookingDate;

            }
        }
    }
}
