using Event_Management.Auth;
using Event_Management.Auth;
using Event_Management.Data;
using Event_Management.ExceptionHandlers;
using Event_Management.Exceptions;
using Event_Management.Hubs;
using Event_Management.Repository;
using Event_Management.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());//to convert enum values to their string representation in JSON responses
    });

builder.Services.AddCors(options=>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
        
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//specifies the default authentication scheme to be used by the application
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        RoleClaimType = ClaimTypes.Role 
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"].FirstOrDefault();
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Event_ManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Event_ManagementContext")));

builder.Services.AddSignalR();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddHostedService<BookingStatusUpdater>();

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
//addScoped we want to maintain a single instance of the email service throughout the request lifecycle.


var app = builder.Build();//Builds the WebApplication instance using the configured services and middleware.

if (app.Environment.IsDevelopment())//to check if the application is running in a development environment
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();//to enable middleware for serving the generated Swagger as a JSON endpoint.
    app.UseSwaggerUI();
//to enable middleware for serving the Swagger UI, which provides a web-based interface for exploring and testing the API endpoints.
}

app.UseHttpsRedirection();//to redirect HTTP requests to HTTPS.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();//to map controller routes to the corresponding controller actions.

app.MapHub<NotificationHub>("/notificationHub");

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("MyCorsPolicy");

app.Run();//to run the application and start listening for incoming HTTP requests.
