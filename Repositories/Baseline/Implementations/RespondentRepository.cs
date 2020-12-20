using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Data;
using Guides.Backend.Domain;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Repositories.Baseline.Implementations
{
    public class RespondentRepository : IRespondentRepository
    {
        private readonly GuidesContext _context;

        public RespondentRepository(GuidesContext context)
        {
            _context = context;
        }
        public IQueryable<Respondent> Get()
        {
            return this._context.Respondents;
        }

        public async Task<Respondent> Get(int id)
        {
            return await this._context.Respondents.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Respondent> Add(Respondent entity)
        {
            await this._context.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity;
        }

        public async Task Save(Respondent entity)
        {
            this._context.Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}