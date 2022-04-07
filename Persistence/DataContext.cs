using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new AddressConfiguration().Configure(builder.Entity<Address>());
            new CompanyConfiguration().Configure(builder.Entity<Company>());
            new EventConfiguration().Configure(builder.Entity<Event>());
            new EventParticipantConfiguration().Configure(builder.Entity<EventParticipant>());
            new PersonConfiguration().Configure(builder.Entity<Person>());
            new ParticipantConfiguration().Configure(builder.Entity<Participant>());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override int SaveChanges()
        {
            AddTimestamps();

            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified)))
            {
                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).Created = DateTime.UtcNow;
                }

                ((Entity)entity.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
