using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        // ========================
        // === Dbsets
        // ========================

        private DbSet<Psychologist> Psychologists { get; set; }
        private DbSet<SchoolManager> SchoolManagers { get; set; }
        private DbSet<Student> Students { get; set; }
        private DbSet<Resource> Resources { get; set; }
        private DbSet<Feedback> Feedbacks { get; set; }
        private DbSet<ResourceSection> ResourceSections { get; set; }
        private DbSet<SupportingProgram> SupportingPrograms { get; set; }
        private DbSet<SupportingProgramHistory> SupportingProgramHistories { get; set; }
        private DbSet<School> Schools { get; set; }
        private DbSet<Specification> Specifications { get; set; }


        // ========================
        // === Methods
        // ========================

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations in the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Apply base entity to all entities
            ApplyBaseEntityToDerivedClass(modelBuilder);
        }

        /// <summary>
        /// Configuring the base entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void ApplyBaseEntityToDerivedClass(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType) && entityType.ClrType != typeof(BaseEntity))
                {
                    // Configure Id
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.Id))
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .UseIdentityColumn();

                    // Configure CreatedAt
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.CreateAt))
                        .IsRequired()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP")
                        .ValueGeneratedOnAdd();

                    // Configure UpdatedAt
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.UpdateAt))
                        .IsRequired()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP")
                        .ValueGeneratedOnAddOrUpdate();
                }
            }
        }
    }
}
