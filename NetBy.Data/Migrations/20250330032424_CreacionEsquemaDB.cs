using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetBy.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreacionEsquemaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INV_Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INV_Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anulado = table.Column<bool>(type: "bit", nullable: false),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_Compras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INV_Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anulado = table.Column<bool>(type: "bit", nullable: false),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_Ventas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INV_Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    RutaImagen = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    CostoUltimaCompra = table.Column<decimal>(type: "decimal(10,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INV_Productos_INV_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "INV_Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INV_ComprasDt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    CostoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "decimal(10,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_ComprasDt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INV_ComprasDt_INV_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "INV_Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INV_ComprasDt_INV_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "INV_Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INV_Kardex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransaccionId = table.Column<int>(type: "int", nullable: false),
                    Ingreso = table.Column<bool>(type: "bit", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioCosto = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anulado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_Kardex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INV_Kardex_INV_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "INV_Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INV_Stock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    UnidadesStock = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    UltimaEdicion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INV_Stock_INV_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "INV_Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INV_VentasDt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INV_VentasDt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INV_VentasDt_INV_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "INV_Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INV_VentasDt_INV_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "INV_Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_INV_ComprasDt_CompraId",
                table: "INV_ComprasDt",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_INV_ComprasDt_ProductoId",
                table: "INV_ComprasDt",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_INV_Kardex_ProductoId",
                table: "INV_Kardex",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_INV_Productos_CategoriaId",
                table: "INV_Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_INV_Stock_ProductoId",
                table: "INV_Stock",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_INV_VentasDt_ProductoId",
                table: "INV_VentasDt",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_INV_VentasDt_VentaId",
                table: "INV_VentasDt",
                column: "VentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INV_ComprasDt");

            migrationBuilder.DropTable(
                name: "INV_Kardex");

            migrationBuilder.DropTable(
                name: "INV_Stock");

            migrationBuilder.DropTable(
                name: "INV_VentasDt");

            migrationBuilder.DropTable(
                name: "INV_Compras");

            migrationBuilder.DropTable(
                name: "INV_Productos");

            migrationBuilder.DropTable(
                name: "INV_Ventas");

            migrationBuilder.DropTable(
                name: "INV_Categorias");
        }
    }
}
