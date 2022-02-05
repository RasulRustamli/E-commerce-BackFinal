using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce_BackFinal.Migrations
{
    public partial class SubscribeModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Subcribes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Subcribes");
        }
    }
}
