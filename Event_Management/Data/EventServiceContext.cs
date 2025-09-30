using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Event_Management.Models;

namespace EventManagement.Data
{
    public class EventServiceContext : DbContext
    {
        public EventServiceContext(DbContextOptions<EventServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Event_Management.Models.Event> Event { get; set; } = default!;
    }
}
