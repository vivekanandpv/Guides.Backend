using Guides.Backend.Services.Baseline.Interfaces.General;
using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.Uganda
{
    public interface IUgandaSocioDemographicService: IFormService<SocioDemographicUgandaListViewModel,
        SocioDemographicUgandaRegisterViewModel, SocioDemographicUgandaUpdateViewModel>
    {
    }
}