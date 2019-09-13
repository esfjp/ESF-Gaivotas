using ESFGaivotas.Model.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESFGaivotas.Model.Repository.Database
{
    public interface IReportRepository : IBaseRepository<ReportModel>
    {
        Task<List<ReportModel>> GetReportsByUser(UserModel user);
    }

    public class MockReportRepository : MockBaseRepository<ReportModel>, IReportRepository
    {
        public MockReportRepository(ESFContext context)
            : base(context) { }

        public ESFContext ESFContext
        {
            get { return Context as ESFContext; }
        }

        #region Implementation

        public async Task<List<ReportModel>> GetReportsByUser(UserModel user)
        {
            return await ESFContext.Reports
                .Where(l => l.User == user)
                .Include(l => l.Debris)
                .ToListAsync();
        }

        #endregion
    }
}
