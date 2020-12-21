using Guides.Backend.Services.Baseline.Interfaces.General;
using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.India
{
    public interface IIndiaRespondentService : IFormService<RespondentIndiaListViewModel,
        RespondentIndiaRegisterViewModel, RespondentIndiaUpdateViewModel>
    {
    }
}
