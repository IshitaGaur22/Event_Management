using Event_Management.Data;
using Event_Management.Repository;
using Event_Management.Services;
using Event_Management.ExceptionHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    //options.InvalidModelStateResponseFactory = context =>
    //{

    //    if (!context.ModelState.IsValid &&
    //        context.ModelState.Values.All(v => v.Errors.Count > 0))
    //    {
    //        return new BadRequestObjectResult(new
    //        {
    //            error = "Value have not been entered, please enter values."
    //        });
    //    }

    //    return new BadRequestObjectResult(new
    //    {
    //        error = "Invalid model state.",
    //        details = context.ModelState
    //    });
    //};

    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                Field = e.Key,
                Messages = e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
            });

        return new BadRequestObjectResult(new
        {
            error = "Validation failed.",
            details = errors
        });
    };


});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Event_ManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Event_ManagementContext")));

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
