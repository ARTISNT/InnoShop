using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPasswordTokenAndUserEntityCorections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResetPasswordTokens",
                table: "ResetPasswordTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ResetPasswordTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "EmailVerificationTokens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResetPasswordTokens",
                table: "ResetPasswordTokens",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordTokens_UserId",
                table: "ResetPasswordTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationTokens_UserEntityId",
                table: "EmailVerificationTokens",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailVerificationTokens_Users_UserEntityId",
                table: "EmailVerificationTokens",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailVerificationTokens_Users_UserEntityId",
                table: "EmailVerificationTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResetPasswordTokens",
                table: "ResetPasswordTokens");

            migrationBuilder.DropIndex(
                name: "IX_ResetPasswordTokens_UserId",
                table: "ResetPasswordTokens");

            migrationBuilder.DropIndex(
                name: "IX_EmailVerificationTokens_UserEntityId",
                table: "EmailVerificationTokens");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "EmailVerificationTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ResetPasswordTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResetPasswordTokens",
                table: "ResetPasswordTokens",
                column: "UserId");
        }
    }
}
