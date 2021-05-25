using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.QuizTaker.ModelViews.QuizAnswering
{
    /// <summary>
    /// App user search view model
    /// </summary>
    public class QuizAnsweringViewModel
    {
        
        public Guid ParticipantId { get; set; } = default!;
        public Quiz Quiz { get; set; } = default!;
        public int QuestionIndex { get; set; } = default!;
        public SelectList Answers { get; set; } = default!;
        public string? ErrorMessage { get; set; }
        
        public Guid? PickedAnswerId { get; set; }

    }
}