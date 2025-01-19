using DummbAPI.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DummbAPI.Persistence
{
    public partial class DummyDbContext : DbContext
    {
        public DummyDbContext()
        {
        }

        public DummyDbContext(DbContextOptions<DummyDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<DummyRight> DummyRights { get; set; }

        public virtual DbSet<DummyRole> DummyRoles { get; set; }

        public virtual DbSet<DummyUser> DummyUsers { get; set; }

        public virtual DbSet<DummyUserRight> DummyUserRights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DummyRight>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("dummy_right_pkey");

                entity.ToTable("dummy_right");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DummyRole>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("dummy_role_pkey");

                entity.ToTable("dummy_role");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DummyUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("dummy_user_pkey");

                entity.ToTable("dummy_user");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Active).HasColumnName("active");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");
                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");
                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role).WithMany(p => p.DummyUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dummy_user_role_id_fkey");
            });

            modelBuilder.Entity<DummyUserRight>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("dummy_user_right_pkey");

                entity.ToTable("dummy_user_right");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.RightId).HasColumnName("right_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Right).WithMany(p => p.DummyUserRights)
                    .HasForeignKey(d => d.RightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dummy_user_right_right_id_fkey");

                entity.HasOne(d => d.User).WithMany(p => p.DummyUserRights)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dummy_user_right_user_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
