using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EventManagement.Data;
using Event_Management.Repository;
using Event_Management.Services;
using Event_Management.ExceptionHandlers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EventServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Event_ManagementContext") ?? throw new InvalidOperationException("Connection string 'Event_ManagementContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*ilder.Services.AddDbContext<EventServiceContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("StudentServiceContext")));*/
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ExceptionHandlerAttribute>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddHostedService<NotificationBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
