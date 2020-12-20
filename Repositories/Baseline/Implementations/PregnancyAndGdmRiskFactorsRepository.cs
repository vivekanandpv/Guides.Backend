using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Data;
using Guides.Backend.Domain;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Repositories.Baseline.Implementations
{
    public class PregnancyAndGdmRiskFactorsRepository : IPregnancyAndGdmRiskFactorsRepository
    {
        private readonly GuidesContext _context;

        public PregnancyAndGdmRiskFactorsRepository(GuidesContext context)
        {
            _context = context;
        }
        public IQueryable<PregnancyAndGdmRiskFactors> Get()
        {
            return this._context.PregnancyAndGdmRiskFactorsCollection;
        }

        public async Task<PregnancyAndGdmRiskFactors> Get(int id)
        {
            return await this._context.PregnancyAndGdmRiskFactorsCollection.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<PregnancyAndGdmRiskFactors> Add(PregnancyAndGdmRiskFactors entity)
        {
            await this._context.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task Save(PregnancyAndGdmRiskFactors entity)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}