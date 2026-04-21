using CapstoneProject.Areas.Identity.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Data;

public class CapstoneProjectDbContext : IdentityDbContext<CapstoneProjectUser>
{
    public CapstoneProjectDbContext(DbContextOptions<CapstoneProjectDbContext> options)
        : base(options)
    {
    }

    public DbSet<Purchasing> Purchases { get; set; }
    public DbSet<StaffRegister> StaffRegisters { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Liability> LiabilityForms { get; set; }
    public DbSet<ResourceRequest> ResourceRequests { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionLineItem> TransactionLineItems { get; set; }
    //public DbSet<UserApplication> UserApplications { get; set; }
    public DbSet<UserSurvey> UserSurveys { get; set; }
    public DbSet<ItemCategory> ItemCategories { get; set; }
    public DbSet<ItemSubcategory> ItemSubcategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // configure Quantity property in the Item entity
        modelBuilder.Entity<Item>()
            .Property(i => i.Quantity)
            .HasColumnType("decimal(6, 2)");
        modelBuilder.Entity<CapstoneProjectUser>()
            .HasIndex(x=>x.StudentId).IsUnique();

        //SEED DEFAULT DATA FOR CATEGORIES  !!RUN "Update-Database" IN PACKAGE MANAGER CONSOLE TO ADD!!
        modelBuilder.Entity<ItemCategory>().HasData(
            new ItemCategory { CategoryID = 1, Name = "Dairy" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 1, CategoryID = 1, Name = "Milk" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 2, CategoryID = 1, Name = "Cheese" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 3, CategoryID = 1, Name = "Yogurt" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 4, CategoryID = 1, Name = "Butter" }
        );

        modelBuilder.Entity<ItemCategory>().HasData(
            new ItemCategory { CategoryID = 2, Name = "Produce" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 1, CategoryID = 2, Name = "Fruit" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 2, CategoryID = 2, Name = "Vegetable" }
        );


        modelBuilder.Entity<ItemCategory>().HasData(
            new ItemCategory { CategoryID = 3, Name = "Canned Products" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 1, CategoryID = 3, Name = "Canned Meat" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 2, CategoryID = 3, Name = "Canned Vegetable" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 3, CategoryID = 3, Name = "Other Can Food" }
        );

        modelBuilder.Entity<ItemCategory>().HasData(
            new ItemCategory { CategoryID = 4, Name = "Grains/Pasta" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 1, CategoryID = 4, Name = "Bread" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 2, CategoryID = 4, Name = "Box Pasta" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 3, CategoryID = 4, Name = "Rice" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 4, CategoryID = 4, Name = "Other Grains" }
        );

        modelBuilder.Entity<ItemCategory>().HasData(
        new ItemCategory { CategoryID = 5, Name = "Laundry" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 1, CategoryID = 5, Name = "Liquid Detergent" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 2, CategoryID = 5, Name = "Dry Detergent" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 3, CategoryID = 5, Name = "Bleach" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 4, CategoryID = 5, Name = "Other Laundry" }
        );

        modelBuilder.Entity<ItemCategory>().HasData(
        new ItemCategory { CategoryID = 6, Name = "Personal Care" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 1, CategoryID = 6, Name = "Body Soap" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 2, CategoryID = 6, Name = "Shampoo" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 3, CategoryID = 6, Name = "Conditioner" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 4, CategoryID = 6, Name = "Toothpaste" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 5, CategoryID = 6, Name = "Hand Soap" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 6, CategoryID = 6, Name = "Deodorant" }
        );

        modelBuilder.Entity<ItemCategory>().HasData(
        new ItemCategory { CategoryID = 7, Name = "Baby Care" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 1, CategoryID = 6, Name = "Diapers" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 2, CategoryID = 6, Name = "Wipes" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 3, CategoryID = 6, Name = "Lotion" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 4, CategoryID = 6, Name = "Shampoo" }
        );

        modelBuilder.Entity<ItemSubcategory>().HasData(
            new ItemSubcategory { SubcategoryID = 5, CategoryID = 6, Name = "Powder" }
        );











    }


}
