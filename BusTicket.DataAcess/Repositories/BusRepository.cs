using BusTicket.DataAcess.Infrastructure;
using BusTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.DataAcess.Repositories
{
    public class BusRepository:GenericRepository<Bus>, IBusRepository
    {
        private readonly BusApplicationDBContext _context;
        public BusRepository(BusApplicationDBContext context):base(context)
        {
            _context = context;

        }
        public void Update(Bus bus)
        {
            var busfromDb = _context.Bus.FirstOrDefault(x => x.Id == bus.Id);
            if (busfromDb != null)
            {
                busfromDb.BusNumber = bus.BusNumber;
                busfromDb.SeatCapacity = bus.SeatCapacity;
            }


        }
    }
}
