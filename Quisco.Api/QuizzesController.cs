﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quisco.DataAccess;
using Quisco.Model;

namespace Quisco.Api
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
	    private string uselesssss = "";
        private readonly QuiscoContext _context;

        public QuizzesController(QuiscoContext context)
        {
            _context = context;
        }

        // GET: api/Quizzes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizes()
        {
            return await _context.Quizes.ToListAsync();
        }

        // GET: api/Quizzes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizes.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }

        // GET: api/Quizzes/userIdHash/abc
        [HttpGet("userIdHash/{userIdHash}")]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuestionsFromQuizId(string userIdHash)
        {
	        var quiz = await _context.Quizes.Where(x => x.UserIdHash == userIdHash).ToListAsync();

	        if (quiz == null)
	        {
		        return NotFound();
	        }
	        return quiz;
        }

		// PUT: api/Quizzes/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.QuizId)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quizzes
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            _context.Quizes.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.QuizId }, quiz);
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Quiz>> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizes.Remove(quiz);
            await _context.SaveChangesAsync();

            return quiz;
        }

        private bool QuizExists(int id)
        {
            return _context.Quizes.Any(e => e.QuizId == id);
        }
    }
}
