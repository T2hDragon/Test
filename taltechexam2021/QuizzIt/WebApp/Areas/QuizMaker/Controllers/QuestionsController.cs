using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using BLL.App.DTO;
using Domain.App.Constants;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.QuizMaker.Controllers
{
    [Area(nameof(WebApp.Areas.QuizMaker))]
    [Authorize(Roles = AppRoles.QuizCreator)]
    public class QuestionsController : Controller
    {
        private readonly IAppBLL _bll;

        public QuestionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _bll.Quizzes.GetQuizWithStats(id!.Value);
            quiz.Questions = (await _bll.Questions.GetQuizQuestions(quiz.Id)).ToList();

            return View(quiz);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Quizzes.Update(quiz);
                await _bll.SaveChangesAsync();
            }

            return View(quiz);
        }

        // Post: Quizzes/Question/Create
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(Guid quizId)
        {
            var questions = await _bll.Questions.GetQuizQuestions(quizId);
            var initialQuestion = new Question
            {
                QuizId = quizId,
                Value = Request.Form["title"],
                Order = questions.Count()
            };
            initialQuestion = _bll.Questions.Add(initialQuestion);
            await _bll.SaveChangesAsync();
            var correctAnswer = new Answer
            {
                IsCorrect = true,
                Value = "Correct Answer",
                QuestionId = initialQuestion.Id,
                Order = 0
            };
            var incorrectAnswer = new Answer
            {
                IsCorrect = false,
                Value = "Incorrect Answer",
                QuestionId = initialQuestion.Id,
                Order = 1
            };
            _bll.Answers.Add(correctAnswer);
            _bll.Answers.Add(incorrectAnswer);
            await _bll.SaveChangesAsync();
            initialQuestion.Answers = new List<Answer> {correctAnswer, incorrectAnswer};
            return RedirectToAction(nameof(Edit), new {id = quizId});
        }

        // Post: Quizzes/Name/Change
        [HttpPost]
        public async Task<IActionResult> UpdateName(Guid? quizId, string name, string value)
        {
            var quiz = await _bll.Quizzes.GetQuizWithStats(quizId!.Value);
            quiz.Name = name;
            _bll.Quizzes.Update(quiz);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new {id = quizId});
        }

        // Post: Quizzes/Questions/Order/Change
        [HttpGet]
        public async Task<IActionResult> MoveOrder(Guid? questionId, string move)
        {
            var question = await _bll.Questions.MoveQuestion(questionId!.Value, Int32.Parse(move));
            return RedirectToAction(nameof(Edit), new {id = question.QuizId});
        }
    }
}