using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OilShop.Entities
{
    public class ApplicationDbContext : IdentityDbContext<DbUser, DbRole, long, IdentityUserClaim<long>,
                                            DbUserRole, IdentityUserLogin<long>,
                                            IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Oil> Oils { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OilType> OilTypes { get; set; }
        public DbSet<OilManafacturer> OilManafacturers { get; set; }
        public DbSet<OilApplying> OilApplyings { get; set; }
        public DbSet<OilCapacity> OilCapacities { get; set; }
        public DbSet<OilSpecification> OilSpecifications { get; set; }
        public DbSet<SpecificationOil> SpecificationOils { get; set; }
        public DbSet<OilTolerance> OilTolerances { get; set; }
        public DbSet<ToleranceOil> ToleranceOils { get; set; }
        public DbSet<OilRecommendation> OilRecommendations { get; set; }
        public DbSet<RecommendationOil> RecommendationOils { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<RecommendationOil>().HasKey(sc => new { sc.OilId, sc.RecommendationId });
            builder.Entity<SpecificationOil>().HasKey(sc => new { sc.OilId, sc.SpecificationId });
            builder.Entity<ToleranceOil>().HasKey(sc => new { sc.OilId, sc.ToleranceId });
        }
    }
}
