using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace salvia.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUserPropAllowLoadFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAllowedToLoadFiles",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllowedToLoadFiles",
                table: "Users");
        }
    }
}
