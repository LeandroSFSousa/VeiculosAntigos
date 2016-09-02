namespace VeiculosAntigos.Data.EF.Context
{
    using Model;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class VeiculosAntigosEFModel : DbContext
    {
        public VeiculosAntigosEFModel()
            : base("name=VeiculosAntigosEFModel")
        {
        }

        public virtual DbSet<Fabricante> Fabricante { get; set; }
        public virtual DbSet<TipoDeVeiculo> TipoDeVeiculo { get; set; }
        public virtual DbSet<Veiculo> Veiculo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Fabricante>()
                .Property(e => e.NomeFabricante)
                .IsUnicode(false);

            modelBuilder.Entity<Fabricante>()
                .HasMany(e => e.Veiculo)
                .WithRequired(e => e.Fabricante)
                .HasForeignKey(e => e.IdFabricante)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoDeVeiculo>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDeVeiculo>()
                .HasMany(e => e.Veiculo)
                .WithRequired(e => e.TipoDeVeiculo)
                .HasForeignKey(e => e.IdTipo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Veiculo>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Veiculo>()
                .HasRequired<TipoDeVeiculo>(e => e.TipoDeVeiculo)
                .WithMany(e => e.Veiculo)
                .HasForeignKey(e => e.IdTipo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Veiculo>()
                .HasRequired<Fabricante>(e => e.Fabricante)
                .WithMany(e => e.Veiculo)
                .HasForeignKey(e => e.IdFabricante)
                .WillCascadeOnDelete(false);

        }
    }
}
