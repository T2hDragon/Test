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
    public class QuizzesController : Controller
    {
        private readonly IAppBLL _bll;

        public QuizzesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            var quiz = await _bll.Quizzes.GetUserCreatedQuizzes(User.GetUserId()!.Value);
            return View(quiz);
        }

        // Post: Quizzes/Create
        [HttpPost]
        public async Task<IActionResult> CreateQuiz()
        {
            await _bll.Quizzes.InitializeQuiz(Request.Form["title"], User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        
        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Quizzes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
