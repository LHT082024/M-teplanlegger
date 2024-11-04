using Microsoft.EntityFrameworkCore;
using Møteplanlegger;
using Møteplanlegger.models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContextClass>(opt => opt.UseInMemoryDatabase("DbContextClass"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

//these two get lines defines the route for the GET request It will take the People/Meeting object from the dbcontextclass
//Then convert the objects to a list of People/Meeting-objects and then return the list
app.MapGet("/People", async (DbContextClass db) =>
  await db.peoples.ToListAsync());

app.MapGet("/Meeting", async (DbContextClass db) =>
await db.meetings.ToListAsync());

app.MapPost("")
app.Run();

