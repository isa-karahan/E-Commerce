using Bussiness;
using Core;
using Core.Middlewares.Exceptions;
using Core.Middlewares.Logging;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices();
builder.Services.AddBusinessServices();

builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
      };
      options.Events = new JwtBearerEvents
      {
          OnMessageReceived = context =>
          {
              context.Token = context.Request.Cookies["accessToken"];
              return Task.CompletedTask;
          }
      };
  }
);

builder.Services.AddDbContext<ShoppingSystemContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr"))
    );

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyHeader()
            .SetIsOriginAllowed(host => true)
            .AllowAnyMethod()
            .AllowCredentials());

app.UseMiddleware<LoggerMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
