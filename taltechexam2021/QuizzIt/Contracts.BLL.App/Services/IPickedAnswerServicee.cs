using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IPickedAnswerService: IBaseEntityService<BLLAppDTO.PickedAnswer, DALAppDTO.PickedAnswer>, IPickedAnswerRepositoryCustom<BLLAppDTO.PickedAnswer>
    {
        public Task Submit(Guid userId, Guid answerId, bool noTracing = true);

    }
}