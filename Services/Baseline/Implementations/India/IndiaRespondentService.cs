﻿using System;
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
    public class IndiaRespondentService : IIndiaRespondentService
    {
        private readonly IRespondentRepository _repository;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public IndiaRespondentService(
            IRespondentRepository repository, 
            IAuthRepository authRepository, 
            IMapper mapper,
            ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _authRepository = authRepository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger(GeneralStaticDataProvider.RespondentCategory);
        }
        public async Task<IEnumerable<RespondentIndiaListViewModel>> Get()
        {
            return await this._repository
                .Get()
                .Include(r => r.User)
                .Where(r => r.Country == Country.India)
                .Select(
                    r => this._mapper.Map<Respondent, RespondentIndiaListViewModel>(r)
                ).ToListAsync();
        }

        public async Task<RespondentIndiaListViewModel> Get(int id)
        {
            var respondent = await this._repository
                .Get(id);

            if (respondent.Country != Country.India)
            {
                this._logger.LogInformation($"Cross region access to respondent id: {id} is blocked");
                throw new UserActionNotSupportedException();
            }
                
            return this._mapper.Map<Respondent, RespondentIndiaListViewModel>(respondent);
        }

        public async Task<RespondentIndiaListViewModel> Register(RespondentIndiaRegisterViewModel viewModel, string initiatedBy)
        {
            if (viewModel.RegisteredBy != initiatedBy)
            {
                this._logger.LogInformation($"Prevented registration of respondent as user discrepancy found registration data: {viewModel.RegisteredBy}; auth system: {initiatedBy}");
                throw new UserActionNotSupportedException();
            }
            
            this._logger.LogInformation($"Respondent registration of {viewModel.FullName} is initiated");
            var respondent = this._mapper.Map<RespondentIndiaRegisterViewModel, Respondent>(viewModel);
            respondent.Country = Country.India;
            respondent.User = await this._authRepository.GetUserByEmail(viewModel.RegisteredBy);
            respondent.DateOfActualEntry = DateTime.UtcNow;

            this._logger.LogInformation($"Persistence for respondent registration of {viewModel.FullName} is initiated");
            await this._repository.Add(respondent);
            this._logger.LogInformation($"Respondent registration of {viewModel.FullName} succeeded. Id: {respondent.Id}");

            return this._mapper.Map<Respondent, RespondentIndiaListViewModel>(respondent);
        }

        public async Task<RespondentIndiaListViewModel> Update(int id, RespondentIndiaUpdateViewModel viewModel, string initiatedBy)
        {
            if (viewModel.Id != id)
            {
                this._logger.LogInformation($"Discrepancy found in id: {id} and data sent: {viewModel.Id}");
                throw new UserActionPreventedException();
            }
            
            var respondentDb = await this._repository.Get(id);

            if (respondentDb == null)
            {
                this._logger.LogInformation($"Target respondent is not found for id: {id}");
                throw new RecordNotFoundException();
            }

            var user = await this._authRepository.GetUserByEmail(initiatedBy);

            var roles = this._authRepository.GetRolesForUser(user);

            var createdBy = respondentDb.User.Email;

            var roleIntersection = roles.Intersect(GeneralStaticDataProvider.IndiaAdministratorGroup);
            
            if (respondentDb.User.Email == initiatedBy || roleIntersection.Any())
            {
                this._logger.LogInformation($"Respondent data update initiated for id: {id} by {initiatedBy}");
                this._mapper.Map(viewModel, respondentDb);
                await this._repository.Save(respondentDb);
                
                this._logger.LogInformation($"Respondent data update completed for id: {id} by {initiatedBy}");
                return this._mapper.Map<Respondent, RespondentIndiaListViewModel>(respondentDb);
            }
            
            throw new UserActionPreventedException();
        }
    }
}