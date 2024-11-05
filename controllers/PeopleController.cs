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


        //this lets you search for a specific object inside the people table
        [HttpGet("id:int")]
        public async Task<IActionResult> GetPeopleId([FromRoute] int id)
        {

            var people = await _context.peoples.FindAsync(id);
            return Ok(people);



        }

        //this gives the ability to create a new people object from the people class 
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] People people)
        {
            people.Id = 0;
            _context.peoples.Add(people);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllPeople), new { id = people.Id }, people);
        }



        //Typing in an Id I this method gives you the option of searching for a specific object created
        //from the people class and modifying it
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


        //using Id you can search and delete a specific object created from the people class.
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