using Event_Management.DTOs;
using Event_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Management.Data
{
    public class Event_ManagementContext : DbContext
    {
        public Event_ManagementContext(DbContextOptions<Event_ManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Event_Management.Models.Event> Event { get; set; } = default!;
        public DbSet<Event_Management.Models.Category> Category { get; set; } = default!;
        public DbSet<Event_Management.Models.Booking> Booking { get; set; }= default!;
        public DbSet<Event_Management.Models.User> User { get; set; } = default!;
        public DbSet<Event_Management.Models.Payment> Payment { get; set; } = default!;
        public DbSet<Event_Management.Models.Notification> Notification { get; set; }=default!;
        public DbSet<Event_Management.Models.Feedback> Feedback { get; set; } = default!;
        public DbSet<Event_Management.Models.Replies> Replies { get; set; } = default!;
        public DbSet<EventRevenueDto> EventRevenueDto { get; set; } = default;

    }
}