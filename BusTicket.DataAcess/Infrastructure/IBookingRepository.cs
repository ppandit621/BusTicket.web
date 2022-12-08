using BusTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.DataAcess.Infrastructure
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        void Update(Booking bus);
    }
}
