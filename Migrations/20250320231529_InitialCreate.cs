using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wikiAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WikiCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WikiEntries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WikiCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiEntries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WikiEntries_WikiCategories_WikiCategoryID",
                        column: x => x.WikiCategoryID,
                        principalTable: "WikiCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WikiSection",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bodies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WikiEntryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiSection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WikiSection_WikiEntries_WikiEntryID",
                        column: x => x.WikiEntryID,
                        principalTable: "WikiEntries",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "WikiStat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Val = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WikiEntryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiStat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WikiStat_WikiEntries_WikiEntryID",
                        column: x => x.WikiEntryID,
                        principalTable: "WikiEntries",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WikiEntries_WikiCategoryID",
                table: "WikiEntries",
                column: "WikiCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_WikiSection_WikiEntryID",
                table: "WikiSection",
                column: "WikiEntryID");

            migrationBuilder.CreateIndex(
                name: "IX_WikiStat_WikiEntryID",
                table: "WikiStat",
                column: "WikiEntryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WikiSection");

            migrationBuilder.DropTable(
                name: "WikiStat");

            migrationBuilder.DropTable(
                name: "WikiEntries");

            migrationBuilder.DropTable(
                name: "WikiCategories");
        }
    }
}
