using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IAnswerService: IBaseEntityService<BLLAppDTO.Answer, DALAppDTO.Answer>, IAnswerRepositoryCustom<BLLAppDTO.Answer>
    {
        public Task<BLLAppDTO.Answer> MoveAnswer(Guid answerId, int move, bool noTracing = true);
        public Task SetCorrectAnswer(Guid answerId, bool noTracing = true);
        public Task SetAnswerCorrect(Guid answerId, bool correct, bool noTracing = true);

    }
}