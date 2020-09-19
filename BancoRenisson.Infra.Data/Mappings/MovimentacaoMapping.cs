using BancoRenisson.Domain.Movimentacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoRenisson.Infra.Data.Mappings
{
    public static class MovimentacaoMapping
    {
        public static void Map(this EntityTypeBuilder<Movement> builder)
        {
            builder.ToTable("tbl_MOV_Movimentacao");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever().HasColumnName("mov_id");
            builder.Property(p => p.ValueMovement).HasColumnName("mov_valor").IsRequired();
            builder.Property(p => p.Description).HasColumnName("mov_descricao").IsRequired(false);
            builder.Property(p => p.DateCreation).HasColumnName("mov_dataCriacao").IsRequired();
            builder.Property(p => p.DateLastUpdate).HasColumnName("mov_dataUltimaAtualizacao").IsRequired();
            builder.Property(p => p.Operation).HasColumnName("mov_tipo_operacao").IsRequired();
            builder.Property(p => p.OperationDescription).HasColumnName("mov_tipo_operacao_descricao").IsRequired();
            builder.Property(p => p.CurrentAccountId).HasColumnName("cor_id").IsRequired();

            builder.HasOne(p => p.CurrentAccount)
              .WithMany(p => p.Movements)
              .HasForeignKey(p => p.CurrentAccountId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(p => p.ValidationResult);
        }
    }
}