using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Localize.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEmpresarial = table.Column<string>(type: "varchar(255)", nullable: false),
                    NomeFantasia = table.Column<string>(type: "varchar(255)", nullable: false),
                    Cnpj = table.Column<string>(type: "varchar(255)", nullable: false),
                    Situacao = table.Column<string>(type: "varchar(255)", nullable: false),
                    Abertura = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(255)", nullable: false),
                    NaturezaLegal = table.Column<string>(type: "varchar(255)", nullable: false),
                    AtividadePrincipal = table.Column<string>(type: "varchar(255)", nullable: false),
                    Endereco_Rua = table.Column<string>(type: "varchar(255)", nullable: false),
                    Endereco_Numero = table.Column<string>(type: "varchar(255)", nullable: false),
                    Endereco_Complemento = table.Column<string>(type: "varchar(255)", nullable: false),
                    Endereco_Bairro = table.Column<string>(type: "varchar(255)", nullable: false),
                    Endereco_Cidade = table.Column<string>(type: "varchar(255)", nullable: false),
                    Endereco_Estado = table.Column<string>(type: "varchar(255)", nullable: false),
                    Endereco_Cep = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UserId",
                table: "Organizations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
