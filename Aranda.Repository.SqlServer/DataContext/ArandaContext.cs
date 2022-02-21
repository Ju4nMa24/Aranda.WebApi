using Aranda.Common.Types.Categories;
using Aranda.Common.Types.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Repository.SqlServer.DataContext
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class ArandaContext : DbContext
    {
        public ArandaContext(DbContextOptions<ArandaContext> options) : base(options) { }
        /// <summary>
        /// Database tables.
        /// </summary>
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Category>  Categories { get; set; }
        /// <summary>
        /// Create the database model.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Product settings
            modelBuilder.Entity<Product>().HasKey(o => o.ProductId);
            modelBuilder.Entity<Product>().Property(o => o.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Product>().Property(o => o.BriefDescription).HasMaxLength(4096).IsRequired();
            modelBuilder.Entity<Product>().Property(o => o.CreationDate).HasDefaultValue(DateTime.UtcNow).IsRequired();
            //Foreign Key
            modelBuilder.Entity<Product>().HasOne<Category>().WithMany().HasForeignKey(p => p.CategoryId).IsRequired();
            //ProductImages settings
            modelBuilder.Entity<ProductImages>().HasKey(p => p.ProductImagesId);
            modelBuilder.Entity<ProductImages>().Property(p => p.ProductId).HasDefaultValue(Guid.NewGuid()).IsRequired();
            modelBuilder.Entity<ProductImages>().Property(p => p.ImageUrl).HasMaxLength(4096).IsRequired();
            modelBuilder.Entity<ProductImages>().Property(p => p.CreationDate).HasDefaultValue(DateTime.UtcNow).IsRequired();
            //Foreign Key
            modelBuilder.Entity<ProductImages>().HasOne<Product>().WithMany().HasForeignKey(p => p.ProductId).IsRequired();
            //Category Settngs
            modelBuilder.Entity<Category>().HasKey(p => p.CategoryId);
            modelBuilder.Entity<Category>().Property(p => p.CategoryName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Category>().Property(p => p.CreationDate).HasDefaultValue(DateTime.UtcNow).IsRequired();
            //Default Category
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Consolas",
                    CreationDate = DateTime.UtcNow
                });
        }
    }
}
