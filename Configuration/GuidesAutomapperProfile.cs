using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Guides.Backend.Domain;
using Guides.Backend.ViewModels.Auth;
using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.Configuration
{
    public class GuidesAutoMapperProfile:Profile
    {
        public GuidesAutoMapperProfile()
        {
            CreateMap<AuthRegisterViewModel, User>()
                .ForMember(u => u.Country,
                    opt => opt.MapFrom(
                        vm => 
                            (Country) Enum.Parse(typeof(Country), vm.Country, true)
                            )
                    );

            CreateMap<Respondent, RespondentIndiaListViewModel>()
                .ForMember(
                    vm => vm.RegisteredBy, 
                    options => options.MapFrom(r => r.User.Email));
            
            CreateMap<Respondent, RespondentUgandaListViewModel>()
                .ForMember(
                    vm => vm.RegisteredBy, 
                    options => options.MapFrom(r => r.User.Email));
            
            CreateMap<RespondentIndiaRegisterViewModel, Respondent>();
            CreateMap<RespondentUgandaRegisterViewModel, Respondent>();

            CreateMap<RespondentIndiaUpdateViewModel, Respondent>();
            CreateMap<RespondentUgandaUpdateViewModel, Respondent>();

            CreateMap<SocioDemographic, SocioDemographicIndiaListViewModel>();
            CreateMap<SocioDemographic, SocioDemographicUgandaListViewModel>();
            
            CreateMap<SocioDemographicIndiaRegisterViewModel, SocioDemographic>();
            CreateMap<SocioDemographicUgandaRegisterViewModel, SocioDemographic>();
            
            CreateMap<SocioDemographicIndiaUpdateViewModel, SocioDemographic>();
            CreateMap<SocioDemographicUgandaUpdateViewModel, SocioDemographic>();
            
            
            CreateMap<PregnancyAndGdmRiskFactors, PregnancyAndGdmRiskFactorsListViewModel>();
            CreateMap<PregnancyAndGdmRiskFactorsRegisterViewModel, PregnancyAndGdmRiskFactors>();
            CreateMap<PregnancyAndGdmRiskFactorsUpdateViewModel, PregnancyAndGdmRiskFactors>();
            
            CreateMap<TobaccoAndAlcoholUse, ToboccoAndAlcoholUseListViewModel>();
            CreateMap<ToboccoAndAlcoholUseRegisterViewModel, TobaccoAndAlcoholUse>();
            CreateMap<ToboccoAndAlcoholUseUpdateViewModel, TobaccoAndAlcoholUse>();
            
            CreateMap<PhysicalActivity, PhysicalActivityListViewModel>();
            CreateMap<PhysicalActivityRegisterViewModel, PhysicalActivity>();
            CreateMap<PhysicalActivityUpdateViewModel, PhysicalActivity>();
            
            CreateMap<DietaryBehaviour, DietaryBehaviourListViewModel>();
            CreateMap<DietaryBehaviourRegisterViewModel, DietaryBehaviour>();
            CreateMap<DietaryBehaviourUpdateViewModel, DietaryBehaviour>();
        }
    }
}
