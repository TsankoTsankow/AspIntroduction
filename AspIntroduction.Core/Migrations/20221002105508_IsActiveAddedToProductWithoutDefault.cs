using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspIntroduction.Core.Migrations
{
    public partial class IsActiveAddedToProductWithoutDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "products",
                type: "bit",
                nullable: false,
                comment: "Product is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true,
                oldComment: "Product is active or not");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: true,
                comment: "Product is active or not",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Product is active or not");
        }
    }
}
