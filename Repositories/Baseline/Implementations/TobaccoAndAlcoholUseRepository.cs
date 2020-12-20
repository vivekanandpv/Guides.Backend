using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Data;
using Guides.Backend.Domain;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Repositories.Baseline.Implementations
{
    public class TobaccoAndAlcoholUseRepository : ITobaccoAndAlcoholUseRepository
    {
        private readonly GuidesContext _context;

        public TobaccoAndAlcoholUseRepository(GuidesContext context)
        {
            _context = context;
        }
        public IQueryable<TobaccoAndAlcoholUse> Get()
        {
            return this._context.TobaccoAndAlcoholUseCollection;
        }

        public async Task<TobaccoAndAlcoholUse> Get(int id)
        {
            return await this._context.TobaccoAndAlcoholUseCollection.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<TobaccoAndAlcoholUse> Add(TobaccoAndAlcoholUse entity)
        {
            await this._context.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task Save(TobaccoAndAlcoholUse entity)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}