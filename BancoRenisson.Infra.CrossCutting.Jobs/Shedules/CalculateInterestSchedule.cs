using BancoRenisson.Domain.ContasCorrentes.Repositories;
using BancoRenisson.Domain.Movimentacoes;
using BancoRenisson.Domain.Movimentacoes.Repositories;
using Envolva.Infra.CrossCutting.Jobs.Schedules.Contracts;
using Hangfire;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Envolva.Infra.CrossCutting.Jobs.Schedules
{
    public class CalculateInterestSchedule
    {
        private readonly JobScheduleOptionsDiary _jobScheduleOptions;
        private readonly ICurrentAccountRepository _currentAccountRepository;
        private readonly IMovementRepository _movementRepository;

        public CalculateInterestSchedule(
            IOptionsMonitor<JobScheduleOptionsDiary> options,
            ICurrentAccountRepository currentAccountRepository,
            IMovementRepository movementRepository)
        {
            _jobScheduleOptions = options.CurrentValue;
            _currentAccountRepository = currentAccountRepository;
            _movementRepository = movementRepository;
        }

        public void ApplyJob()
        {
            RecurringJob.AddOrUpdate(() => ExecuteAsync(),
                $"*/{_jobScheduleOptions.TimeToUpdate} {_jobScheduleOptions.StartHour}-{_jobScheduleOptions.EndHour} * * *",
                TimeZoneInfo.Local);
        }

        public async Task ExecuteAsync()
        {
            var accounts = await _currentAccountRepository.GetAllCalculateInterest();

            foreach (var account in accounts)
            {
                var movement = new Movement();
                movement.CurrentAccount = account;
                movement.CurrentAccountId = account.Id;
                movement.CalculateInterest();
                await _movementRepository.Add(movement);
                await _currentAccountRepository.Update(movement.CurrentAccount);
            }
        }
    }
}