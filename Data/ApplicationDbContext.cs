using IdentityServer4.EntityFramework.Options;
using jdobson_pairs.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace jdobson_pairs.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<Models.ApplicationUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<GameCard> GameCards { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasData(new Card { Id = System.Guid.NewGuid(), Name = "Beth", ImagePath = "/assets/images/cards/beth.png" });
            modelBuilder.Entity<Card>().HasData(new Card { Id = System.Guid.NewGuid(), Name = "Bird Person", ImagePath = "/assets/images/cards/birdperson.png" });
            modelBuilder.Entity<Card>().HasData(new Card { Id = System.Guid.NewGuid(), Name = "Jerry", ImagePath = "/assets/images/cards/jerry.png" });
            modelBuilder.Entity<Card>().HasData(new Card { Id = System.Guid.NewGuid(), Name = "Morty", ImagePath = "/assets/images/cards/morty.png" });
            modelBuilder.Entity<Card>().HasData(new Card { Id = System.Guid.NewGuid(), Name = "Mr. Goldenfold", ImagePath = "/assets/images/cards/mrgoldenfold.png" });
            modelBuilder.Entity<Card>().HasData(new Card { Id = System.Guid.NewGuid(), Name = "Mr. Meeseeks", ImagePath = "/assets/images/cards/mrmeeseeks.png" });
            modelBuilder.Entity<Card>().HasData(new Card { Id = System.Guid.NewGuid(), Name = "Mr. PB", ImagePath = "/assets/images/cards/mrpb.png" });
            modelBuilder.Entity<Card>().HasData(new Card { Id = System.Guid.NewGuid(), Name = "Squanchy", ImagePath = "/assets/images/cards/squanchy.png" });
            modelBuilder.Entity<Card>().HasData(new Card { Id = System.Guid.NewGuid(), Name = "Summer", ImagePath = "/assets/images/cards/summer.png" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
