using System.Collections.Generic;
using System.Threading.Tasks;
using Guides.Backend.Services.Baseline.Interfaces.General;
using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.India
{
    public interface IIndiaRespondentService : IFormService<RespondentIndiaListViewModel,
        RespondentIndiaRegisterViewModel, RespondentIndiaUpdateViewModel>
    {
        Task<FormStatusNavigatorViewModel> GetFormStatusNavigator(int id);
        Task<IEnumerable<RespondentWithFormStatusViewModel>> GetRespondentList();
        Task<RespondentWithFormStatusViewModel> GetRespondentWithFormStatus(int id);
    }
}
