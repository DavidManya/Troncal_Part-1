using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project1.Lib.Context.Interfaces;
using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Project1.Lib.Models;

namespace DemoSuperGuay.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public StudentsController()
        {
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await Task.Run(() =>
            {
                var repo = Entity.DepCon.Resolve<IStudentRepository>();
                return repo.QueryAll();
            });
        }

        //// GET: api/Books/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Book>> GetBook(Guid id)
        //{
        //    var book = await _context.Books.FindAsync(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        //// PUT: api/Books/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(Guid id, Book book)
        //{
        //    if (id != book.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Books
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<SaveResult<Student>> PostStudent(Student student)
        {
            return await Task.Run(() =>
            {
                return student.Save();
            });

        }

        //// DELETE: api/Books/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Book>> DeleteBook(Guid id)
        //{
        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();

        //    return book;
        //}

        //private bool BookExists(Guid id)
        //{
        //    return _context.Books.Any(e => e.Id == id);
        //}
    }
}
