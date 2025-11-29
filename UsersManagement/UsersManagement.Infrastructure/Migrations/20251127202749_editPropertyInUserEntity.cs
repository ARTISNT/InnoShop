using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editPropertyInUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Users",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "IsDeleted");
        }
    }
}
