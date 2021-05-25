using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Domain.App.Constants;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.QuizTaker.ModelViews.QuizSearch;

namespace WebApp.Areas.QuizTaker.Controllers
{
    [Area(nameof(WebApp.Areas.QuizTaker))]
    public class QuizSearchController : Controller
    {
        private readonly IAppBLL _bll;

        public QuizSearchController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index(int? page, string? search)
        {
            var userId = Guid.Empty;
            try
            {
                userId = User.GetUserId()!.Value;
            } catch (Exception) { /* ignored*/ }

            var quizzes = await _bll.Quizzes.GetQuizzesWithSearchData(userId, page??0, search??"");
            return View(new QuizSearchViewModel
            {
                Search = search??"",
                Quizzes = quizzes,
                Page = page??0
            });
        }
        
        [HttpPost]
        public IActionResult FilterQuizSearch(string search, int? page)
        {
            return RedirectToAction(nameof(Index), new {page= page??0, search= search??""});
        }
        
        [HttpGet]
        public async Task<IActionResult> AnswerQuiz(Guid quizId)
        {
            var userId = Guid.Empty;
            try
            {
                userId = User.GetUserId()!.Value;
            } catch (Exception) { /* ignored*/ }
            var participant = new Participant
            {
                AppUserId = userId == Guid.Empty? null : userId,
                QuizId = quizId,
                PickedAnswers = new List<PickedAnswer>()
            };
            participant = _bll.Participants.Add(participant);
            await _bll.SaveChangesAsync();

            return RedirectToAction("Index", "QuizAnswering", new {quizId= quizId, participantId = participant.Id, questionIndex=0});
        }


    }
}
