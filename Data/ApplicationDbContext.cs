using Microsoft.EntityFrameworkCore;




public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Authority> Authorities { get; set; }

    public virtual DbSet<Personel> Personels { get; set; }

    public virtual DbSet<Stage> Stages { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Authority>(entity =>
        {
            entity.HasKey(e => e.AuthoritiesId);

            entity.ToTable("authorities");

            entity.Property(e => e.AuthoritiesId).HasColumnName("authoritiesID");
            entity.Property(e => e.AuthoritesName)
                .HasMaxLength(50)
                .HasColumnName("authoritesName");
        });

        modelBuilder.Entity<Personel>(entity =>
        {
            entity.ToTable("Personel");

            entity.Property(e => e.PersonelId).HasColumnName("personelID");
            entity.Property(e => e.PersonelAuthoritesId).HasColumnName("personelAuthoritesID");
            entity.Property(e => e.PersonelName)
                .HasMaxLength(50)
                .HasColumnName("personelName");
            entity.Property(e => e.PersonelPassword)
                .HasMaxLength(100)
                .HasColumnName("personelPassword");
            entity.Property(e => e.PersonelUnitId).HasColumnName("personelUnitID");
            entity.Property(e => e.PersonelUserName)
                .HasMaxLength(50)
                .HasColumnName("personelUserName");

            entity.HasOne(d => d.PersonelAuthorites).WithMany(p => p.Personels)
                .HasForeignKey(d => d.PersonelAuthoritesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personel_authorities");

            entity.HasOne(d => d.PersonelUnit).WithMany(p => p.Personels)
                .HasForeignKey(d => d.PersonelUnitId)
                .HasConstraintName("FK_Personel_Unit");
        });

        modelBuilder.Entity<Stage>(entity =>
        {
            entity.ToTable("stages");

            entity.Property(e => e.StageId).HasColumnName("stageID");
            entity.Property(e => e.StageName)
                .HasMaxLength(50)
                .HasColumnName("stageName");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("Unit");

            entity.Property(e => e.UnitId).HasColumnName("unitID");
            entity.Property(e => e.UnitName)
                .HasMaxLength(50)
                .HasColumnName("unitName");
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.ToTable("Work");

            entity.Property(e => e.WorkId).HasColumnName("workID");
            entity.Property(e => e.WorkAndDate)
                .HasColumnType("datetime")
                .HasColumnName("workAndDate");
            entity.Property(e => e.WorkComment).HasColumnName("workComment");
            entity.Property(e => e.WorkName).HasColumnName("workName");
            entity.Property(e => e.WorkPersonelId).HasColumnName("workPersonelID");
            entity.Property(e => e.WorkStageId).HasColumnName("workStageID");
            entity.Property(e => e.WorkStartDate)
                .HasColumnType("datetime")
                .HasColumnName("workStartDate");

            entity.HasOne(d => d.WorkPersonel).WithMany(p => p.Works)
                .HasForeignKey(d => d.WorkPersonelId)
                .HasConstraintName("FK_Work_Personel");

            entity.HasOne(d => d.WorkStage).WithMany(p => p.Works)
                .HasForeignKey(d => d.WorkStageId)
                .HasConstraintName("FK_Work_stages");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
