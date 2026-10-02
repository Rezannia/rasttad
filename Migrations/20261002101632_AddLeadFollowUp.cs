using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasttad.Migrations
{
    /// <inheritdoc />
    public partial class AddLeadFollowUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFollowedUp",
                table: "Leads",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Leads",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFollowedUp",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Leads");
        }
    }
}
