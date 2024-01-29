﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Music4Every1.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Saldo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Leiloes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendedorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompradorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    PrecoInicial = table.Column<double>(type: "float", nullable: false),
                    PrecoCompraImediata = table.Column<double>(type: "float", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leiloes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leiloes_Utilizadores_CompradorId",
                        column: x => x.CompradorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_Leiloes_Utilizadores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoredFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeilaoId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagens_Leiloes_LeilaoId",
                        column: x => x.LeilaoId,
                        principalTable: "Leiloes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Utilizadores",
                columns: new[] { "Email", "Nome", "PasswordHash", "PasswordSalt", "Saldo" },
                values: new object[,]
                {
                    { "ana@gmail.com", "Ana", new byte[] { 49, 50, 51 }, new byte[] { 49, 50, 51 }, 1000.0 },
                    { "carlos@gmail.com", "Carlos", new byte[] { 49, 50, 51 }, new byte[] { 49, 50, 51 }, 1000.0 },
                    { "joao@gmail.com", "João", new byte[] { 49, 50, 51 }, new byte[] { 49, 50, 51 }, 1000.0 },
                    { "jose@gmail.com", "José", new byte[] { 49, 50, 51 }, new byte[] { 49, 50, 51 }, 1000.0 },
                    { "maria@gmail.com", "Maria", new byte[] { 49, 50, 51 }, new byte[] { 49, 50, 51 }, 1000.0 }
                });

            migrationBuilder.InsertData(
                table: "Leiloes",
                columns: new[] { "Id", "CompradorId", "DataInicio", "Descricao", "Duracao", "Estado", "PrecoCompraImediata", "PrecoInicial", "VendedorId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guitarra", 1, "FINISHED", 200.0, 100.0, "joao@gmail.com" },
                    { 2, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bateria", 1, "FINISHED", 200.0, 100.0, "maria@gmail.com" },
                    { 3, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Piano", 1, "FINISHED", 200.0, 100.0, "jose@gmail.com" },
                    { 4, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Violino", 1, "FINISHED", 200.0, 100.0, "ana@gmail.com" },
                    { 5, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saxofone", 1, "FINISHED", 200.0, 100.0, "carlos@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Categoria", "LeilaoId", "Nome" },
                values: new object[,]
                {
                    { 1, "electric", 1, "Guitarra Elétrica" },
                    { 2, "percussion", 2, "Bateria" },
                    { 3, "strings", 3, "Piano" },
                    { 4, "strings", 4, "Violino" },
                    { 5, "wind", 5, "Saxofone" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imagens_LeilaoId",
                table: "Imagens",
                column: "LeilaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_LeilaoId",
                table: "Item",
                column: "LeilaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Leiloes_CompradorId",
                table: "Leiloes",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Leiloes_VendedorId",
                table: "Leiloes",
                column: "VendedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagens");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Leiloes");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}