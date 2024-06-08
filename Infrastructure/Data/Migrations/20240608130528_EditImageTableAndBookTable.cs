using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Infrastructure
{
    /// <inheritdoc />
    public partial class EditImageTableAndBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Book_Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Book_Images_BookId",
                table: "Book_Images",
                column: "BookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Images_Books_BookId",
                table: "Book_Images",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Images_Books_BookId",
                table: "Book_Images");

            migrationBuilder.DropIndex(
                name: "IX_Book_Images_BookId",
                table: "Book_Images");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Book_Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
