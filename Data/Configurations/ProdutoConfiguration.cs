﻿using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(p => p.Descricao).HasColumnType("VARCHAR(60)");
            builder.Property(p => p.Valor).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.TipoProduto).HasConversion<string>();
        }
    }
}
