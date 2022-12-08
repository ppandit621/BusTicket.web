using BusTicket.DataAcess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.DataAcess.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private BusApplicationDBContext _context;
        public UnitOfWork(BusApplicationDBContext context)
        {
            _context = context;
            BusRepository = new BusRepository(_context);
            SeatRepository = new SeatRepository(_context);
            BookingRepository = new BookingRepository(_context);
        }
        public IBusRepository BusRepository { get; private set; }
        public ISeatRepository SeatRepository { get; private set; }
        public IBookingRepository BookingRepository { get; private set; }
        public void save()
        {
            _context.SaveChanges();
        }

    }
}
