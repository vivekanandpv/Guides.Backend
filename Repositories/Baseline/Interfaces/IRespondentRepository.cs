using System.Threading.Tasks;
using Guides.Backend.Domain;

namespace Guides.Backend.Repositories.Baseline.Interfaces
{
    public interface IRespondentRepository : ICrudRepository<Respondent>
    {
        Task<bool> IsDuplicate(string fullName, string husbandName, string addressLine1);
    }
}