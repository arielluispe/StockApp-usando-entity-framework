using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent api

            #region tables

            modelBuilder.Entity<Product>()
                .ToTable("Products");
            modelBuilder.Entity<Category>().
                ToTable("Categories");

            #endregion

            #region "Primary Keys"
            modelBuilder.Entity<Product>()
                .HasKey(product => product.Id);
            modelBuilder.Entity<Category>()
                .HasKey(CategorY => CategorY.Id);
            #endregion

            #region "Relationships"
            modelBuilder.Entity<Category>()
                .HasMany<Product>(Category => Category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "Property Configurations"

            #region "products"
            modelBuilder.Entity<Product>()
                .Property(Product => Product.Name)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(Product => Product.Price)
                .IsRequired();
            #endregion

            #region "categories"
            modelBuilder.Entity<Product>()
                .Property(Category => Category.Name)
                .IsRequired()
                .HasMaxLength(100);
            #endregion

            #endregion
        }
    }
}
