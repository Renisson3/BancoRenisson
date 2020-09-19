using BancoRenisson.Domain.ContasCorrentes;
using BancoRenisson.Domain.Movimentacoes.Enums;
using Envolva.Domain.Core.Entities;
using Envolva.Infra.CrossCutting.Helper.Extensions;
using System;

namespace BancoRenisson.Domain.Movimentacoes
{
    public class Movement : Entity
    {
        public decimal ValueMovement { get; set; }
        public string Description { get; set; }
        public Guid CurrentAccountId { get; set; }
        public CurrentAccount CurrentAccount { get; set; }
        public MovementTypeEnum Operation { get; set; }
        public string OperationDescription { get; set; }

        public Movement()
        {
        }

        public void Deposit(decimal valor)
        {
            Operation = MovementTypeEnum.Deposit;
            OperationDescription = Operation.GetDescription();
            ValueMovement = valor;
            CurrentAccount.Value += valor;
        }

        public bool Withdraw(decimal valor)
        {
            if (decimal.Subtract(CurrentAccount.Value, valor) < 0)
            {
                return false;
            }

            Operation = string.IsNullOrEmpty(Description)
                ? MovementTypeEnum.Withdraw
                : MovementTypeEnum.Payment;
            OperationDescription = Operation.GetDescription();
            ValueMovement = valor;
            CurrentAccount.Value -= ValueMovement;

            return true;
        }

        public bool Payment(decimal valor, string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
            {
                return false;
            }

            Description = descricao;
            return Withdraw(valor);
        }

        public void CalculateInterest()
        {
            if (CurrentAccount.Value > 0)
            {
                ValueMovement = decimal.Multiply(CurrentAccount.Value, (decimal)0.005);
                CurrentAccount.Value += ValueMovement;
                Operation = MovementTypeEnum.Interest;
                OperationDescription = Operation.GetDescription();
            }
        }
    }
}