using Microsoft.EntityFrameworkCore;
using API.model;

public class APIDbContext : DbContext
{
    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }

    public DbSet<Moni> Moni { get; set; }
    public DbSet<Horario> Horario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        const string schema = "monitoria";
        // Tabela Moni (Monitor)
        modelBuilder.Entity<Moni>(entity =>
        {
            entity.ToTable("Moni", schema); 
            entity.HasKey(m => m.IdMonitor);

            entity.Property(m => m.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(m => m.Apelido)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(m => m.RA)
                .IsRequired()
                .HasMaxLength(20);
        });

        // Tabela Horario
        modelBuilder.Entity<Horario>(entity =>
        {
            entity.ToTable("Horario",  schema);
            entity.HasKey(h => h.IdHorario);

            entity.Property(h => h.DiaSemana)
                .IsRequired();

            entity.Property(h => h.horario)
                .HasColumnName("horario")
                .IsRequired()
                .HasMaxLength(15);

            entity.HasOne(h => h.Monitor)
                .WithMany()
                .HasForeignKey(h => h.IdMonitor)
                .OnDelete(DeleteBehavior.Restrict); 
        });
    }
}