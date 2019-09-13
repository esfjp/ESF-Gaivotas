using ESFGaivotas.Model.Repository.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESFGaivotas.Model.Core
{
    public class UnitOfWork : IDisposable
    {
        private readonly ESFContext _context;

        public UnitOfWork(ESFContext context)
        {
            this._context = context;

#if Debug_Mock
            User = new MockUserRepository(this._context);
            Reports = new MockReportRepository(this._context);
            Debris = new MockDebrisRepository(this._context);
#else
            User = new UserRepository(this._context);
            Reports = new ReportRepository(this._context);
            Debris = new DebrisRepository(this._context);
#endif
        }

        public IUserRepository User { get; private set; }

        public IReportRepository Reports { get; private set; }

        public IDebrisRepository Debris { get; private set; }

        public async Task<int> Complete()
        {
            return await this._context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
