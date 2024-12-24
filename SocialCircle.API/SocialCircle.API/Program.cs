using Microsoft.EntityFrameworkCore;
using SocialCircle.API.ApplicationContact;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var Connection = builder.Configuration.GetConnectionString("Dbsql");
builder.Services.AddDbContext<SocialCircleContext>(option => option.UseSqlServer(Connection));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// user services
//builder.Services.AddScoped<IUserRepository, UserRepository>();

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
