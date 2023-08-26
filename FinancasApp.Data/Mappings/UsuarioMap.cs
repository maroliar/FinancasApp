using FinancasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace FinancasApp.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(50)
                .IsRequired();

            // Nao pode ter duas pessoas com mesmo email
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.DataHoraCriacao)
                .HasColumnName("DATAHORACRIACAO")
                .HasColumnType("datetime") // nao precisa, mas apenas se quiser especificar
                .IsRequired();

        }
    }
}
