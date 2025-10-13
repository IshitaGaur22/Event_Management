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
        public DbSet<Event_Management.Models.Booking> Booking { get; set; } = default!;
        public DbSet<Event_Management.Models.Ticket> Ticket { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Booking → Ticket
        //    modelBuilder.Entity<Booking>()
        //        .HasOne(b => b.Ticket)
        //        .WithMany()
        //        .HasForeignKey(b => b.TicketId)
        //        .OnDelete(DeleteBehavior.Restrict); // 👈 Prevent cascade delete

        //    // Booking → Event
        //    modelBuilder.Entity<Booking>()
        //        .HasOne(b => b.Event)
        //        .WithMany()
        //        .HasForeignKey(b => b.EventId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    // Booking → User
        //    modelBuilder.Entity<Booking>()
        //        .HasOne(b => b.User)
        //        .WithMany()
        //        .HasForeignKey(b => b.UserId)
        //        .OnDelete(DeleteBehavior.Restrict);

        // Ticket → Event
        //modelBuilder.Entity<Ticket>()
        //    .HasOne(t => t.Event)
        //    .WithMany()
        //    .HasForeignKey(t => t.EventId)
        //    .OnDelete(DeleteBehavior.Restrict);
        //}
    }
}
