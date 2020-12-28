using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Domain;
using Guides.Backend.Exceptions;
using Guides.Backend.Exceptions.Auth;
using Guides.Backend.Exceptions.Domain;
using Guides.Backend.Repositories.Auth;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Guides.Backend.Services.Baseline.Interfaces;
using Guides.Backend.Services.Baseline.Interfaces.Uganda;
using Guides.Backend.StaticProviders;
using Guides.Backend.ViewModels.Baseline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Guides.Backend.Services.Baseline.Implementations.Uganda
{
    public class UgandaTobaccoAndAlcoholUseService : IUgandaTobaccoAndAlcoholUseService
    {
        private readonly ITobaccoAndAlcoholUseRepository _repository;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IRespondentRepository _respondentRepository;
        private readonly ILogger _logger;

        public UgandaTobaccoAndAlcoholUseService(
            ITobaccoAndAlcoholUseRepository repository, 
            IAuthRepository authRepository, 
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IRespondentRepository respondentRepository)
        {
            _repository = repository;
            _authRepository = authRepository;
            _mapper = mapper;
            _respondentRepository = respondentRepository;
            _logger = loggerFactory.CreateLogger(GeneralStaticDataProvider.TobaccoAndAlcoholUseCategory);
        }
        public async Task<IEnumerable<TobaccoAndAlcoholUseListViewModel>> Get()
        {
            return await this._repository
                .Get()
                .Include(m => m.Respondent)
                .Where(m => m.Respondent.Country == Country.Uganda)
                .Select(
                    r => this._mapper.Map<TobaccoAndAlcoholUse, TobaccoAndAlcoholUseListViewModel>(r)
                ).ToListAsync();
        }

        public async Task<TobaccoAndAlcoholUseListViewModel> Get(int id)
        {
            var model = await this._repository
                .Get(id);

            if (model.Respondent.Country != Country.Uganda)
            {
                this._logger.LogInformation($"Tobacco and alcohol use (Uganda): Cross region access to respondent id: {id} is blocked");
                throw new UserActionNotSupportedException();
            }
                
            return this._mapper.Map<TobaccoAndAlcoholUse, TobaccoAndAlcoholUseListViewModel>(model);

        }

        public async Task<TobaccoAndAlcoholUseListViewModel> Register(TobaccoAndAlcoholUseRegisterViewModel viewModel, string initiatedBy)
        {
            if (viewModel.RegisteredBy != initiatedBy)
            {
                this._logger.LogInformation($"Prevented registration of tobacco and alcohol use (Uganda) as user discrepancy found registration data: {viewModel.RegisteredBy}; auth system: {initiatedBy}");
                throw new UserActionNotSupportedException();
            }
            
            this._logger.LogInformation($"tobacco and alcohol use (Uganda) registration for RID: {viewModel.RespondentId} is initiated");
            var model = this._mapper.Map<TobaccoAndAlcoholUseRegisterViewModel, TobaccoAndAlcoholUse>(viewModel);

            var respondent = await this._respondentRepository.Get(viewModel.RespondentId);

            if (respondent == null)
            {
                this._logger.LogInformation($"Prevented registration of tobacco and alcohol use (Uganda) for non existent RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            if (respondent.DeathRecord != null)
            {
                this._logger.LogInformation($"Prevented registration of tobacco and alcohol use (Uganda) for deceased respondent RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            if (respondent.LossToFollowUp != null)
            {
                this._logger.LogInformation($"Prevented registration of tobacco and alcohol use (Uganda) for blocked respondent RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }

            if (respondent.User.Country != Country.Uganda)
            {
                this._logger.LogInformation($"Prevented cross-region registration of tobacco and alcohol use (Uganda) for RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }

            if (respondent.TobaccoAndAlcoholUse != null)
            {
                this._logger.LogInformation($"Prevented duplicate registration of tobacco and alcohol use (Uganda) for RID: {viewModel.RespondentId}");
                throw new DuplicatePreventionException();
            }
            
            if (respondent.RegisteredOn.Date > model.RegisteredOn.Date)
            {
                this._logger.LogInformation($"Prevented registration of tobacco and alcohol use (Uganda) for RID: {viewModel.RespondentId} because of registration date discrepancy. Baseline: {respondent.RegisteredOn.Date:dd-MM-yyyy}; Form: {model.RegisteredOn.Date:dd-MM-yyyy}");
                throw new RegistrationDateDiscrepancyException();
            }
            
            model.Respondent = respondent;
            model.RegisteredBy = initiatedBy;
            model.DateOfActualEntry = DateTime.UtcNow;

            this._logger.LogInformation($"Persistence for tobacco and alcohol use (Uganda) registration for RID: {viewModel.RespondentId} is initiated");
            await this._repository.Add(model);
            this._logger.LogInformation($"Tobacco and alcohol use (Uganda) registration for RID: {viewModel.RespondentId} succeeded. Id: {respondent.Id}");

            return this._mapper.Map<TobaccoAndAlcoholUse, TobaccoAndAlcoholUseListViewModel>(model);
        
        }

        public async Task<TobaccoAndAlcoholUseListViewModel> Update(int id, TobaccoAndAlcoholUseUpdateViewModel viewModel, string initiatedBy)
        {
            if (viewModel.RespondentId != id)
            {
                this._logger.LogInformation($"Tobacco and alcohol use (Uganda): Discrepancy found in id: {id} and data sent: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            var respondentDb = await this._respondentRepository.Get(id);
            
            if (respondentDb?.TobaccoAndAlcoholUse == null)
            {
                this._logger.LogInformation($"Tobacco and alcohol use (Uganda): Record not found for RID: {viewModel.RespondentId}");
                throw new RecordNotFoundException();
            }
            
            if (respondentDb.Country != Country.Uganda)
            {
                this._logger.LogInformation($"Tobacco and alcohol use (Uganda): Cross-origin access for RID: {viewModel.RespondentId}");
                throw new RecordNotFoundException();
            }

            var modelDb = respondentDb.TobaccoAndAlcoholUse;

            var user = await this._authRepository.GetUserByEmail(initiatedBy);

            var roles = this._authRepository.GetRolesForUser(user);

            var createdBy = modelDb.RegisteredBy;

            var roleIntersection = roles.Intersect(GeneralStaticDataProvider.UgandaAdministratorRoles.Split(','));
            
            if (createdBy == initiatedBy || roleIntersection.Any())
            {
                this._logger.LogInformation($"Tobacco and alcohol use (Uganda): data update initiated for RID: {viewModel.RespondentId} by {initiatedBy}");
                this._mapper.Map(viewModel, modelDb);
                await this._repository.Save(modelDb);
                
                this._logger.LogInformation($"Tobacco and alcohol use (Uganda): data update completed for RID: {id} by {initiatedBy}");
                return this._mapper.Map<TobaccoAndAlcoholUse, TobaccoAndAlcoholUseListViewModel>(modelDb);
            }
            
            throw new UserActionPreventedException();
        }
    }
}