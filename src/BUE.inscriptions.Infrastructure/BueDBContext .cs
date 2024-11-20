using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;


namespace BUE.Inscriptions.Infrastructure
{
    public class BueDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public BueDBContext(IConfiguration configuration) => this.Configuration = configuration;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentRepresentative>().HasOne<Contact>((Expression<Func<StudentRepresentative, Contact>>)(b => b.contact)).WithMany((Expression<Func<Contact, IEnumerable<StudentRepresentative>>>)(ba => ba.studentRepresentatives)).HasForeignKey((Expression<Func<StudentRepresentative, object>>)(bi => (object)bi.contactCode));
            modelBuilder.Entity<StudentRepresentative>().HasOne<StudentBUE>((Expression<Func<StudentRepresentative, StudentBUE>>)(b => b.student)).WithMany((Expression<Func<StudentBUE, IEnumerable<StudentRepresentative>>>)(ba => ba.studentRepresentatives)).HasForeignKey((Expression<Func<StudentRepresentative, object>>)(bi => (object)bi.studentCodeSchoolYear));
            modelBuilder.Entity<StudentBUE>()
            .HasOne(e => e.courseGrade)
            .WithMany(c => c.students);
            modelBuilder.Entity<StudentBUE>()
            .HasOne(e => e.level)
            .WithMany(c => c.students);

            modelBuilder.Entity<TransactionDetailStudent>(e =>
            {
                e.HasNoKey();
            }); 
            modelBuilder.Entity<TransactionStudent>(e =>
            {
                e.HasNoKey();
            });
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlServer(this.Configuration.GetConnectionString("ConnStrDBBueV2"));

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<StudentBUE> Students { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<CourseGrade> CourseGrades { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<ParallelClass> Parallels { get; set; }

        public DbSet<StudentRepresentative> studentRepresentatives { get; set; }

        public DbSet<Nationality> Nationalities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<Canton> Cantons { get; set; }

        public DbSet<Parish> Parishes { get; set; }

        public DbSet<RelationShip> RelationsShip { get; set; }

        public DbSet<Profession> Professions { get; set; }

        public DbSet<SchoolYear> SchoolYears { get; set; }

        // REPORTS
        public DbSet<TransactionDetailStudent> TransactionDetailStudent { get; set; }
        public DbSet<TransactionStudent> TransactionStudent { get; set; }
    }
}


