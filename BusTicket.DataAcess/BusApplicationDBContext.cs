
using BusTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BusTicket.DataAcess
{
    public class BusApplicationDBContext : IdentityDbContext
    {
        public BusApplicationDBContext(DbContextOptions<BusApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<SeatDetails> SeatDetails { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Booking> Booking{ get; set; }


    }
}
