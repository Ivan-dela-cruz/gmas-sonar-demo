using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Domain.Padron.Entities;
using BUE.Inscriptions.Domain.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace BUE.Inscriptions.Infrastructure
{
    public class PortalMatriculasDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public PortalMatriculasDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplySoftDeleteQueryFilter();

            #region V1
            modelBuilder.Entity<UserRole>()
               .HasKey(bc => new { bc.userCode, bc.roleCode });
            modelBuilder.Entity<UserRole>()
                .HasOne(bc => bc.user)
                .WithMany(b => b.userRoles)
                .HasForeignKey(bc => bc.userCode);
            modelBuilder.Entity<UserRole>()
                .HasOne(bc => bc.role)
                .WithMany(c => c.userRoles)
                .HasForeignKey(bc => bc.roleCode);

            //modelBuilder.Entity<StudentPortal>()
            //   .HasOne(e => e.level)
            //   .WithMany(c => c.students);

            modelBuilder.Entity<StudentPortal>()
              .HasOne(e => e.courseGrade)
              .WithMany(c => c.students);

            //modelBuilder.Entity<CourseGradePortal>()
            // .HasOne(e => e.level)
            // .WithOne(c => c.courseGrade);

            modelBuilder.Entity<AuthorizePeople>()
              .HasOne(e => e.PortalRequest)
              .WithMany(c => c.AuthorizePeople);

            modelBuilder.Entity<StudentPortal>()
            .HasOne(a => a.PortalRequest)
            .WithOne(b => b.StudentPortal)
            .HasForeignKey<PortalRequest>(b => b.studentCodeSchoolYear);

            modelBuilder.Entity<PortalRequest>()
            .HasOne(a => a.FinanceInformation)
            .WithOne(b => b.PortalRequest)
            .HasForeignKey<FinanceInformation>(b => b.requestCode);

            modelBuilder.Entity<PortalRequest>()
           .HasOne(a => a.MedicalRecord)
           .WithOne(b => b.PortalRequest)
           .HasForeignKey<MedicalRecord>(b => b.RequestId);

            modelBuilder.Entity<PortalSchoolYear>()
           .HasOne(a => a.Application)
           .WithOne(b => b.SchoolYear)
           .HasForeignKey<Application>(b => b.currentSchoolYear);

            modelBuilder.Entity<ContactPortal>()
           .HasMany(a => a.PortalRequest)
           .WithOne(b => b.FirstContact)
           .HasForeignKey(b => b.contactCodeFirst);

            modelBuilder.Entity<ContactPortal>()
           .HasMany(a => a.SecondRequest)
           .WithOne(b => b.SecondContact)
           .HasForeignKey(b => b.contactCodeSecond);

            modelBuilder.Entity<DashBoardChart>(e =>
            {
                e.HasNoKey();
            });
            modelBuilder.Entity<RecordStudentReport>(e =>
            {
                e.HasNoKey();
            });
            modelBuilder.Entity<RecordAuthPeople>(e =>
            {
                e.HasNoKey();
            });
            modelBuilder.Entity<RolePermission>(e =>
            {
                e.HasNoKey();
            });
            #endregion
            #region V2
            modelBuilder.Entity<StudentDetails>(e =>
            {
                e.HasNoKey();
            });
           
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("ConnStrDBPortalMatriculas"));
        }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }


        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.Now;
                var createdAtProperty = entity.Property("CreatedAt");
                var updatedAtProperty = entity.Property("UpdatedAt");
                if (entity.State == EntityState.Added && createdAtProperty != null && createdAtProperty.CurrentValue == null)
                {
                    createdAtProperty.CurrentValue = now;
                }
                if (entity.State == EntityState.Modified && createdAtProperty != null && createdAtProperty.CurrentValue == null)
                {
                    createdAtProperty.IsModified = false;
                }
                if (updatedAtProperty != null)
                {
                    updatedAtProperty.CurrentValue = now;
                }
            }
        }
        #region VERSION 2
        public DbSet<Student> Student { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<StudentFamilies> StudentFamilies { get; set; }
        public DbSet<Inscription> Inscription { get; set; }
        public DbSet<PaymentInformation> PaymentInformation { get; set; }
        public DbSet<StudentDetails> StudentDetails { get; set; }
        #endregion
        #region VERSION 1
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ContactPortal> Contacts { get; set; }
        public DbSet<StudentPortal> Students { get; set; }
        public DbSet<Application> Companies { get; set; }
        public DbSet<FileDownload> Files { get; set; }
        public DbSet<AuthorizePeople> People { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<LevelPortal> Levels { get; set; }
        public DbSet<PortalSchoolYear> SchoolYear { get; set; }
        public DbSet<CourseGradePortal> CourseGrades { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<PortalRequest> PortalRequests { get; set; }
        public DbSet<UserNotification> UserNotification { get; set; }
        public DbSet<DashBoardChart> DashBoardChart { get; set; }

        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<FinanceInformation> FinanceInformation { get; set; }
        public DbSet<DebitType> DebitType { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<RelationShip> RelationsShip { get; set; }
        public DbSet<CreditCard> CreditCard { get; set; }
        public DbSet<CivilStatus> CivilStatus { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        #endregion

        #region ELECTIONS
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateElection> CandidateElections { get; set; }
        public DbSet<CandidateOrganization> CandidateOrganizations { get; set; }
        public DbSet<OrganizationElection> OrganizationElections { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<OrganizationVote> OrganizationVotes { get; set; }
        public DbSet<Padron> Padron { get; set; }
        #endregion
        // report 
        public DbSet<RecordStudentReport> RecordStudentReport { get; set; }
        public DbSet<RecordAuthPeople> RecordAuthPeople { get; set; }

    }
    internal static class SoftDeleteModelBuilderExtensions
    {
        public static ModelBuilder ApplySoftDeleteQueryFilter(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    continue;
                }

                var param = Expression.Parameter(entityType.ClrType, "entity");
                var prop = Expression.PropertyOrField(param, nameof(ISoftDelete.DeletedAt));
                var entityNotDeleted = Expression.Lambda(Expression.Equal(prop, Expression.Constant(null)), param);

                entityType.SetQueryFilter(entityNotDeleted);
            }

            return modelBuilder;
        }
    }
}
