using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Datalayer.Entities.Permission;
using TachraFac.Datalayer.Entities.User;
using TachraFac.Datalayer.Entities.Wallet;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Datalayer.Entities.User;
using TachraFac.Datalayer.Entities.Product;



namespace TachraFac.Datalayer.Context
{
    public class TachraContext:DbContext
    {
        public TachraContext(DbContextOptions<TachraContext> options):base(options)
        {
            
        }

        #region User
        public DbSet<Role> tblRole { get; set; }
        public DbSet<User> tblUser { get; set; }
        public DbSet<UserRole> tblUserRole { get; set; }

        public DbSet<UserContact> tblUserContact { get; set; }


        #endregion

        #region Wallet
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        #endregion

        #region Permission 
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        #endregion

        #region Product
        public  DbSet<Photo> Photos { get; set; }
        public  DbSet<Product> Products { get; set; }
        public  DbSet<ProductMaterial> ProductMaterials { get; set; }
        public  DbSet<RawMaterial> RawMaterials { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserContact>().HasKey(uc => uc.UserId); // 🔥 کلید اصلی
            modelBuilder.Entity<UserContact>().HasOne(uc => uc.User).WithOne(u => u.UserContact).HasForeignKey<UserContact>(uc => uc.UserId);
            modelBuilder.Entity<WalletType>(entity =>
            {
                entity.HasKey(e => e.TypeId);
                entity.HasMany(wt => wt.Wallets)
                .WithOne(w => w.WalletType)
                .HasForeignKey(w => w.TypeId);
            });
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<ProductLike>(entity =>
            {
                entity.HasKey(pl => new { pl.ProductId, pl.UserId });

                entity.HasOne(pl => pl.Product)
                    .WithMany(p => p.ProductLikes)
                    .HasForeignKey(pl => pl.ProductId);

                entity.HasOne(pl => pl.User)
                    .WithMany(u => u.ProductLikes)
                    .HasForeignKey(pl => pl.UserId);
            });
        }      
    }
}
