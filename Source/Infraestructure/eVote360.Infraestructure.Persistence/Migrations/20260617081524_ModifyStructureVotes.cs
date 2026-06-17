using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVote360.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyStructureVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditVotes_Citizens_CitizenId",
                table: "AuditVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditVotes_Elections_ElectionId",
                table: "AuditVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Candidates_CandidatesId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Citizens_CitizenId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_ElectivePosictions_ElectivePositionsId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_CandidatesId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_CitizenId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_ElectivePositionsId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_AuditVotes_CitizenId",
                table: "AuditVotes");

            migrationBuilder.DropIndex(
                name: "IX_AuditVotes_ElectionId",
                table: "AuditVotes");

            migrationBuilder.DropColumn(
                name: "CandidatesId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "CitizenId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "ElectivePositionsId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "CitizenId",
                table: "AuditVotes");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "AuditVotes");

            migrationBuilder.AlterColumn<int>(
                name: "IdCandidate",
                table: "Votes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "PoliticalAlliances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Pendiente",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Pending");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdCandidate",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidatesId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CitizenId",
                table: "Votes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectivePositionsId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "PoliticalAlliances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Pendiente");

            migrationBuilder.AddColumn<Guid>(
                name: "CitizenId",
                table: "AuditVotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "AuditVotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidatesId",
                table: "Votes",
                column: "CandidatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CitizenId",
                table: "Votes",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ElectivePositionsId",
                table: "Votes",
                column: "ElectivePositionsId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditVotes_CitizenId",
                table: "AuditVotes",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditVotes_ElectionId",
                table: "AuditVotes",
                column: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditVotes_Citizens_CitizenId",
                table: "AuditVotes",
                column: "CitizenId",
                principalTable: "Citizens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditVotes_Elections_ElectionId",
                table: "AuditVotes",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Candidates_CandidatesId",
                table: "Votes",
                column: "CandidatesId",
                principalTable: "Candidates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Citizens_CitizenId",
                table: "Votes",
                column: "CitizenId",
                principalTable: "Citizens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_ElectivePosictions_ElectivePositionsId",
                table: "Votes",
                column: "ElectivePositionsId",
                principalTable: "ElectivePosictions",
                principalColumn: "Id");
        }
    }
}
