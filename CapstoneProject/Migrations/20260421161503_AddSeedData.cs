using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CapstoneProject.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { 1, "Dairy" },
                    { 2, "Produce" },
                    { 3, "Canned Products" },
                    { 4, "Grains/Pasta" },
                    { 5, "Laundry" },
                    { 6, "Personal Care" },
                    { 7, "Baby Care" }
                });

            migrationBuilder.InsertData(
                table: "ItemSubcategories",
                columns: new[] { "SubcategoryID", "CategoryID", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Milk" },
                    { 2, 1, "Cheese" },
                    { 3, 1, "Yogurt" },
                    { 4, 1, "Butter" },
                    { 5, 2, "Fruit" },
                    { 6, 2, "Vegetable" },
                    { 7, 3, "Canned Meat" },
                    { 8, 3, "Canned Vegetable" },
                    { 9, 3, "Other Can Food" },
                    { 10, 4, "Bread" },
                    { 11, 4, "Box Pasta" },
                    { 12, 4, "Rice" },
                    { 13, 4, "Other Grains" },
                    { 14, 5, "Liquid Detergent" },
                    { 15, 5, "Dry Detergent" },
                    { 16, 5, "Bleach" },
                    { 17, 5, "Other Laundry" },
                    { 18, 6, "Body Soap" },
                    { 19, 6, "Shampoo" },
                    { 20, 6, "Conditioner" },
                    { 21, 6, "Toothpaste" },
                    { 22, 6, "Hand Soap" },
                    { 23, 6, "Deodorant" },
                    { 24, 7, "Diapers" },
                    { 25, 7, "Wipes" },
                    { 26, 7, "Lotion" },
                    { 27, 7, "Shampoo" },
                    { 28, 7, "Powder" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "Description", "PointCost", "Quantity", "SubcategoryID", "Type", "UUID" },
                values: new object[,]
                {
                    { 1, "Gallon of Whole milk", 5, 1m, 1, "Gallon-Whole", "62446e0b-b4a2-4430-93e9-edcee1007375" },
                    { 2, "Aged cheddar cheese", 7, 2.5m, 2, "Cheddar", "532026ed-b264-44e7-81a6-abd2007ffeaf" },
                    { 3, "Greek yogurt", 3, 1m, 3, "Greek", "fcfe3fd1-d10e-4b0f-8d98-ddf2fdfa6663" },
                    { 4, "Fresh red apples", 2, 3m, 5, "Apple", "e94a0402-f2f9-40e2-b423-7b8e4c65be5d" },
                    { 5, "Carrots", 1, 5.0m, 6, "Carrot", "97255123-ddc5-4cc4-8a3a-aa3a65809356" },
                    { 6, "Canned Spam", 6, 12.0m, 7, "Spam", "4622ef7d-72fe-4db8-8182-056d02da6cdf" },
                    { 7, "Canned tomato soup", 3, 8.0m, 9, "Tomato Soup", "a89ea68b-4b7a-4651-a150-ac534ddc81ae" },
                    { 8, "Box of spaghetti pasta", 2, 1.0m, 11, "Spaghetti", "04368fd1-c129-4e1c-98c2-c4d058d713f9" },
                    { 9, "Premium basmati rice", 5, 2.0m, 12, "Basmati", "7b5caca6-d148-4966-8163-2211c9545f1a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "CategoryID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "CategoryID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ItemSubcategories",
                keyColumn: "SubcategoryID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "CategoryID",
                keyValue: 4);
        }
    }
}
