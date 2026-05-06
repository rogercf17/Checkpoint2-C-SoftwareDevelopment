using Microsoft.EntityFrameworkCore;
using Fiap.Banco.API.Models;
using Fiap.Banco.Api.Models;

namespace Fiap.Banco.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contratacao> Contratacoes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasDiscriminator<string>("Tipo")
                .HasValue<PessoaFisica>("PF")
                .HasValue<PessoaJuridica>("PJ");

            modelBuilder.Entity<Produto>()
                .HasDiscriminator<string>("TipoProduto")
                .HasValue<Emprestimo>("Emprestimo")
                .HasValue<ReceberSalario>("ReceberSalario");

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Agencia)
                .WithMany(a => a.Clientes)
                .HasForeignKey(c => c.IdAgencia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contratacao>()
                .HasOne(c => c.Cliente)
                .WithMany(c => c.Contratacoes)
                .HasForeignKey(c => c.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contratacao>()
                .HasOne(c => c.Produto)
                .WithMany(p => p.Contratacoes)
                .HasForeignKey(c => c.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PessoaFisica>()
                .Property(p => p.CPF).HasMaxLength(11);

            modelBuilder.Entity<PessoaJuridica>()
                .Property(p => p.CNPJ).HasMaxLength(14);
            modelBuilder.Entity<PessoaJuridica>()
                .Property(p => p.RazaoSocial).HasMaxLength(180);

            modelBuilder.Entity<Agencia>()
                .Property(a => a.Numero).HasMaxLength(20);

            modelBuilder.Entity<Emprestimo>()
                .Property(e => e.Valor)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Emprestimo>()
                .Property(e => e.TaxaJuros)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Emprestimo>().HasData(
                new Emprestimo { Id = 1, Nome = "Empréstimo" }
            );

            modelBuilder.Entity<ReceberSalario>().HasData(
                new ReceberSalario { Id = 2, Nome = "Receber Salário", EmpresaConveniada = "", ConvenioAtivo = 0 }
            );
        }
    }
}
