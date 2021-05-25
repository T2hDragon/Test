using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Domain.App.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.QuizTaker.ModelViews.QuizAnswering;
using WebApp.Areas.QuizTaker.ModelViews.QuizResultViewModel;
using WebApp.Debugging;

namespace WebApp.Areas.QuizTaker.Controllers
{
    [Area(nameof(WebApp.Areas.QuizTaker))]
    public class QuizAnsweringController : Controller
    {
        private readonly IAppBLL _bll;

        public QuizAnsweringController(IAppBLL bll)
        {
            _bll = bll;
        }
        // GET: Quizzes
        [HttpGet]
        public async Task<IActionResult> Index(Guid quizId, Guid participantId, string? errorMessage, int? questionIndex = 0)
        {
            var qIndex = questionIndex ?? 0;
            var quiz = await _bll.Quizzes.GetQuizWithStats(quizId);
            if (quiz.Questions!.Count() <= qIndex)
            {
                return RedirectToAction("Result", new {participantId= participantId});
            }
            var answers = quiz.Questions!.ToList()[qIndex!].Answers;
            var possibleAnswer = Guid.Empty;
            var counter = 0;
            foreach (Answer answer in answers!)
            {
                if (await _bll.Participants.HasPickedAnswer(participantId, answer.Id))
                {
                    possibleAnswer = answer.Id;
                    break;
                }
                counter += 1;
            }
            var answersSelectList = new SelectList(answers,
                nameof(Answer.Id),
                nameof(Answer.Value));
            return View(new QuizAnsweringViewModel
            {
                ErrorMessage = errorMessage,
                ParticipantId = participantId,
                Answers = answersSelectList,
                QuestionIndex = qIndex,
                Quiz = quiz,
                PickedAnswerId = possibleAnswer == Guid.Empty ? null : possibleAnswer
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> QuestionSubmit(Guid questionId, Guid participantId, int questionIndex)
        {
            var question = await _bll.Questions.FirstOrDefaultAsync(questionId);
            var answer = Request.Form["answer"][0];
            if (answer == null)
            {
                return RedirectToAction(nameof(Index), new {quizId= question!.QuizId, participantId= participantId, questionIndex= questionIndex, errorMessage= "Need to pick an option!"});
            }
            await _bll.PickedAnswers.Submit(participantId, Guid.Parse(answer));
            return RedirectToAction(nameof(Index), new {quizId= question!.QuizId, participantId= participantId, questionIndex= questionIndex + 1});
        }
        
        [HttpGet]
        public async Task<IActionResult> Result(Guid participantId)
        {
            var participant = await _bll.Participants.FirstOrDefaultAsync(participantId);
            var answers = await _bll.Answers.GetParticipantAnswers(participantId);
            var quiz = await _bll.Quizzes.GetQuizWithStats(participant!.QuizId);
            return View(new QuizResultViewModel()
            {
                PickedAnswers = answers.ToList(),
                Quiz = quiz,
                ParticipantId = participantId,
                CorrectAnswerPercentage = await _bll.Participants.GetCorrectAnswerPercentage(participantId)
            });
        }
        [HttpGet]
        public async Task<IActionResult> QuestionBack(Guid questionId, Guid participantId, int questionIndex)
        {
            var question = await _bll.Questions.FirstOrDefaultAsync(questionId);
            return RedirectToAction(nameof(Index), new {quizId= question!.QuizId, participantId= participantId, questionIndex= questionIndex - 1});
        }
    }
}