using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Data;
using Guides.Backend.Domain;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Repositories.Baseline.Implementations
{
    public class DeathRecordRepository : IDeathRecordRepository
    {
        private readonly GuidesContext _context;

        public DeathRecordRepository(GuidesContext context)
        {
            _context = context;
        }
        public IQueryable<DeathRecord> Get()
        {
            return this._context.DeathRecords;
        }

        public async Task<DeathRecord> Get(int id)
        {
            return await this._context.DeathRecords.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<DeathRecord> Add(DeathRecord entity)
        {
            await this._context.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task Save(DeathRecord entity)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}