using System.Collections.Generic;
using System.Threading.Tasks;
using Guides.Backend.Services.Baseline.Interfaces.General;
using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.Uganda
{
    public interface IUgandaRespondentService: IFormService<RespondentUgandaListViewModel,
        RespondentUgandaRegisterViewModel, RespondentUgandaUpdateViewModel>
    {
        Task<FormStatusNavigatorViewModel> GetFormStatusNavigator(int id);
        Task<IEnumerable<RespondentWithFormStatusViewModel>> GetRespondentList();
        Task<RespondentWithFormStatusViewModel> GetRespondentWithFormStatus(int id);
        Task<IEnumerable<RespondentSearchViewModel>> GetRespondentByPattern(string pattern);
    }
}