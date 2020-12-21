using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guides.Backend.Services.Baseline.Interfaces.General
{
    public interface IFormService<TListViewModel, TRegisterViewModel, TUpdateViewModel>
    {
        Task<IEnumerable<TListViewModel>> Get();
        Task<TListViewModel> Get(int id);
        Task<TListViewModel> Register(TRegisterViewModel viewModel, string initiatedBy);
        Task<TListViewModel> Update(int id, TUpdateViewModel viewModel, string initiatedBy);
    }
}