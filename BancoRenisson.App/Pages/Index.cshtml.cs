using BancoRenisson.Domain.ContasCorrentes;
using BancoRenisson.Domain.ContasCorrentes.Repositories;
using BancoRenisson.Domain.Movimentacoes;
using BancoRenisson.Domain.Movimentacoes.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BancoRenisson.App.Pages
{
    public class IndextModel : PageModel
    {
        private readonly ICurrentAccountRepository _currentAccountRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly ILogger<IndextModel> _logger;

        public IndextModel(
            ILogger<IndextModel> logger,
            ICurrentAccountRepository currentAccountRepository,
            IMovementRepository movementRepository)
        {
            _logger = logger;
            _currentAccountRepository = currentAccountRepository;
            _movementRepository = movementRepository;
        }

        public IEnumerable<CurrentAccount> Currents { get; set; }
        public IEnumerable<Movement> Movements { get; private set; }
        public CurrentAccount _current { get; set; }

        [BindProperty()]
        public CurrentAccount Current { get; set; }

        [BindProperty()]
        public Movement Movement { get; set; }

        public async Task OnGet()
        {
            Currents = await _currentAccountRepository.GetAll();
            Movements = await _movementRepository.GetAll();
        }

        public async Task<IActionResult> OnPostOpenAccount()
        {
            if (string.IsNullOrEmpty(Current.UserName))
            {
                ModelState.AddModelError("Preencha o nome do titular", "O nome do titular é obrigatorio para abrir contas.");
                await OnGet();

                return Page();
            }

            await _currentAccountRepository.Add(Current);
            await OnGet();

            return Page();
        }

        public async Task<IActionResult> OnPostDeposit()
        {
            if (!ValidatorOperations() || await CurrentAccount())
            {
                await OnGet();

                return Page();
            }

            Movement.CurrentAccountId = _current.Id;
            Movement.CurrentAccount = _current;
            Movement.Deposit(Movement.ValueMovement);
            await _movementRepository.Add(Movement);
            await _currentAccountRepository.Update(Movement.CurrentAccount);

            await OnGet();

            return Page();
        }

        public async Task<IActionResult> OnPostWithdraw()
        {
            if (!ValidatorOperations() || await CurrentAccount())
            {
                await OnGet();

                return Page();
            }

            Movement.CurrentAccountId = _current.Id;
            Movement.CurrentAccount = _current;

            var result = Movement.Withdraw(Movement.ValueMovement);

            if (!result)
            {
                ModelState.AddModelError("Saldo insuficiente", "Não há saldo disponível para realizar a operação");
                await OnGet();

                return Page();
            }

            await _movementRepository.Add(Movement);
            await _currentAccountRepository.Update(Movement.CurrentAccount);
            await OnGet();

            return Page();
        }

        public async Task<IActionResult> OnPostPayment()
        {
            if (!ValidatorOperations(true) || await CurrentAccount())
            {
                await OnGet();

                return Page();
            }

            Movement.CurrentAccountId = _current.Id;
            Movement.CurrentAccount = _current;

            var result = Movement.Payment(Movement.ValueMovement, Movement.Description);

            if (!result)
            {
                ModelState.AddModelError("Saldo insuficiente", "Não há saldo dísponivel para realizar a operação");
                await OnGet();

                return Page();
            }

            await _movementRepository.Add(Movement);
            await _currentAccountRepository.Update(Movement.CurrentAccount);

            await OnGet();

            return Page();
        }

        private async Task<bool> CurrentAccount()
        {
            _current = await _currentAccountRepository.SearchByNumber(Current.NumberAccount);

            if (_current is null)
            {
                ModelState.AddModelError("Conta não localizada", "O número da conta digitado não foi encontrado.");
                return true;
            }
            return false;
        }

        private bool ValidatorOperations(bool pay = false)
        {
            if (Current.NumberAccount <= 0)
            {
                ModelState.AddModelError("Preencha o número da conta", "Número da conta é obrigatorio.");
                return false;
            }

            if (Movement.ValueMovement <= 0)
            {
                ModelState.AddModelError("Preencha o valor", "Valor é obrigatorio.");
                return false;
            }

            if (pay && string.IsNullOrEmpty(Movement.Description))
            {
                ModelState.AddModelError("Preencha a Descrição", "Descrição é obrigatorio para pagamentos.");
                return false;
            }

            return true;
        }
    }
}