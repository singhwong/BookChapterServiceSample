using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksServiceSampleHost.Models;
using BooksServiceSampleHost.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksServiceSampleHost.Controllers
{
    [Produces("application/json","application/xml")]
    [Route("api/[controller]")]
    public class BookChaptersController : Controller
    {
        private readonly IBookChaptersService _bookChaptersService;
        public BookChaptersController(IBookChaptersService bookChaptersService)
        {
            _bookChaptersService = bookChaptersService;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetBookChaptersAsync()
        {
           var data =  await _bookChaptersService.GetAllAsync();
            return new ObjectResult(data);
        }

        // GET api/values/5
        [HttpGet("{id}",Name =nameof(GetBookChapterByIdAsync))]
        public async Task<IActionResult> GetBookChapterByIdAsync(Guid id)
        {
            var chapter = await _bookChaptersService.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(chapter);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostBookChapterAsync([FromBody] BookChapter chapter)
        {
            if (chapter == null)
            {
                return BadRequest();
            }
            await _bookChaptersService.AddAsync(chapter);
            return CreatedAtRoute(nameof(GetBookChapterByIdAsync),new { chapter.Id},chapter);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookChapterAsync(Guid id, [FromBody] BookChapter chapter)
        {
            if (chapter == null || id != chapter.Id)
            {
                return BadRequest();
            }
            if (await _bookChaptersService.FindAsync(chapter.Id) == null)
            {
                return NotFound();
            }
            await _bookChaptersService.UpdateAsync(chapter);
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookChapterAsync(Guid id)
        {
            var chapter = await _bookChaptersService.RemoveAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            //return CreatedAtAction("removed!",new { id, },chapter);
            return Ok();
        }
    }
}
