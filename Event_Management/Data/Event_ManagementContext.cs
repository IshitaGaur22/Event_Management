using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Event_Management.Models;

namespace Event_Management.Data
{
    public class Event_ManagementContext : DbContext
    {
        public Event_ManagementContext(DbContextOptions<Event_ManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Event_Management.Models.Event> Event { get; set; } = default!;
        public DbSet<Event_Management.Models.Ticket> Ticket { get; set; } = default!;
        public DbSet<Event_Management.Models.Category> Category { get; set; } = default!;
        public DbSet<Event_Management.Models.Booking> Booking { get; set; }= default!;
        public DbSet<Event_Management.Models.User> User { get; set; } = default!;
        public DbSet<Event_Management.Models.Payment> Payment { get; set; } = default!;
    }
}