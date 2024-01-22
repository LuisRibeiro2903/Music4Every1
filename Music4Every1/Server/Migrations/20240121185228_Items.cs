using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music4Every1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Items : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Saldo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leiloes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    CompradorId = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time", nullable: false),
                    PrecoInicial = table.Column<double>(type: "float", nullable: false),
                    PrecoCompraImediata = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leiloes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leiloes_Utilizadores_CompradorId",
                        column: x => x.CompradorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Leiloes_Utilizadores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Utilizadores",
                columns: new[] { "Id", "Nome", "Password", "Saldo" },
                values: new object[,]
                {
                    { 1, "João", "123", 1000.0 },
                    { 2, "Maria", "123", 1000.0 },
                    { 3, "José", "123", 1000.0 },
                    { 4, "Ana", "123", 1000.0 },
                    { 5, "Carlos", "123", 1000.0 }
                });

            migrationBuilder.InsertData(
                table: "Leiloes",
                columns: new[] { "Id", "CompradorId", "DataInicio", "Descricao", "Duracao", "PrecoCompraImediata", "PrecoInicial", "VendedorId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guitarra", new TimeSpan(1, 0, 0, 0, 0), 200.0, 100.0, 1 },
                    { 2, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bateria", new TimeSpan(1, 0, 0, 0, 0), 200.0, 100.0, 2 },
                    { 3, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Piano", new TimeSpan(1, 0, 0, 0, 0), 200.0, 100.0, 3 },
                    { 4, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Violino", new TimeSpan(1, 0, 0, 0, 0), 200.0, 100.0, 4 },
                    { 5, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saxofone", new TimeSpan(1, 0, 0, 0, 0), 200.0, 100.0, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leiloes_CompradorId",
                table: "Leiloes",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Leiloes_VendedorId",
                table: "Leiloes",
                column: "VendedorId");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeilaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Leiloes_LeilaoId",
                        column: x => x.LeilaoId,
                        principalTable: "Leiloes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_LeilaoId",
                table: "Item",
                column: "LeilaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leiloes");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
