using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Data;
using Guides.Backend.Domain;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Repositories.Baseline.Implementations
{
    public class PhysicalActivityRepository : IPhysicalActivityRepository
    {
        private readonly GuidesContext _context;

        public PhysicalActivityRepository(GuidesContext context)
        {
            _context = context;
        }
        public IQueryable<PhysicalActivity> Get()
        {
            return this._context.PhysicalActivityCollection;
        }

        public async Task<PhysicalActivity> Get(int id)
        {
            return await this._context.PhysicalActivityCollection.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<PhysicalActivity> Add(PhysicalActivity entity)
        {
            await this._context.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task Save(PhysicalActivity entity)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}