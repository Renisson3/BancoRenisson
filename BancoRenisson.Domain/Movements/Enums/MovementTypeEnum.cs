using System.ComponentModel;

namespace BancoRenisson.Domain.Movimentacoes.Enums
{
    public enum MovementTypeEnum
    {
        [Description("Depósito")]
        Deposit,

        [Description("Saque")]
        Withdraw,

        [Description("Pagamento")]
        Payment,

        [Description("Juros")]
        Interest
    }
}