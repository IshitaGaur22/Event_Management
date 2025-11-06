using Event_Management.Data;
using Event_Management.ExceptionHandlers;
using Event_Management.Exceptions;
using Event_Management.Repository;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args); 
//Createbuilder method initializes a new instance of the WebApplicationBuilder class with preconfigured defaults.


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());//to convert enum values to their string representation in JSON responses
    });

builder.Services.AddAuthentication(options =>//to set up authentication services
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//specifies the default authentication scheme to be used by the application
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});


builder.Services.AddEndpointsApiExplorer();//to configure services for API endpoint exploration and documentation generation.
builder.Services.AddSwaggerGen();//to generate Swagger/OpenAPI documentation for the API.

builder.Services.AddDbContext<Event_ManagementContext>(options =>//to configure the database context for the application
    options.UseSqlServer(builder.Configuration.GetConnectionString("Event_ManagementContext")));

builder.Services.AddScoped<IEventRepository, EventRepository>();
//to register the EventRepository class as the implementation of the IEventRepository interface in the dependency injection container.
builder.Services.AddScoped<IEventService, EventService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddScoped<IBookingHistoryRepository, BookingHistoryRepository>();
builder.Services.AddScoped<IBookingHistoryService, BookingHistoryService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IEmailService, EmailService>();
//why not add transient?
//because we want to maintain a single instance of the email service throughout the request lifecycle.


var app = builder.Build();//Builds the WebApplication instance using the configured services and middleware.

if (app.Environment.IsDevelopment())//to check if the application is running in a development environment
{
    app.UseSwagger();//to enable middleware for serving the generated Swagger as a JSON endpoint.
    app.UseSwaggerUI();
//to enable middleware for serving the Swagger UI, which provides a web-based interface for exploring and testing the API endpoints.
}

app.UseHttpsRedirection();//to redirect HTTP requests to HTTPS.

app.UseAuthorization();//to enable authorization middleware, which checks if the user is authorized to access certain resources.

app.MapControllers();//to map controller routes to the corresponding controller actions.

app.UseMiddleware<ExceptionMiddleware>();//to add custom exception handling middleware to the application's request pipeline.

app.Run();//to run the application and start listening for incoming HTTP requests.
