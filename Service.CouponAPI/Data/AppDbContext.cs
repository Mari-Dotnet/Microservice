using Microsoft.EntityFrameworkCore;
using Service.CouponAPI.Models;

namespace Service.CouponAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> Options) :base(Options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon() { CouponId=1,CouponCode="10OFF" ,DiscountAmount=10,MinAmount=20},
                new Coupon() { CouponId=2,CouponCode="20OFF" ,DiscountAmount=20,MinAmount=40},
                new Coupon() { CouponId=3,CouponCode="30OFF" ,DiscountAmount=30,MinAmount=60},
                new Coupon() { CouponId=4,CouponCode="40OFF" ,DiscountAmount=40,MinAmount=80},
                new Coupon() { CouponId=5,CouponCode="50OFF" ,DiscountAmount=50,MinAmount=100}
                );
        }
    }
}
