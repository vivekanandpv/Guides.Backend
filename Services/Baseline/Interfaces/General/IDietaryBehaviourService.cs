using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.General
{
    public interface IDietaryBehaviourService : IFormService<DietaryBehaviourListViewModel,
        DietaryBehaviourRegisterViewModel, DietaryBehaviourUpdateViewModel>
    {
    }
}