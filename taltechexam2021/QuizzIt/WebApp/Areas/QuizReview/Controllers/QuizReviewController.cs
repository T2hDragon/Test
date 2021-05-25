using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.QuizReview.Controllers
{
    [Area(nameof(WebApp.Areas.QuizReview))]
    public class QuizReviewController : Controller
    {
        private readonly IAppBLL _bll;

        public QuizReviewController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index(Guid quizId)
        {
            var quiz = await _bll.Quizzes.GetQuizWithStats(quizId);
            return View(quiz);
        }

    }
}
