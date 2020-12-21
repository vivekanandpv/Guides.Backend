using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.General
{
    public interface IPregnancyAndGdmRiskFactorsService : IFormService<PregnancyAndGdmRiskFactorsListViewModel, 
        PregnancyAndGdmRiskFactorsRegisterViewModel, PregnancyAndGdmRiskFactorsUpdateViewModel>
    {
    }
}