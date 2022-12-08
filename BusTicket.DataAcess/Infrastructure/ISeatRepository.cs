using BusTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.DataAcess.Infrastructure
{
    public interface ISeatRepository:IGenericRepository<SeatDetails>
    {
        void Update(SeatDetails seat);
        List<SeatDetails> GetByBusId(int id);
    }
}
