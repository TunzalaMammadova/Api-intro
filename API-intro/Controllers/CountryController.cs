using System;
using API_intro.Data;
using API_intro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_intro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountryController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok(await _context.Countries.Include(m=> m.Cities).ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Country countries)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _context.Countries.AddAsync(countries);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), countries);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existData = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);

            if (existData is null) return NotFound();

            return Ok(existData);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest();

            var existData = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);

            if (existData is null) return NotFound();

            _context.Countries.Remove(existData);

            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] Country country,[FromRoute] int? id)
        {
            if (id is null) return BadRequest();

            var existData = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);

            if (existData is null) return NotFound();

            existData.Name = country.Name;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

