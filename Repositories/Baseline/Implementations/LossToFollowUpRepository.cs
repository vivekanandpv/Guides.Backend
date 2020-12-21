using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Data;
using Guides.Backend.Domain;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Repositories.Baseline.Implementations
{
    public class LossToFollowUpRepository : ILossToFollowUpRepository
    {
        private readonly GuidesContext _context;

        public LossToFollowUpRepository(GuidesContext context)
        {
            _context = context;
        }
        public IQueryable<LossToFollowUp> Get()
        {
            return this._context.LossToFollowUpCollection;
        }

        public async Task<LossToFollowUp> Get(int id)
        {
            return await this._context.LossToFollowUpCollection.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<LossToFollowUp> Add(LossToFollowUp entity)
        {
            await this._context.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task Save(LossToFollowUp entity)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}