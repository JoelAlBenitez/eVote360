using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVote360.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCandidactesAssigment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateAssignments_ElectivePosictions_ElectivePositionsId",
                table: "CandidateAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CandidateAssignments_ElectivePositionsId",
                table: "CandidateAssignments");

            migrationBuilder.DropColumn(
                name: "ElectivePositionsId",
                table: "CandidateAssignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElectivePositionsId",
                table: "CandidateAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAssignments_ElectivePositionsId",
                table: "CandidateAssignments",
                column: "ElectivePositionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateAssignments_ElectivePosictions_ElectivePositionsId",
                table: "CandidateAssignments",
                column: "ElectivePositionsId",
                principalTable: "ElectivePosictions",
                principalColumn: "Id");
        }
    }
}
