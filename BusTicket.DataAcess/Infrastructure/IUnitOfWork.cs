 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.DataAcess.Infrastructure
{
    public interface IUnitOfWork
    {
        IBusRepository BusRepository { get; }
        ISeatRepository SeatRepository { get; }
        IBookingRepository BookingRepository { get; }
        void save();
    }
}
