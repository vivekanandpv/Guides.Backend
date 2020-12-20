using System.Collections.Generic;
using System.Threading.Tasks;
using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Services.Baseline.Interfaces
{
    public interface IIndiaRespondentService
    {
        Task<IEnumerable<RespondentIndiaListViewModel>> Get();
        Task<RespondentIndiaListViewModel> Get(int id);
        Task<RespondentIndiaListViewModel> Register(RespondentIndiaRegisterViewModel viewModel, string initiatedBy);
        Task<RespondentIndiaListViewModel> Update(int id, RespondentIndiaUpdateViewModel viewModel, string initiatedBy);
    }
    
    public interface IIndiaSocioDemographicService
    {
        Task<IEnumerable<SocioDemographicIndiaListViewModel>> Get();
        Task<SocioDemographicIndiaListViewModel> Get(int id);
        Task<SocioDemographicIndiaListViewModel> Register(SocioDemographicIndiaRegisterViewModel viewModel, string initiatedBy);
        Task<SocioDemographicIndiaListViewModel> Update(int id, SocioDemographicIndiaUpdateViewModel viewModel, string initiatedBy);
    }


    public interface IUgandaRespondentService
    {
        Task<IEnumerable<RespondentUgandaListViewModel>> Get();
        Task<RespondentUgandaListViewModel> Get(int id);
        Task<RespondentUgandaListViewModel> Register(RespondentUgandaRegisterViewModel viewModel, string initiatedBy);
        Task<RespondentUgandaListViewModel> Update(int id, RespondentUgandaUpdateViewModel viewModel, string initiatedBy);
    }
    
    public interface IUgandaSocioDemographicService
    {
        Task<IEnumerable<SocioDemographicUgandaListViewModel>> Get();
        Task<SocioDemographicUgandaListViewModel> Get(int id);
        Task<SocioDemographicUgandaListViewModel> Register(SocioDemographicUgandaRegisterViewModel viewModel, string initiatedBy);
        Task<SocioDemographicUgandaListViewModel> Update(int id, SocioDemographicUgandaUpdateViewModel viewModel, string initiatedBy);
    }
}
