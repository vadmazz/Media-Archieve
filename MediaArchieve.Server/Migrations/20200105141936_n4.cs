using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaArchieve.Server.Migrations
{
    public partial class n4 : Migration
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
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Item",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preview_Item_ItemId",
                table: "Preview");

            migrationBuilder.DropIndex(
                name: "IX_Preview_ItemId",
                table: "Preview");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Preview",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
