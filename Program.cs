using ContactsAPI.Data;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<ContactsAPIDbContext>(options => options.UseInMemoryDatabase("ContactDb"));
builder.Services.AddDbContext<ContactsAPIDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add the 'guid' constraint manually
//builder.Services.Configure<RouteOptions>(options =>
//{
//    options.ConstraintMap.Add("guid", typeof(GuidRouteConstraint));
//});

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
