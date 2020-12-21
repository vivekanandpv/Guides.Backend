using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Domain;
using Guides.Backend.Exceptions.Auth;
using Guides.Backend.Exceptions.Domain;
using Guides.Backend.Repositories.Auth;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Guides.Backend.Services.Baseline.Interfaces;
using Guides.Backend.Services.Baseline.Interfaces.India;
using Guides.Backend.StaticProviders;
using Guides.Backend.ViewModels.Baseline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Guides.Backend.Services.Baseline.Implementations.India
{
    public class IndiaDietaryBehaviourService : IIndiaDietaryBehaviourService
    {
        private readonly IDietaryBehaviourRepository _repository;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IRespondentRepository _respondentRepository;
        private readonly ILogger _logger;

        public IndiaDietaryBehaviourService(
            IDietaryBehaviourRepository repository, 
            IAuthRepository authRepository, 
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IRespondentRepository respondentRepository)
        {
            _repository = repository;
            _authRepository = authRepository;
            _mapper = mapper;
            _respondentRepository = respondentRepository;
            _logger = loggerFactory.CreateLogger(GeneralStaticDataProvider.DietaryBehaviourCategory);
        }
        public async Task<IEnumerable<DietaryBehaviourListViewModel>> Get()
        {
            return await this._repository
                .Get()
                .Include(m => m.Respondent)
                .Where(m => m.Respondent.Country == Country.India)
                .Select(
                    r => this._mapper.Map<DietaryBehaviour, DietaryBehaviourListViewModel>(r)
                ).ToListAsync();
        }

        public async Task<DietaryBehaviourListViewModel> Get(int id)
        {
            var model = await this._repository
                .Get(id);

            if (model.Respondent.Country != Country.India)
            {
                this._logger.LogInformation($"Dietary behaviour (India): Cross region access to respondent id: {id} is blocked");
                throw new UserActionNotSupportedException();
            }
                
            return this._mapper.Map<DietaryBehaviour, DietaryBehaviourListViewModel>(model);

        }

        public async Task<DietaryBehaviourListViewModel> Register(DietaryBehaviourRegisterViewModel viewModel, string initiatedBy)
        {
            if (viewModel.RegisteredBy != initiatedBy)
            {
                this._logger.LogInformation($"Prevented registration of dietary behaviour (India) as user discrepancy found registration data: {viewModel.RegisteredBy}; auth system: {initiatedBy}");
                throw new UserActionNotSupportedException();
            }
            
            this._logger.LogInformation($"dietary behaviour (India) registration for RID: {viewModel.RespondentId} is initiated");
            var model = this._mapper.Map<DietaryBehaviourRegisterViewModel, DietaryBehaviour>(viewModel);

            var respondent = await this._respondentRepository.Get(viewModel.RespondentId);

            if (respondent == null)
            {
                this._logger.LogInformation($"Prevented registration of dietary behaviour (India) for non existent RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            if (respondent.DeathRecord != null)
            {
                this._logger.LogInformation($"Prevented registration of dietary behaviour (India) for deceased respondent RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            if (respondent.LossToFollowUp != null)
            {
                this._logger.LogInformation($"Prevented registration of dietary behaviour (India) for blocked respondent RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }

            if (respondent.User.Country != Country.India)
            {
                this._logger.LogInformation($"Prevented cross-region registration of dietary behaviour (India) for RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }

            if (respondent.DietaryBehaviour != null)
            {
                this._logger.LogInformation($"Prevented duplicate registration of dietary behaviour (India) for RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            model.Respondent = respondent;
            model.RegisteredBy = initiatedBy;
            model.DateOfActualEntry = DateTime.UtcNow;

            this._logger.LogInformation($"Persistence for dietary behaviour (India) registration for RID: {viewModel.RespondentId} is initiated");
            await this._repository.Add(model);
            this._logger.LogInformation($"Dietary behaviour (India) registration for RID: {viewModel.RespondentId} succeeded. Id: {respondent.Id}");

            return this._mapper.Map<DietaryBehaviour, DietaryBehaviourListViewModel>(model);
        
        }

        public async Task<DietaryBehaviourListViewModel> Update(int id, DietaryBehaviourUpdateViewModel viewModel, string initiatedBy)
        {
            if (viewModel.RespondentId != id)
            {
                this._logger.LogInformation($"Dietary behaviour (India): Discrepancy found in id: {id} and data sent: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            var respondentDb = await this._respondentRepository.Get(id);
            
            if (respondentDb?.SocioDemographic == null)
            {
                this._logger.LogInformation($"Dietary behaviour (India): Record not found for RID: {viewModel.RespondentId}");
                throw new RecordNotFoundException();
            }
            
            if (respondentDb.Country != Country.India)
            {
                this._logger.LogInformation($"Dietary behaviour (India): Cross-origin access for RID: {viewModel.RespondentId}");
                throw new RecordNotFoundException();
            }

            var modelDb = respondentDb.DietaryBehaviour;

            var user = await this._authRepository.GetUserByEmail(initiatedBy);

            var roles = this._authRepository.GetRolesForUser(user);

            var createdBy = modelDb.RegisteredBy;

            var roleIntersection = roles.Intersect(GeneralStaticDataProvider.IndiaAdministratorGroup);
            
            if (createdBy == initiatedBy || roleIntersection.Any())
            {
                this._logger.LogInformation($"Dietary behaviour (India): data update initiated for RID: {viewModel.RespondentId} by {initiatedBy}");
                this._mapper.Map(viewModel, modelDb);
                await this._repository.Save(modelDb);
                
                this._logger.LogInformation($"Dietary behaviour (India): data update completed for RID: {id} by {initiatedBy}");
                return this._mapper.Map<DietaryBehaviour, DietaryBehaviourListViewModel>(modelDb);
            }
            
            throw new UserActionPreventedException();
        }
    }
}