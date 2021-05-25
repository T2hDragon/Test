using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO;

namespace WebApp.Areas.QuizTaker.ModelViews.QuizSearch
{
    /// <summary>
    /// App user search view model
    /// </summary>
    public class QuizSearchViewModel
    {

        /// <summary>
        /// Search value
        /// </summary>
        [MaxLength(1024)]
        public string Search { get; set; } = default!;
        public int Page { get; set; } = default!;

        public IEnumerable<BLL.App.DTO.Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}