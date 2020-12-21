using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.General
{
    public interface IPhysicalActivityService : IFormService<PhysicalActivityListViewModel,
        PhysicalActivityRegisterViewModel, PhysicalActivityUpdateViewModel>
    {
    }
}