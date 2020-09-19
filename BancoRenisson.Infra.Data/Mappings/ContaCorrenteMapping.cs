using BancoRenisson.Domain.ContasCorrentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoRenisson.Infra.Data.Mappings
{
    public static class ContaCorrenteMapping
    {
        public static void Map(this EntityTypeBuilder<CurrentAccount> builder)
        {
            builder.ToTable("tbl_COR_ContaCorrente");

            builder.HasKey(p => p.Id);
            builder.HasAlternateKey(p => p.NumberAccount);
            builder.Property(p => p.Id).ValueGeneratedNever().HasColumnName("cor_id");
            builder.Property(p => p.NumberAccount).HasColumnName("cor_numero").IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.UserName).HasColumnName("cor_nomeTitular").IsRequired();
            builder.Property(p => p.Value).HasColumnName("cor_valorDisponivel").IsRequired();
            builder.Property(p => p.DateCreation).HasColumnName("cor_dataCriacao").IsRequired();
            builder.Property(p => p.DateLastUpdate).HasColumnName("cor_dataUltimaAtualizacao").IsRequired();

            builder.Ignore(p => p.ValidationResult);
        }
    }
}