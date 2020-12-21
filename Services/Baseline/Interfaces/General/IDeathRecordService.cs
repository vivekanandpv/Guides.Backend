using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces.General
{
    public interface IDeathRecordService : IFormService<DeathRecordListViewModel,
        DeathRecordRegisterViewModel, DeathRecordUpdateViewModel>
    {
    }
}