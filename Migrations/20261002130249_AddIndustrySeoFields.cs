using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasttad.Migrations
{
    /// <inheritdoc />
    public partial class AddIndustrySeoFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Industries",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Industries",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Industries",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Industries");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Industries");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Industries");
        }
    }
}
