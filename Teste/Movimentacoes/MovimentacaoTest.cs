using BancoRenisson.Domain.ContasCorrentes;
using BancoRenisson.Domain.Movimentacoes;
using BancoRenisson.Domain.Movimentacoes.Enums;
using System;
using Xunit;

namespace BancoRenisson.DomainTest.Movimentacoes
{
    public class MovimentacaoTest
    {
        [Fact]
        public void CriarMovimentacao()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            Assert.True(movimentacao.DateCreation <= DateTime.UtcNow);
            Assert.True(movimentacao.DateLastUpdate <= DateTime.UtcNow);
            Assert.Equal(1000, movimentacao.CurrentAccount.Value);
        }

        [Fact]
        public void DepositarValor()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            movimentacao.Deposit(1000);

            Assert.True(movimentacao.DateCreation <= DateTime.UtcNow);
            Assert.True(movimentacao.DateLastUpdate <= DateTime.UtcNow);
            Assert.Equal(1000, movimentacao.ValueMovement);
            Assert.Equal(2000, movimentacao.CurrentAccount.Value);
            Assert.Equal(MovementTypeEnum.Deposit, movimentacao.Operation);
        }

        [Fact]
        public void SacarValor()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            Assert.True(movimentacao.Withdraw(500));
            Assert.True(movimentacao.DateCreation <= DateTime.UtcNow);
            Assert.True(movimentacao.DateLastUpdate <= DateTime.UtcNow);
            Assert.Equal(500, movimentacao.ValueMovement);
            Assert.Equal(500, movimentacao.CurrentAccount.Value);
            Assert.Equal(MovementTypeEnum.Withdraw, movimentacao.Operation);
        }

        [Fact]
        public void SacarValorNegativo()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            Assert.False(movimentacao.Withdraw(1500));
            Assert.Equal(0, movimentacao.ValueMovement);
            Assert.Equal(1000, movimentacao.CurrentAccount.Value);
            Assert.NotEqual(MovementTypeEnum.Withdraw, movimentacao.Operation);
        }

        [Fact]
        public void SacarZerarSaldo()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            Assert.True(movimentacao.Withdraw(1000));
            Assert.Equal(1000, movimentacao.ValueMovement);
            Assert.Equal(0, movimentacao.CurrentAccount.Value);
            Assert.Equal(MovementTypeEnum.Withdraw, movimentacao.Operation);
        }

        [Fact]
        public void PagamentoValor()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            Assert.True(movimentacao.Payment(500, "Boleto"));
            Assert.Equal(500, movimentacao.ValueMovement);
            Assert.Equal(500, movimentacao.CurrentAccount.Value);
            Assert.NotNull(movimentacao.Description);
            Assert.Equal("Boleto", movimentacao.Description);
            Assert.Equal(MovementTypeEnum.Payment, movimentacao.Operation);
        }

        [Fact]
        public void PagamentoValorNegativo()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            Assert.False(movimentacao.Payment(1500, "Boleto"));
            Assert.Equal(0, movimentacao.ValueMovement);
            Assert.Equal(1000, movimentacao.CurrentAccount.Value);
            Assert.NotNull(movimentacao.Description);
            Assert.Equal("Boleto", movimentacao.Description);
            Assert.NotEqual(MovementTypeEnum.Payment, movimentacao.Operation);
        }

        [Fact]
        public void PagamentoZerarSaldo()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            Assert.True(movimentacao.Payment(1000, "Boleto"));
            Assert.Equal(1000, movimentacao.ValueMovement);
            Assert.Equal(0, movimentacao.CurrentAccount.Value);
            Assert.NotNull(movimentacao.Description);
            Assert.Equal("Boleto", movimentacao.Description);
            Assert.Equal(MovementTypeEnum.Payment, movimentacao.Operation);
        }

        [Fact]
        public void PagamentoDescricaoNull()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            Assert.False(movimentacao.Payment(1000, null));
            Assert.Equal(0, movimentacao.ValueMovement);
            Assert.Equal(1000, movimentacao.CurrentAccount.Value);
            Assert.Null(movimentacao.Description);
            Assert.NotEqual(MovementTypeEnum.Payment, movimentacao.Operation);
        }

        [Fact]
        public void PagamentoDescricaoVazia()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            Assert.False(movimentacao.Payment(1000, string.Empty));
            Assert.Equal(0, movimentacao.ValueMovement);
            Assert.Equal(1000, movimentacao.CurrentAccount.Value);
            Assert.Null(movimentacao.Description);
            Assert.NotEqual(MovementTypeEnum.Payment, movimentacao.Operation);
        }

        [Fact]
        public void CalcularJuros()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            movimentacao.CalculateInterest();

            Assert.Equal(5, movimentacao.ValueMovement);
            Assert.Equal(1005, movimentacao.CurrentAccount.Value);
            Assert.Equal(MovementTypeEnum.Interest, movimentacao.Operation);
        }

        [Fact]
        public void CalcularJurosCentavos()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = (decimal)0.5 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            movimentacao.CalculateInterest();

            Assert.Equal((decimal)0.0025, movimentacao.ValueMovement);
            Assert.Equal((decimal)0.5025, movimentacao.CurrentAccount.Value);
            Assert.Equal(MovementTypeEnum.Interest, movimentacao.Operation);
        }

        [Fact]
        public void CalcularJurosZerado()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 0 };

            var movimentacao = new Movement();
            movimentacao.CurrentAccount = contaCorrente;

            movimentacao.CalculateInterest();

            Assert.Equal(0, movimentacao.ValueMovement);
            Assert.Equal(0, movimentacao.CurrentAccount.Value);
            Assert.NotEqual(MovementTypeEnum.Interest, movimentacao.Operation);
        }
    }
}