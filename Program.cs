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

//this two methods creates a new people/meething object. First it identfies the path in the database
//then it creates the new people/meeting object and places it into it's assigned space
//in the database
app.MapPost("/People", async (People people, DbContextClass db) =>
{
    db.peoples.Add(people);
    await db.SaveChangesAsync();

    return Results.Created($"/People/{people.EmployeeNumber}", people);
});

app.MapPost("/Meeting", async (Meeting meeting, DbContextClass db) =>
{
    db.meetings.Add(meeting);
    await db.SaveChangesAsync();

    return Results.Created($"/People/{meeting.MeetingId}", meeting);
});


//these methods finds a meeting/person by id and then gives you the abilty to update that meeting/person
app.MapPut("/People/{id}", async (int id, People inputPeople, DbContextClass db) =>
{
    var people = await db.peoples.FindAsync(id);

    if (people is null) return Results.NotFound();

    people.Name = inputPeople.Name;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPut("/Meeting/{id}", async (int id, Meeting inputMeeting, DbContextClass db) =>
{
    var meeting = await db.meetings.FindAsync(id);

    if (meeting is null) return Results.NotFound();

    meeting.Subject = meeting.Subject;

    await db.SaveChangesAsync();
    return Results.NoContent();
});


app.Run();

