using Microsoft.EntityFrameworkCore;
using Møteplanlegger;
using Møteplanlegger.models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContextClass>(opt => opt.UseInMemoryDatabase("DbContextClass"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

//these two get methods defines the route for the GET request It will take the People/Meeting object from the dbcontextclass
//Then convert the objects to a list of People/Meeting-objects and then return the list
app.MapGet("/People", async (DbContextClass db) =>
  await db.peoples.ToListAsync());

app.MapGet("/Meeting", async (DbContextClass db) =>
await db.meetings.ToListAsync());

//These two get methods once again defines the route for the GET request
//but this time we can be more specific instead of getting a list of all the meetings/people
//we instead can get a specific meeting or person. 
app.MapGet("/People/{id}", async (int id, DbContextClass db) =>
await db.peoples.FindAsync(id)
is People people
? Results.Ok(people)
: Results.NotFound());

app.MapGet("/Meeting/{id}", async (int id, DbContextClass db) =>
await db.meetings.FindAsync(id)
is Meeting meeting
? Results.Ok(meeting)
: Results.NotFound());



app.Run();

