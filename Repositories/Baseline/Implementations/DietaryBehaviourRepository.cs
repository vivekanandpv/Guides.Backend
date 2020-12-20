using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Data;
using Guides.Backend.Domain;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Repositories.Baseline.Implementations
{
    public class DietaryBehaviourRepository : IDietaryBehaviourRepository
    {
        private readonly GuidesContext _context;

        public DietaryBehaviourRepository(GuidesContext context)
        {
            _context = context;
        }
        public IQueryable<DietaryBehaviour> Get()
        {
            return this._context.DietaryBehaviourCollection;
        }

        public async Task<DietaryBehaviour> Get(int id)
        {
            return await this._context.DietaryBehaviourCollection.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<DietaryBehaviour> Add(DietaryBehaviour entity)
        {
            await this._context.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task Save(DietaryBehaviour entity)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}