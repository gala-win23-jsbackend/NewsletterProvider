using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Circle1 = table.Column<bool>(type: "bit", nullable: false),
                    Circle2 = table.Column<bool>(type: "bit", nullable: false),
                    Circle3 = table.Column<bool>(type: "bit", nullable: false),
                    Circle4 = table.Column<bool>(type: "bit", nullable: false),
                    Circle5 = table.Column<bool>(type: "bit", nullable: false),
                    Circle6 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}
