using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 1, "Тарас Шевченко" },
                    { 2, "Леся Українка" },
                    { 3, "Іван Франко" },
                    { 4, "Михайло Коцюбинський" },
                    { 5, "Ольга Кобилянська" },
                    { 6, "Джордж Орвелл" },
                    { 7, "Джейн Остін" },
                    { 8, "Ернест Гемінґвей" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Title", "Year" },
                values: new object[,]
                {
                    { 1, 1, "Кобзар", 1840 },
                    { 2, 1, "Гайдамаки", 1841 },
                    { 3, 1, "Катерина", 1838 },
                    { 4, 2, "Лісова пісня", 1911 },
                    { 5, 2, "Бояриня", 1910 },
                    { 6, 2, "Кассандра", 1908 },
                    { 7, 3, "Захар Беркут", 1883 },
                    { 8, 3, "Мойсей", 1905 },
                    { 9, 3, "Перехресні стежки", 1900 },
                    { 10, 4, "Тіні забутих предків", 1911 },
                    { 11, 4, "Fata morgana", 1903 },
                    { 12, 5, "Земля", 1902 },
                    { 13, 6, "1984", 1949 },
                    { 14, 6, "Колгосп тварин", 1945 },
                    { 15, 7, "Гордість і упередження", 1813 },
                    { 16, 7, "Розум і почуття", 1811 },
                    { 17, 8, "Старий і море", 1952 },
                    { 18, 8, "Прощавай, зброє", 1929 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
