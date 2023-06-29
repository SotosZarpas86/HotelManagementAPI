using Anixe.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Anixe.Infrastructure
{
    public class AnixeProjectContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AnixeProjectContext(DbContextOptions<AnixeProjectContext> options)
          : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}
