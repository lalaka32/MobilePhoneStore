using Microsoft.EntityFrameworkCore;
using Common.Entities;

namespace DBServices
{
	public class StoreContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<MobilePhone> MobilePhones { get; set; }
		public StoreContext(DbContextOptions<StoreContext> options): base(options)
		{
			Database.EnsureCreated();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserPhone>()
				.HasKey(t => new { t.UserId, t.PhoneId });

			modelBuilder.Entity<UserPhone>()
				.HasOne(sc => sc.User)
				.WithMany(s => s.UserPhones)
				.HasForeignKey(sc => sc.UserId);

			modelBuilder.Entity<UserPhone>()
				.HasOne(sc => sc.Phone)
				.WithMany(c => c.UserPhones)
				.HasForeignKey(sc => sc.PhoneId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
