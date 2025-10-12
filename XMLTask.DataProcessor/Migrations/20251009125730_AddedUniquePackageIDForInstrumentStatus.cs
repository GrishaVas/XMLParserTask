using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMLTask.DataProcessor.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniquePackageIDForInstrumentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InstrumentStatuses_PackageID",
                table: "InstrumentStatuses",
                column: "PackageID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstrumentStatuses_PackageID",
                table: "InstrumentStatuses");
        }
    }
}
