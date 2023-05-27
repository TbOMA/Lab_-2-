using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab__2_.Migrations
{
    /// <inheritdoc />
    public partial class changed_rentalForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalForm_Cars_CarID",
                table: "RentalForm");

            migrationBuilder.DropIndex(
                name: "IX_RentalForm_CarID",
                table: "RentalForm");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PassportNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RentalFormVmId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RentalFormVmId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_RentalFormVmId",
                table: "Clients",
                column: "RentalFormVmId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RentalFormVmId",
                table: "Cars",
                column: "RentalFormVmId",
                unique: true,
                filter: "[RentalFormVmId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_RentalForm_RentalFormVmId",
                table: "Cars",
                column: "RentalFormVmId",
                principalTable: "RentalForm",
                principalColumn: "RentalFormID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_RentalForm_RentalFormVmId",
                table: "Clients",
                column: "RentalFormVmId",
                principalTable: "RentalForm",
                principalColumn: "RentalFormID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_RentalForm_RentalFormVmId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_RentalForm_RentalFormVmId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_RentalFormVmId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RentalFormVmId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RentalFormVmId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RentalFormVmId",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PassportNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalForm_CarID",
                table: "RentalForm",
                column: "CarID");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalForm_Cars_CarID",
                table: "RentalForm",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
