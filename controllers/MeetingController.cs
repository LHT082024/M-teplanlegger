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
        public async Task<IActionResult> CreateMeeting([FromBody] Meeting meeting)
        {
            meeting.Id = 0;
            _context.meetings.Add(meeting);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllMeetings), new { id = meeting.Id }, meeting);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateMeeting(int id, [FromBody] Meeting updateMeeting)
        {
            var meeting = await _context.meetings.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }

            meeting.Id = updateMeeting.Id;
            meeting.Place = updateMeeting.Place;
            meeting.Attendees = updateMeeting.Attendees;
            meeting.Subject = updateMeeting.Subject;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var meeting = await _context.meetings.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }

            _context.meetings.Remove(meeting);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
