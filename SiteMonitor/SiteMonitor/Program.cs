using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using SiteMonitor.BusinessLogicLayer.ConsumerDevice;
using SiteMonitor.BusinessLogicLayer.ConsumerService;
using SiteMonitor.BusinessLogicLayer.EnergyConsumptionBLL;
using SiteMonitor.BusinessLogicLayer.WebSocketService;
using SiteMonitor.DataLayer.Context;
using SiteMonitor.DataLayer.Mapper;
using SiteMonitor.DataLayer.Repository.EnergyConsumptionRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        build => build
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEnergyConsumptionService, EnergyConsumptionService>();
builder.Services.AddScoped<IEnergyConsumptionRepository, EnergyConsumptionRepository>();

builder.Services.AddHostedService<ConsumerService>();
builder.Services.AddHostedService<ConsumerDevice>();
builder.Services.AddSignalR();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
//builder.Services.AddHostedService<SensorConsumer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseCors("AllowAnyOrigin");

app.MapHub<SocketHub>("socket-hub");

app.Run();