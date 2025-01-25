using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Category", "IsAvailable", "Title" },
                values: new object[,]
                {
                    { 1, "J. K. Rowling", "Fantasy", true, "Philosopher's Stone" },
                    { 2, "J. K. Rowling", "Fantasy", true, "Chamber of Secrets" },
                    { 3, "J. K. Rowling", "Fantasy", true, "Prisoner of Azkaban" },
                    { 4, "J. K. Rowling", "Fantasy", true, "Goblet of Fire" },
                    { 5, "J. R. R. Tolkien", "Fantasy", true, "The Fellowship of the Ring" },
                    { 6, "J. R. R. Tolkien", "Fantasy", true, "The Two Towers" },
                    { 7, "J. R. R. Tolkien", "Fantasy", true, "The Return of the King" },
                    { 8, "George Orwell", "Dystopian", true, "1984" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
