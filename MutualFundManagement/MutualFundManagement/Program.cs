using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MutualFundManagement.Models;
using MutualFundManagement.Services;
using Serilog;
using Serilog.Formatting.Json;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CustomerFundsService>();
builder.Services.AddScoped<MutualFundBanksService>();

builder.Host.UseSerilog((HostBuilder, loggerConf) => loggerConf.WriteTo.Console());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description ="Standard Authorizartion using Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey


    });


    options.OperationFilter<SecurityRequirementsOperationFilter>();
});




var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
builder.Services.AddDbContext<MutualFundDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("SqlDevEnv"), serverVersion);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters { 
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my top secret key for jwt token")) ,
      ValidateIssuer = false,
      ValidateAudience = false

    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
