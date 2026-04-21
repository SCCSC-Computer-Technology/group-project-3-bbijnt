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

        //SEED DEFAULT DATA FOR CATEGORIES
        //!!RUN "Update-Database" IN PACKAGE MANAGER CONSOLE TO ADD!!
        modelBuilder.Entity<ItemCategory>().HasData(
            new ItemCategory { CategoryID = 1, Name = "Dairy" },
            new ItemCategory { CategoryID = 2, Name = "Produce" },
            new ItemCategory { CategoryID = 3, Name = "Canned Products" },
            new ItemCategory { CategoryID = 4, Name = "Grains/Pasta" },
            new ItemCategory { CategoryID = 5, Name = "Laundry" },
            new ItemCategory { CategoryID = 6, Name = "Personal Care" },
            new ItemCategory { CategoryID = 7, Name = "Baby Care" }
        );

        // seed default sub categorys
        modelBuilder.Entity<ItemSubcategory>().HasData(
            // dairy sub Cs
            new ItemSubcategory { SubcategoryID = 1, CategoryID = 1, Name = "Milk" },
            new ItemSubcategory { SubcategoryID = 2, CategoryID = 1, Name = "Cheese" },
            new ItemSubcategory { SubcategoryID = 3, CategoryID = 1, Name = "Yogurt" },
            new ItemSubcategory { SubcategoryID = 4, CategoryID = 1, Name = "Butter" },

            // produce sub Cs
            new ItemSubcategory { SubcategoryID = 5, CategoryID = 2, Name = "Fruit" },
            new ItemSubcategory { SubcategoryID = 6, CategoryID = 2, Name = "Vegetable" },

            // canned products sub Cs
            new ItemSubcategory { SubcategoryID = 7, CategoryID = 3, Name = "Canned Meat" },
            new ItemSubcategory { SubcategoryID = 8, CategoryID = 3, Name = "Canned Vegetable" },
            new ItemSubcategory { SubcategoryID = 9, CategoryID = 3, Name = "Other Can Food" },

            // grains/pasta sub Cs
            new ItemSubcategory { SubcategoryID = 10, CategoryID = 4, Name = "Bread" },
            new ItemSubcategory { SubcategoryID = 11, CategoryID = 4, Name = "Box Pasta" },
            new ItemSubcategory { SubcategoryID = 12, CategoryID = 4, Name = "Rice" },
            new ItemSubcategory { SubcategoryID = 13, CategoryID = 4, Name = "Other Grains" },

            //laundry sub Cs
            new ItemSubcategory { SubcategoryID = 14, CategoryID = 5, Name = "Liquid Detergent" },
            new ItemSubcategory { SubcategoryID = 15, CategoryID = 5, Name = "Dry Detergent" },
            new ItemSubcategory { SubcategoryID = 16, CategoryID = 5, Name = "Bleach" },
            new ItemSubcategory { SubcategoryID = 17, CategoryID = 5, Name = "Other Laundry" },

            //personal care sub Cs
            new ItemSubcategory { SubcategoryID = 18, CategoryID = 6, Name = "Body Soap" },
            new ItemSubcategory { SubcategoryID = 19, CategoryID = 6, Name = "Shampoo" },
            new ItemSubcategory { SubcategoryID = 20, CategoryID = 6, Name = "Conditioner" },
            new ItemSubcategory { SubcategoryID = 21, CategoryID = 6, Name = "Toothpaste" },
            new ItemSubcategory { SubcategoryID = 22, CategoryID = 6, Name = "Hand Soap" },
            new ItemSubcategory { SubcategoryID = 23, CategoryID = 6, Name = "Deodorant" },

            //baby care sub Cs
            new ItemSubcategory { SubcategoryID = 24, CategoryID = 7, Name = "Diapers" },
            new ItemSubcategory { SubcategoryID = 25, CategoryID = 7, Name = "Wipes" },
            new ItemSubcategory { SubcategoryID = 26, CategoryID = 7, Name = "Lotion" },
            new ItemSubcategory { SubcategoryID = 27, CategoryID = 7, Name = "Shampoo" },
            new ItemSubcategory { SubcategoryID = 28, CategoryID = 7, Name = "Powder" }
        );

        //default data for items
        modelBuilder.Entity<Item>().HasData( //created UUID with Guid.NewGuid().ToString() the UUID will be used to create barcodes
            new Item { ItemID = 1, SubcategoryID = 1, Type = "Gallon-Whole", UUID = "62446e0b-b4a2-4430-93e9-edcee1007375", Description = "Gallon of Whole milk", Quantity = 1, PointCost = 5 },
            new Item { ItemID = 2, SubcategoryID = 2, Type = "Cheddar", UUID = "532026ed-b264-44e7-81a6-abd2007ffeaf", Description = "Aged cheddar cheese", Quantity = 2.5m, PointCost = 7 },
            new Item { ItemID = 3, SubcategoryID = 3, Type = "Greek", UUID = "fcfe3fd1-d10e-4b0f-8d98-ddf2fdfa6663", Description = "Greek yogurt", Quantity = 1, PointCost = 3 },
            new Item { ItemID = 4, SubcategoryID = 5, Type = "Apple", UUID = "e94a0402-f2f9-40e2-b423-7b8e4c65be5d", Description = "Fresh red apples", Quantity = 3, PointCost = 2 },
            new Item { ItemID = 5, SubcategoryID = 6, Type = "Carrot", UUID = "97255123-ddc5-4cc4-8a3a-aa3a65809356", Description = "Carrots", Quantity = 5.0m, PointCost = 1 },
            new Item { ItemID = 6, SubcategoryID = 7, Type = "Spam", UUID = "4622ef7d-72fe-4db8-8182-056d02da6cdf", Description = "Canned Spam", Quantity = 12.0m, PointCost = 6 },
            new Item { ItemID = 7, SubcategoryID = 9, Type = "Tomato Soup", UUID = "a89ea68b-4b7a-4651-a150-ac534ddc81ae", Description = "Canned tomato soup", Quantity = 8.0m, PointCost = 3 },
            new Item { ItemID = 8, SubcategoryID = 11, Type = "Spaghetti", UUID = "04368fd1-c129-4e1c-98c2-c4d058d713f9", Description = "Box of spaghetti pasta", Quantity = 1.0m, PointCost = 2 },
            new Item { ItemID = 9, SubcategoryID = 12, Type = "Basmati", UUID = "7b5caca6-d148-4966-8163-2211c9545f1a", Description = "Premium basmati rice", Quantity = 2.0m, PointCost = 5 }
        );








    }


}
