using Microsoft.EntityFrameworkCore;
using ConsumerApi.Models;

namespace ConsumerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Consumer> Consumers { get; set; }
        public virtual DbSet<Bussiness> Bussinesses { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<QuotesMaster> Quotes { get; set; }
        public virtual DbSet<ConsumerPolicy> ConsumerPolicies { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consumer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.LastName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(255);
                entity.Property(c => c.Pan).IsRequired().HasMaxLength(20);
                entity.Property(c => c.Dob).IsRequired();
                entity.Property(c => c.BusinessName).HasMaxLength(255);
                entity.Property(c => c.Validity).IsRequired();
                entity.Property(c => c.AgentName).HasMaxLength(100);
                entity.Property(c => c.AgentId).IsRequired();
            });

            modelBuilder.Entity<Bussiness>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.BusinessName).IsRequired().HasMaxLength(255);
                entity.Property(b => b.BusinessType).IsRequired().HasMaxLength(100);
                entity.Property(b => b.BusinessAge).IsRequired();
                entity.Property(b => b.TotalEmployees).IsRequired();
                entity.Property(b => b.CapitalInvested).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(b => b.BusinessTurnover).IsRequired().HasColumnType("decimal(18,2)");

                entity.HasOne<Consumer>()
                    .WithMany()
                    .HasForeignKey(b => b.ConsumerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.BuildingSqFt).IsRequired();
                entity.Property(p => p.BuildingType).IsRequired().HasMaxLength(100);
                entity.Property(p => p.BuildingStoreys).IsRequired();
                entity.Property(p => p.BuildingAge).IsRequired();
                entity.Property(p => p.PropertyValue).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(p => p.CostOfTheAsset).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(p => p.SalvageValue).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(p => p.UsefulLifeOfAsset).IsRequired();

                entity.HasOne<Bussiness>()
                    .WithMany()
                    .HasForeignKey(p => p.BusinessId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Consumer>()
                    .WithMany()
                    .HasForeignKey(p => p.ConsumerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ConsumerPolicy>(entity =>
            {
                entity.HasKey(cp => cp.PolicyId);
                entity.Property(cp => cp.PolicyId)
                    .HasDefaultValueSql("NEWID()"); 

                entity.Property(cp => cp.AcceptanceStatus).HasMaxLength(100);
                entity.Property(cp => cp.AcceptedQuote).HasMaxLength(255);
                entity.Property(cp => cp.CoveredSum).HasMaxLength(100);
                entity.Property(cp => cp.Duration).HasMaxLength(100);
                entity.Property(cp => cp.EffectiveDate).HasMaxLength(50);
                entity.Property(cp => cp.PaymentDetails).HasMaxLength(255);
                entity.Property(cp => cp.PolicyStatus).HasMaxLength(100);
                entity.Property(cp => cp.PropertyType).HasMaxLength(100);
                entity.Property(cp => cp.ConsumerType).HasMaxLength(100);
                entity.Property(cp => cp.AssuredSum).HasMaxLength(100);
                entity.Property(cp => cp.Tenure).HasMaxLength(100);
                entity.Property(cp => cp.BaseLocation).HasMaxLength(100);
                entity.Property(cp => cp.Type).HasMaxLength(100);
            });
        }
    }
}
