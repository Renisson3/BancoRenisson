using BancoRenisson.Domain.ContasCorrentes;
using System;
using Xunit;

namespace BancoRenisson.DomainTest.ContasCorrentes
{
    public class ContaCorrenteTest
    {
        [Fact]
        public void CriarConta()
        {
            var contaCorrente = new CurrentAccount()
            { UserName = "Renisson Machado Santos", NumberAccount = 1, Value = 1000 };

            Assert.True(contaCorrente.DateCreation <= DateTime.UtcNow);
            Assert.True(contaCorrente.DateLastUpdate <= DateTime.UtcNow);
            Assert.Equal(1000, contaCorrente.Value);
            Assert.NotNull(contaCorrente.UserName);
            Assert.NotEmpty(contaCorrente.UserName);
            Assert.True(contaCorrente.NumberAccount > 0);
        }
    }
}