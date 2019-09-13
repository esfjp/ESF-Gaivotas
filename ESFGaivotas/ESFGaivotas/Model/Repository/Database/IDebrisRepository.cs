using ESFGaivotas.Enumerations;
using ESFGaivotas.Model.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESFGaivotas.Model.Repository.Database
{
    public interface IDebrisRepository : IBaseRepository<DebrisModel>
    {
        Task<IEnumerable<DebrisModel>> GetDebrisByType(EDebrisType debrisType);
    }

    public class MockDebrisRepository : MockBaseRepository<DebrisModel>, IDebrisRepository
    {
        public MockDebrisRepository(ESFContext context)
            : base(context) { }

        public ESFContext ESFContext
        {
            get { return Context as ESFContext; }
        }

        #region Implementation
        public async Task<IEnumerable<DebrisModel>> GetDebrisByType(EDebrisType debrisType)
        {
            return await ESFContext.Debris
                .Where(l => l.DebrisType == debrisType)
                .Include(l => l.Report)
                .ToListAsync();
        }
        #endregion
    }
}
