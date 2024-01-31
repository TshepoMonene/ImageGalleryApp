using ImageGalleryAPI.Contracts;
using ImageGalleryAPI.Models;
using ImageGalleryAPI.Repository;
using ImageGalleryAPI.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add DbContext
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//PhotoRepository
builder.Services.AddScoped<IPhotosRepository, PhotoRepository>();
//Repository Manager
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
//add cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
//IdentityContext
builder.Services.AddIdentityCore<UserModel>(option => 
{
    option.Password.RequireNonAlphanumeric = false;
    option.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<DataContext>()
  .AddDefaultTokenProviders();
//UserManager
//builder.Services.AddScoped<UserManager<IdentityUser>, UserManager<IdentityUser>>();
//authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["jwt:Issuer"],
            ValidAudience = builder.Configuration["jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"]))
        };
    });

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//SeedUsers
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
try
{
    var datacontext = service.GetRequiredService<DataContext>();
    var userManager = service.GetRequiredService<UserManager<UserModel>>();
    await datacontext.Database.MigrateAsync();
    await SeedUsers.SeedData(datacontext,userManager);
}
catch (System.Exception)
{

    throw;
}



app.Run();
