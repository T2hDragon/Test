using System;
using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.QuizTaker.ModelViews.QuizResultViewModel
{
    /// <summary>
    /// App user search view model
    /// </summary>
    public class QuizResultViewModel
    {

    public List<Answer> PickedAnswers { get; set; } = default!;
    public Quiz Quiz { get; set; } = default!;
    public double CorrectAnswerPercentage { get; set; } = default!;
    public Guid ParticipantId { get; set; } = default!;
    }
}