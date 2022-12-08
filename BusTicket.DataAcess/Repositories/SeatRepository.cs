using BusTicket.DataAcess.Infrastructure;
using BusTickets.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.DataAcess.Repositories
{
    public class SeatRepository:GenericRepository<SeatDetails>,ISeatRepository
    {
        private readonly BusApplicationDBContext _context;
        
        public SeatRepository (BusApplicationDBContext context):base(context)
        {
            _context = context;
            
        }

        public List<SeatDetails> GetByBusId(int id)
        {
            var SeatFromDb = _context.SeatDetails.Where(x => x.BusId == id).Include("Bus");
            return SeatFromDb.ToList();
        }
        public void Update(SeatDetails seat)
        {
            var SeatFromDb = _context.SeatDetails.FirstOrDefault(x => x.Id == seat.Id);
            if (SeatFromDb!=null)
            {
                SeatFromDb.SeatNo=seat.SeatNo;
                SeatFromDb.BusId = seat.BusId;
                SeatFromDb.Description = seat.Description;
            }
        }
    }
}
