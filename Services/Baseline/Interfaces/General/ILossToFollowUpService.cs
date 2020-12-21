using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.General
{
    public interface ILossToFollowUpService : IFormService<LossToFollowUpListViewModel,
        LossToFollowUpRegisterViewModel, LossToFollowUpUpdateViewModel>
    {
    }
}