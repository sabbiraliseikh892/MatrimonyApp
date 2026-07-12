using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matrimony.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfileViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileViews_AspNetUsers_ViewedUserId",
                        column: x => x.ViewedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfileViews_AspNetUsers_ViewerUserId",
                        column: x => x.ViewerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileViews_ViewedAt",
                table: "UserProfileViews",
                column: "ViewedAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileViews_ViewedUserId",
                table: "UserProfileViews",
                column: "ViewedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileViews_ViewerUserId",
                table: "UserProfileViews",
                column: "ViewerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileViews_ViewerUserId_ViewedUserId",
                table: "UserProfileViews",
                columns: new[] { "ViewerUserId", "ViewedUserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfileViews");
        }
    }
}
