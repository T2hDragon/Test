using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Home controller 
    /// </summary>
    ///
    public class HomeController : Controller
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Home controller constructor
        /// </summary>
        /// <param name="bll"></param>
        public HomeController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get data for Index page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Empty;
            try
            {
                userId = User.GetUserId()!.Value;
            } catch (Exception) { /* ignored*/ }

            var vmList = new List<WebApp.Areas.QuizTaker.ModelViews.QuizResultViewModel.QuizResultViewModel>();
            var participations = await _bll.Participants.GetUserParticipations(userId);
            foreach (var participant in participations)
            {

                vmList.Add(new WebApp.Areas.QuizTaker.ModelViews.QuizResultViewModel.QuizResultViewModel
                {
                    CorrectAnswerPercentage = await _bll.Participants.GetCorrectAnswerPercentage(participant.Id),
                    ParticipantId = participant.Id,
                    PickedAnswers = participant.PickedAnswers!.Select(pickedAnswer => pickedAnswer.Answer).ToList(),
                    Quiz = await _bll.Quizzes.GetQuizWithAuthorInfo(participant.QuizId)
                });
            }
            return View(vmList);
        }
        

        /// <summary>
        /// Get errors
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        

    }
}