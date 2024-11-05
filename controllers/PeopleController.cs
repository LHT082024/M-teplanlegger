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
    public class PeopleController : ControllerBase
    {
        private readonly DbContextClass _context;

        public PeopleController(DbContextClass context)
        {
            _context = context;
        }

        //this get method takes the people objects in the people model and converts it to a list and then
        //displays the list
        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            var people = await _context.peoples.ToListAsync();

            return Ok(people);
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetPeopleId([FromRoute] int id)
        {

            var people = await _context.peoples.FindAsync(id);
            return Ok(people);



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

    }
}