using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposAuditoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Anulado",
                table: "INV_Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "INV_Productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimaEdicion",
                table: "INV_Productos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Anulado",
                table: "INV_Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "INV_Categorias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimaEdicion",
                table: "INV_Categorias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anulado",
                table: "INV_Productos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "INV_Productos");

            migrationBuilder.DropColumn(
                name: "FechaUltimaEdicion",
                table: "INV_Productos");

            migrationBuilder.DropColumn(
                name: "Anulado",
                table: "INV_Categorias");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "INV_Categorias");

            migrationBuilder.DropColumn(
                name: "FechaUltimaEdicion",
                table: "INV_Categorias");
        }
    }
}
