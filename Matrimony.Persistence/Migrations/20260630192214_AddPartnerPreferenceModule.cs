using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matrimony.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPartnerPreferenceModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_UserId",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "Caste",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "PartnerPreferences");

            migrationBuilder.RenameColumn(
                name: "MinHeight",
                table: "PartnerPreferences",
                newName: "MinHeightInches");

            migrationBuilder.RenameColumn(
                name: "MaxHeight",
                table: "PartnerPreferences",
                newName: "MinHeightFeet");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "UserProfiles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptDivorced",
                table: "PartnerPreferences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptWidowed",
                table: "PartnerPreferences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptWithChildren",
                table: "PartnerPreferences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CasteId",
                table: "PartnerPreferences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "PartnerPreferences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "PartnerPreferences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationId",
                table: "PartnerPreferences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxAnnualIncome",
                table: "PartnerPreferences",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxHeightFeet",
                table: "PartnerPreferences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxHeightInches",
                table: "PartnerPreferences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MinAnnualIncome",
                table: "PartnerPreferences",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MotherTongueId",
                table: "PartnerPreferences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OccupationId",
                table: "PartnerPreferences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReligionId",
                table: "PartnerPreferences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "PartnerPreferences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ProfileId",
                table: "UserProfiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_CasteId",
                table: "PartnerPreferences",
                column: "CasteId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_CityId",
                table: "PartnerPreferences",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_CountryId",
                table: "PartnerPreferences",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_EducationId",
                table: "PartnerPreferences",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_MotherTongueId",
                table: "PartnerPreferences",
                column: "MotherTongueId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_OccupationId",
                table: "PartnerPreferences",
                column: "OccupationId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_ReligionId",
                table: "PartnerPreferences",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_StateId",
                table: "PartnerPreferences",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_UserId",
                table: "PartnerPreferences",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerPreferences_CasteMasters_CasteId",
                table: "PartnerPreferences",
                column: "CasteId",
                principalTable: "CasteMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerPreferences_CityMasters_CityId",
                table: "PartnerPreferences",
                column: "CityId",
                principalTable: "CityMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerPreferences_CountryMasters_CountryId",
                table: "PartnerPreferences",
                column: "CountryId",
                principalTable: "CountryMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerPreferences_EducationMasters_EducationId",
                table: "PartnerPreferences",
                column: "EducationId",
                principalTable: "EducationMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerPreferences_MotherTongueMasters_MotherTongueId",
                table: "PartnerPreferences",
                column: "MotherTongueId",
                principalTable: "MotherTongueMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerPreferences_OccupationMasters_OccupationId",
                table: "PartnerPreferences",
                column: "OccupationId",
                principalTable: "OccupationMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerPreferences_ReligionMasters_ReligionId",
                table: "PartnerPreferences",
                column: "ReligionId",
                principalTable: "ReligionMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerPreferences_StateMasters_StateId",
                table: "PartnerPreferences",
                column: "StateId",
                principalTable: "StateMasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartnerPreferences_CasteMasters_CasteId",
                table: "PartnerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerPreferences_CityMasters_CityId",
                table: "PartnerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerPreferences_CountryMasters_CountryId",
                table: "PartnerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerPreferences_EducationMasters_EducationId",
                table: "PartnerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerPreferences_MotherTongueMasters_MotherTongueId",
                table: "PartnerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerPreferences_OccupationMasters_OccupationId",
                table: "PartnerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerPreferences_ReligionMasters_ReligionId",
                table: "PartnerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerPreferences_StateMasters_StateId",
                table: "PartnerPreferences");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_ProfileId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_CasteId",
                table: "PartnerPreferences");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_CityId",
                table: "PartnerPreferences");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_CountryId",
                table: "PartnerPreferences");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_EducationId",
                table: "PartnerPreferences");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_MotherTongueId",
                table: "PartnerPreferences");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_OccupationId",
                table: "PartnerPreferences");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_ReligionId",
                table: "PartnerPreferences");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_StateId",
                table: "PartnerPreferences");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPreferences_UserId",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "AcceptDivorced",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "AcceptWidowed",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "AcceptWithChildren",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "CasteId",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "MaxAnnualIncome",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "MaxHeightFeet",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "MaxHeightInches",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "MinAnnualIncome",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "MotherTongueId",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "OccupationId",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "ReligionId",
                table: "PartnerPreferences");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "PartnerPreferences");

            migrationBuilder.RenameColumn(
                name: "MinHeightInches",
                table: "PartnerPreferences",
                newName: "MinHeight");

            migrationBuilder.RenameColumn(
                name: "MinHeightFeet",
                table: "PartnerPreferences",
                newName: "MaxHeight");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Caste",
                table: "PartnerPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "PartnerPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "PartnerPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "PartnerPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "PartnerPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPreferences_UserId",
                table: "PartnerPreferences",
                column: "UserId");
        }
    }
}
