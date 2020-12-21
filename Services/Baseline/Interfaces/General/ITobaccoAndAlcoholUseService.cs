using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.General
{
    public interface ITobaccoAndAlcoholUseService : IFormService<TobaccoAndAlcoholUseListViewModel,
        TobaccoAndAlcoholUseRegisterViewModel, TobaccoAndAlcoholUseUpdateViewModel>
    {
    }
}