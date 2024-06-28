using System;
using API_intro.Data;
using API_intro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_intro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Cities.Include(m => m.Country).ThenInclude(m=>m.Cities).ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] City city)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _context.Cities.AddAsync(city);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), city);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existData = await _context.Cities.FirstOrDefaultAsync(m => m.Id == id);

            if (existData is null) return NotFound();

            return Ok(existData);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest();

            var existData = await _context.Cities.FindAsync(id);

            if (existData is null) return NotFound();

            _context.Cities.Remove(existData);

            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] City city, [FromRoute] int? id)
        {
            if (id is null) return BadRequest();

            var existData = await _context.Cities.FirstOrDefaultAsync(m => m.Id == id);

            if (existData is null) return NotFound();

            existData.Name = city.Name;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

