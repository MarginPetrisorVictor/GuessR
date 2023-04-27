using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuessR.Data;
using GuessR.Models;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Configuration;


namespace GuessR.Controllers
{
    public class GuessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuessController(ApplicationDbContext context)
        {
            _context = context;
        }
        private static List<int> _shownQuestionIds = new List<int>();
        private static List<int> _alreadyViewedQuestionsIds = new List<int>();

        // GET: Guess
        public async Task<IActionResult> Index()
        {
              return _context.GuessModel != null ? 
                          View(await _context.GuessModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GuessModel'  is null.");
        }
        

        

    // GET: Guess/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GuessModel == null)
            {
                return NotFound();
            }

            var guessModel = await _context.GuessModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guessModel == null)
            {
                return NotFound();
            }

            return View(guessModel);
        }

        // GET: Guess/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GuessRiddle,GuessAnswer")] GuessModel guessModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guessModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guessModel);
        }

        // GET: Guess/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GuessModel == null)
            {
                return NotFound();
            }

            var guessModel = await _context.GuessModel.FindAsync(id);
            if (guessModel == null)
            {
                return NotFound();
            }
            return View(guessModel);
        }

        // POST: Guess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GuessRiddle,GuessAnswer")] GuessModel guessModel)
        {
            if (id != guessModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guessModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuessModelExists(guessModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(guessModel);
        }

        // GET: Guess/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GuessModel == null)
            {
                return NotFound();
            }

            var guessModel = await _context.GuessModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guessModel == null)
            {
                return NotFound();
            }

            return View(guessModel);
        }

        // POST: Guess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GuessModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GuessModel'  is null.");
            }
            var guessModel = await _context.GuessModel.FindAsync(id);
            if (guessModel != null)
            {
                _context.GuessModel.Remove(guessModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuessModelExists(int id)
        {
          return (_context.GuessModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // NEW

        // save the current question properties to ViewBag
        public void SaveViewBag(GuessModel question)
        {
            ViewBag.QuestionType = question.QuestionType;
            ViewBag.Question = question.GuessRiddle;
            ViewBag.ContentType = question.ContentType;
            ViewBag.ContentUrl = question.ContentUrl;
            ViewBag.QuestionId = question.Id;
        }

        private GuessModel GenerateRandomQuestion()
        {
            var randomQuestion = _context.GuessModel
            .Where(q => !_shownQuestionIds.Contains(q.Id))
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefault();

            // Keep track of the ID of the shown question
            if (randomQuestion != null)
            {
                _shownQuestionIds.Add(randomQuestion.Id);

                // Store the question ID in the session for future requests
                HttpContext.Session.SetInt32("QuestionId", randomQuestion.Id);
            }
            return randomQuestion;
        }

        // GET: Guess/Game
        public IActionResult Game()
        {
            GuessModel randomQuestion;
            int? questionId = HttpContext.Session.GetInt32("QuestionId");

            if(questionId.HasValue)
            {
                if(questionId.Value!=-1)
                {
                    randomQuestion=_context.GuessModel.FirstOrDefault(q => q.Id == questionId.Value);
                    if (randomQuestion==null)
                    {
                        // If the question no longer exists in the database, generate a new question
                        randomQuestion = GenerateRandomQuestion();
                    }
                }
                else
                {
                    _shownQuestionIds.Clear();
                    HttpContext.Session.Clear();
                    ViewBag.Alert=true;
                    return RedirectToAction("Index", "Home");
                }
            }
            
            else
            {
                // If a question has not been generated for this session, generate a new question
                randomQuestion = GenerateRandomQuestion();
            }

            SaveViewBag(randomQuestion);
            HttpContext.Session.SetInt32("QuestionId", -1);
            return View(randomQuestion);
        }

        //answer Method
        [HttpPost]
        public IActionResult GuessAnswer(string answer, int questionId)
        {
            // Get the question from the database
            var question = _context.GuessModel.FirstOrDefault(x => x.Id == questionId);

            // Check if the answer is correct
            if (question != null && answer.ToLower() == question.GuessAnswer.ToLower())
            {
                // If the answer is correct, increment the score
                int score=HttpContext.Session.GetInt32("Score") ?? 0;
                score++;
                HttpContext.Session.SetInt32("Score", score);
            }

            // Get the next question from the database that hasn't been shown before
            var nextQuestion = GenerateRandomQuestion();
            if (nextQuestion != null)
            {
                // If there is a next question, show it
                SaveViewBag(nextQuestion);
                ViewBag.Score = HttpContext.Session.GetInt32("Score") ?? 0;
                

                // return View("Game", nextQuestion);
                return RedirectToAction("Game");
            }
            else
            {
                // If there are no more questions, show the GameOver view with the score
                ViewBag.Score = HttpContext.Session.GetInt32("Score") ?? 0;
                HttpContext.Session.Clear();
                _shownQuestionIds.Clear();
                return View("GameOver");
            }
        }


    }
}
