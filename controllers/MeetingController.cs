using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Møteplanlegger.models;
using Møteplanlegger.Migrations;

namespace Møteplanlegger.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly DbContextClass _context;

        public MeetingController(DbContextClass context)
        {
            _context = context;
        }

        //this get method takes the people objects in the people model and converts it to a list and then
        //displays the list
        [HttpGet]
        public async Task<IActionResult> GetAllMeetings()
        {
            var meeting = await _context.meetings.ToListAsync();

            return Ok(meeting);
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetMeetingId([FromRoute] int id)
        {

            var meeting = await _context.meetings.FindAsync(id);
            return Ok(meeting);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] People people)
        {
            people.Id = 0;
            _context.peoples.Add(people);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllPeople), new { id = people.Id }, people);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] People updatePerson)
        {
            var people = await _context.peoples.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }

            people.Id = updatePerson.Id;
            people.Name = updatePerson.Name;
            people.Age = updatePerson.Age;
            people.Title = updatePerson.Title;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var people = await _context.peoples.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }

            _context.peoples.Remove(people);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
}