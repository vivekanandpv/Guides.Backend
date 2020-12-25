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
using Guides.Backend.Services.Baseline.Interfaces.India;
using Guides.Backend.StaticProviders;
using Guides.Backend.ViewModels.Baseline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Guides.Backend.Services.Baseline.Implementations.India
{
    public class IndiaDeathRecordService : IIndiaDeathRecordService
    {
        private readonly IDeathRecordRepository _repository;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IRespondentRepository _respondentRepository;
        private readonly ILogger _logger;

        public IndiaDeathRecordService(
            IDeathRecordRepository repository, 
            IAuthRepository authRepository, 
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IRespondentRepository respondentRepository)
        {
            _repository = repository;
            _authRepository = authRepository;
            _mapper = mapper;
            _respondentRepository = respondentRepository;
            _logger = loggerFactory.CreateLogger(GeneralStaticDataProvider.DeathRecordCategory);
        }
        public async Task<IEnumerable<DeathRecordListViewModel>> Get()
        {
            return await this._repository
                .Get()
                .Include(m => m.Respondent)
                .Where(m => m.Respondent.Country == Country.India)
                .Select(
                    r => this._mapper.Map<DeathRecord, DeathRecordListViewModel>(r)
                ).ToListAsync();
        }

        public async Task<DeathRecordListViewModel> Get(int id)
        {
            var model = await this._repository
                .Get(id);

            if (model.Respondent.Country != Country.India)
            {
                this._logger.LogInformation($"Death record (India): Cross region access to respondent id: {id} is blocked");
                throw new UserActionNotSupportedException();
            }
                
            return this._mapper.Map<DeathRecord, DeathRecordListViewModel>(model);

        }

        public async Task<DeathRecordListViewModel> Register(DeathRecordRegisterViewModel viewModel, string initiatedBy)
        {
            if (viewModel.RegisteredBy != initiatedBy)
            {
                this._logger.LogInformation($"Prevented registration of death record (India) as user discrepancy found registration data: {viewModel.RegisteredBy}; auth system: {initiatedBy}");
                throw new UserActionNotSupportedException();
            }
            
            this._logger.LogInformation($"death record (India) registration for RID: {viewModel.RespondentId} is initiated");
            var model = this._mapper.Map<DeathRecordRegisterViewModel, DeathRecord>(viewModel);

            var respondent = await this._respondentRepository.Get(viewModel.RespondentId);

            if (respondent == null)
            {
                this._logger.LogInformation($"Prevented registration of death record (India) for non existent RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            if (respondent.DeathRecord != null)
            {
                this._logger.LogInformation($"Prevented registration of death record (India) for deceased respondent RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            if (respondent.LossToFollowUp != null)
            {
                this._logger.LogInformation($"Prevented registration of death record (India) for blocked respondent RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }

            if (respondent.User.Country != Country.India)
            {
                this._logger.LogInformation($"Prevented cross-region registration of death record (India) for RID: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }

            if (respondent.DeathRecord != null)
            {
                this._logger.LogInformation($"Prevented duplicate registration of death record (India) for RID: {viewModel.RespondentId}");
                throw new DuplicatePreventionException();
            }
            
            model.Respondent = respondent;
            model.RegisteredBy = initiatedBy;
            model.DateOfActualEntry = DateTime.UtcNow;

            this._logger.LogInformation($"Persistence for death record (India) registration for RID: {viewModel.RespondentId} is initiated");
            await this._repository.Add(model);
            this._logger.LogInformation($"Death record (India) registration for RID: {viewModel.RespondentId} succeeded. Id: {respondent.Id}");

            return this._mapper.Map<DeathRecord, DeathRecordListViewModel>(model);
        
        }

        public async Task<DeathRecordListViewModel> Update(int id, DeathRecordUpdateViewModel viewModel, string initiatedBy)
        {
            if (viewModel.RespondentId != id)
            {
                this._logger.LogInformation($"Death record (India): Discrepancy found in id: {id} and data sent: {viewModel.RespondentId}");
                throw new UserActionPreventedException();
            }
            
            var respondentDb = await this._respondentRepository.Get(id);
            
            if (respondentDb?.SocioDemographic == null)
            {
                this._logger.LogInformation($"Death record (India): Record not found for RID: {viewModel.RespondentId}");
                throw new RecordNotFoundException();
            }
            
            if (respondentDb.Country != Country.India)
            {
                this._logger.LogInformation($"Death record (India): Cross-origin access for RID: {viewModel.RespondentId}");
                throw new RecordNotFoundException();
            }

            var modelDb = respondentDb.DeathRecord;

            var user = await this._authRepository.GetUserByEmail(initiatedBy);

            var roles = this._authRepository.GetRolesForUser(user);

            var createdBy = modelDb.RegisteredBy;

            var roleIntersection = roles.Intersect(GeneralStaticDataProvider.IndiaAdministratorRoles.Split(','));
            
            if (createdBy == initiatedBy || roleIntersection.Any())
            {
                this._logger.LogInformation($"Death record (India): data update initiated for RID: {viewModel.RespondentId} by {initiatedBy}");
                this._mapper.Map(viewModel, modelDb);
                await this._repository.Save(modelDb);
                
                this._logger.LogInformation($"Death record (India): data update completed for RID: {id} by {initiatedBy}");
                return this._mapper.Map<DeathRecord, DeathRecordListViewModel>(modelDb);
            }
            
            throw new UserActionPreventedException();
        }
    }
}