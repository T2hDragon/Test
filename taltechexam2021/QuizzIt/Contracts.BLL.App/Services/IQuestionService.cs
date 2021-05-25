using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IQuestionService: IBaseEntityService<BLLAppDTO.Question, DALAppDTO.Question>, IQuestionRepositoryCustom<BLLAppDTO.Question>
    {
        public Task<BLLAppDTO.Question> MoveQuestion(Guid questionId, int move, bool noTracing = true);

    }
}