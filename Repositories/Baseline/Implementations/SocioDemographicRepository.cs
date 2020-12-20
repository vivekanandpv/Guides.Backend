using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Data;
using Guides.Backend.Domain;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Repositories.Baseline.Implementations
{
    public class SocioDemographicRepository : ISocioDemographicRepository
    {
        private readonly GuidesContext _context;

        public SocioDemographicRepository(GuidesContext context)
        {
            _context = context;
        }
        public IQueryable<SocioDemographic> Get()
        {
            return this._context.SocioDemographics;
        }

        public async Task<SocioDemographic> Get(int id)
        {
            return await this._context.SocioDemographics.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<SocioDemographic> Add(SocioDemographic entity)
        {
            await this._context.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task Save(SocioDemographic entity)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}