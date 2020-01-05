using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaArchieve.Server.Migrations
{
    public partial class n2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preview_Item_ItemId",
                table: "Preview");

            migrationBuilder.DropIndex(
                name: "IX_Preview_ItemId",
                table: "Preview");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Preview",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Preview_ItemId",
                table: "Preview",
                column: "ItemId",
                unique: true,
                filter: "[ItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Preview_Item_ItemId",
                table: "Preview",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preview_Item_ItemId",
                table: "Preview");

            migrationBuilder.DropIndex(
                name: "IX_Preview_ItemId",
                table: "Preview");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Preview",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Preview_ItemId",
                table: "Preview",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Preview_Item_ItemId",
                table: "Preview",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
