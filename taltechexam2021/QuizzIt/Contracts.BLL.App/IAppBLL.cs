using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;
using Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App
{
    public interface IAppBLL: IBaseBLL
    {
        
        IAnswerService Answers { get; }
        IParticipantService Participants { get; }
        IPickedAnswerService PickedAnswers { get; }
        IQuestionService Questions { get; }
        IQuizService Quizzes { get; }
        

    }
}