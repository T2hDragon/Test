using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
namespace Contracts.BLL.App.Services
{
    public interface IContractService: IBaseEntityService<BLLAppDTO.Contract, DALAppDTO.Contract>, IContractRepositoryCustom<BLLAppDTO.Contract>
    {
        public Task<IEnumerable<BLLAppDTO.Teacher>> GetSchoolTeachers(Guid schoolId, bool noTracing = true);

        public Task<BLLAppDTO.Teacher> GetTeacher(Guid contractId, bool noTracing = true);
        public Task<BLLAppDTO.PeriodReport> GetContractPeriodReport(Guid contractId, DateTime searchStart, DateTime searchEnd, bool noTracing = true);

    }
}