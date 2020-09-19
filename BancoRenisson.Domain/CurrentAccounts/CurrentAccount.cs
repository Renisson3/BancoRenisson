using BancoRenisson.Domain.Movimentacoes;
using Envolva.Domain.Core.Entities;
using System.Collections.Generic;

namespace BancoRenisson.Domain.ContasCorrentes
{
    public class CurrentAccount : Entity
    {
        public CurrentAccount()
        {
        }

        public int NumberAccount { get; set; }
        public string UserName { get; set; }
        public decimal Value { get; set; }
        public ICollection<Movement> Movements { get; set; }
    }
}