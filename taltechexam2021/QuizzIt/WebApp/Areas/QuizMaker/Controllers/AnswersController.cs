using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using BLL.App.DTO;
using Domain.App.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.QuizMaker.Controllers
{
    [Area(nameof(WebApp.Areas.QuizMaker))]
    [Authorize(Roles = AppRoles.QuizCreator)]
    public class AnswersController : Controller
    {
        private readonly IAppBLL _bll;

        public AnswersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // Post: Quizzes/Name/Change
        [HttpPost]
        public async Task<IActionResult> UpdateName(Guid? answerId, Guid? questionId, string name, string value)
        {
            if (questionId != null)
            {
                var question = await _bll.Questions.FirstOrDefaultAsync(questionId!.Value);

                if (question == null)
                {
                    return NotFound();
                }
                question.Value = value;
                _bll.Questions.Update(question);
                await _bll.SaveChangesAsync();
            
                return RedirectToAction(nameof(Edit), new{questionId = question.Id});
            }
            else
            {
                var answer = await _bll.Answers.FirstOrDefaultAsync(answerId!.Value);

                if (answer == null)
                {
                    return NotFound();
                }
                answer.Value = value;
                _bll.Answers.Update(answer);
                await _bll.SaveChangesAsync();
            
                return RedirectToAction(nameof(Edit), new{questionId = answer.QuestionId});
            }
        }
        
        // Post: Quizzes/Order/Change
        [HttpGet]
        public async Task<IActionResult> MoveOrder(Guid? answerId, string move)
        {
            var answer = await _bll.Answers.MoveAnswer(answerId!.Value, Int32.Parse(move));
            return RedirectToAction(nameof(Edit), new{questionId = answer.QuestionId});
        }
        
        // Post: Quizzes/Answers/Create
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(Guid questionId)
        {
            var answers = await _bll.Answers.GetQuestionAnswers(questionId);
            var newAnswer = new Answer()
            {
                QuestionId = questionId,
                Value = Request.Form["title"],
                Order = answers.Count(),
                IsCorrect = false
            };
            
            _bll.Answers.Add(newAnswer);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new{questionId = questionId});
        }
        
        
        // POST: Quizzes/Question/Delete/5
        [HttpPost, ActionName("QuestionDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuestionDeleteConfirmed(Guid questionId)
        {
            var question = await _bll.Questions.RemoveAsync(questionId);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Edit", "Questions", new{id = question.QuizId});
        }
        
        // POST: Quizzes/Answer/Delete/5
        [HttpPost, ActionName("AnswerDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnswerDeleteConfirmed(Guid answerId)
        {
            var answer = await _bll.Answers.FirstOrDefaultAsync(answerId);
            var answers = await _bll.Answers.GetQuestionAnswers(answer!.QuestionId!.Value);
            if (answers.Count() <= 2)
            {
                ModelState.AddModelError(string.Empty, "There can not be less that two options!");
                return RedirectToAction(nameof(Edit), new{questionId = answer.QuestionId});
            }
            answer = await _bll.Answers.RemoveAsync(answerId);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new{questionId = answer.QuestionId});
        }
        
        // Get: Quizzes/Question/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid questionId)
        {
            var question = await _bll.Questions.FirstOrDefaultAsync(questionId);
            if (question == null) { return NotFound(); }
            question.Answers = (await _bll.Answers.GetQuestionAnswers(questionId)).ToList();
            return View(question);
        }
        
        // POST: Quizzes/Question/5/Answer/5/SetCorrect
        [HttpPost, ActionName("SetCorrectAnswer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetCorrectAnswer(Guid answerId)
        {
            var answer = await _bll.Answers.FirstOrDefaultAsync(answerId);
            await _bll.Answers.SetCorrectAnswer(answerId);
            return RedirectToAction(nameof(Edit), new{questionId = answer!.QuestionId});
        }
        
        // POST: Quizzes/Question/5/Answer/5/RemoveCorrectAnswer
        [HttpPost, ActionName("RemoveCorrectAnswer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCorrectAnswer(Guid answerId)
        {
            var answer = await _bll.Answers.FirstOrDefaultAsync(answerId);
            await _bll.Answers.SetAnswerCorrect(answerId, false);
            return RedirectToAction(nameof(Edit), new{questionId = answer!.QuestionId});
        }
    }
}
