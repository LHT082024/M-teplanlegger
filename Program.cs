using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Møteplanlegger;
using Møteplanlegger.models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContextClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Arasaka-MeetingPlanner";
    config.Title = "MeetingPlanner v1";
    config.Version = "V1";
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "Arasaka-MeetingPlanner";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/Arasaka-MeetingPlanner/swagger.json";
        config.DocExpansion = "list";
    });
}


//these two get methods defines the route for the GET request It will take the People/Meeting object from the dbcontextclass
//Then convert the objects to a list of People/Meeting-objects and then return the list
app.MapGet("/people", async (DbContextClass db) =>
  await db.peoples.ToListAsync());

app.MapGet("/meeting", async (DbContextClass db) =>
await db.meetings.ToListAsync());

//These two get methods once again defines the route for the GET request
//but this time we can be more specific instead of getting a list of all the meetings/people
//we instead can get a specific meeting or person. 
app.MapGet("/people/{id}", async (int id, DbContextClass db) =>
await db.peoples.FindAsync(id)
is People people
? Results.Ok(people)
: Results.NotFound());

app.MapGet("/meeting/{id}", async (int id, DbContextClass db) =>
await db.meetings.FindAsync(id)
is Meeting meeting
? Results.Ok(meeting)
: Results.NotFound());

//this two methods creates a new people/meething object. First it identfies the path in the database
//then it creates the new people/meeting object and places it into it's assigned space
//in the database
app.MapPost("/people", async (People people, DbContextClass db) =>
{
    db.peoples.Add(people);
    await db.SaveChangesAsync();

    return Results.Created($"/people/{people.Id}", people);
});

app.MapPost("/meeting", async (Meeting meeting, DbContextClass db) =>
{
    db.meetings.Add(meeting);
    await db.SaveChangesAsync();

    return Results.Created($"/people/{meeting.Id}", meeting);
});


//these methods finds a meeting/person by id and then gives you the abilty to update that meeting/person
app.MapPut("/people/{id}", async (int id, People inputPeople, DbContextClass db) =>
{
    var people = await db.peoples.FindAsync(id);

    if (people is null) return Results.NotFound();

    people.Name = inputPeople.Name;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPut("/meeting/{id}", async (int id, Meeting inputMeeting, DbContextClass db) =>
{
    var meeting = await db.meetings.FindAsync(id);

    if (meeting is null) return Results.NotFound();

    meeting.Subject = meeting.Subject;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

//this is the final method for people and meeting where you delete them.
//you first find the path of the item via id, then checks if the item exists and if it does
//you then have the ability to delete the item.
app.MapDelete("/people/{id}", async (int id, DbContextClass db) =>
{
    if (await db.peoples.FindAsync(id) is People people)
    {
        db.peoples.Remove(people);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.MapDelete("/meeting/{id}", async (int id, DbContextClass db) =>
{
    if (await db.meetings.FindAsync(id) is Meeting meeting)
    {
        db.meetings.Remove(meeting);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.Run();

