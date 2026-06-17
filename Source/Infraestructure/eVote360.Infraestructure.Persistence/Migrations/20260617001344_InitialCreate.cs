using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVote360.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UpdateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citizens_Users_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citizens_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ElectionState = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elections_Users_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Elections_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoliticalParties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoliticalPartyDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PoliticalPartyAcronym = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PoliticalPartyLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticalParties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliticalParties_Users_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoliticalParties_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCitizen = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdElection = table.Column<int>(type: "int", nullable: false),
                    CreatAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CitizenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ElectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditVotes_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditVotes_Citizens_IdCitizen",
                        column: x => x.IdCitizen,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditVotes_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditVotes_Elections_IdElection",
                        column: x => x.IdElection,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CodeVerification",
                columns: table => new
                {
                    IdCitizens = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdElection = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeVerification", x => new { x.IdCitizens, x.IdElection });
                    table.ForeignKey(
                        name: "FK_CodeVerification_Citizens_IdCitizens",
                        column: x => x.IdCitizens,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CodeVerification_Elections_IdElection",
                        column: x => x.IdElection,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectivePosictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ElectionId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, collation: "Modern_Spanish_CI_AI"),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    UpdateAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectivePosictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectivePosictions_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ElectivePosictions_Users_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectivePosictions_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliticalPartyId = table.Column<int>(type: "int", nullable: false),
                    HasParticipatedInElection = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    UpdateAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_PoliticalParties_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidates_Users_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidates_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoliticalAlliances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestingPartyId = table.Column<int>(type: "int", nullable: false),
                    ReceivingPartyId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Pending"),
                    RequestDate = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ResponseDate = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticalAlliances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliticalAlliances_PoliticalParties_ReceivingPartyId",
                        column: x => x.ReceivingPartyId,
                        principalTable: "PoliticalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoliticalAlliances_PoliticalParties_RequestingPartyId",
                        column: x => x.RequestingPartyId,
                        principalTable: "PoliticalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoliticalAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoliticalLeaderId = table.Column<int>(type: "int", nullable: false),
                    PoliticalPartyId = table.Column<int>(type: "int", nullable: false),
                    PolitcalAssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: true),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticalAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliticalAssignments_PoliticalParties_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoliticalAssignments_Users_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoliticalAssignments_Users_PoliticalLeaderId",
                        column: x => x.PoliticalLeaderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoliticalAssignments_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CandidateAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssigningPartyId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    ElectivePositionId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    ElectivePositionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateAssignments_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CandidateAssignments_ElectivePosictions_ElectivePositionId",
                        column: x => x.ElectivePositionId,
                        principalTable: "ElectivePosictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CandidateAssignments_ElectivePosictions_ElectivePositionsId",
                        column: x => x.ElectivePositionsId,
                        principalTable: "ElectivePosictions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateAssignments_PoliticalParties_AssigningPartyId",
                        column: x => x.AssigningPartyId,
                        principalTable: "PoliticalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdElection = table.Column<int>(type: "int", nullable: false),
                    IdElectivePosiction = table.Column<int>(type: "int", nullable: false),
                    IdCandidate = table.Column<int>(type: "int", nullable: false),
                    CandidatesId = table.Column<int>(type: "int", nullable: true),
                    CitizenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ElectivePositionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votes_Candidates_IdCandidate",
                        column: x => x.IdCandidate,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votes_Elections_IdElection",
                        column: x => x.IdElection,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_ElectivePosictions_ElectivePositionsId",
                        column: x => x.ElectivePositionsId,
                        principalTable: "ElectivePosictions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votes_ElectivePosictions_IdElectivePosiction",
                        column: x => x.IdElectivePosiction,
                        principalTable: "ElectivePosictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditVotes_CitizenId",
                table: "AuditVotes",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditVotes_ElectionId",
                table: "AuditVotes",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditVotes_IdCitizen",
                table: "AuditVotes",
                column: "IdCitizen");

            migrationBuilder.CreateIndex(
                name: "IX_AuditVotes_IdElection",
                table: "AuditVotes",
                column: "IdElection");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAssignments_AssigningPartyId_CandidateId",
                table: "CandidateAssignments",
                columns: new[] { "AssigningPartyId", "CandidateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAssignments_AssigningPartyId_ElectivePositionId",
                table: "CandidateAssignments",
                columns: new[] { "AssigningPartyId", "ElectivePositionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAssignments_CandidateId",
                table: "CandidateAssignments",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAssignments_ElectivePositionId",
                table: "CandidateAssignments",
                column: "ElectivePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateAssignments_ElectivePositionsId",
                table: "CandidateAssignments",
                column: "ElectivePositionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CreateUserId",
                table: "Candidates",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PoliticalPartyId",
                table: "Candidates",
                column: "PoliticalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_UpdateUserId",
                table: "Candidates",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_CreateUserId",
                table: "Citizens",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_Email",
                table: "Citizens",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_IdentificationNumber",
                table: "Citizens",
                column: "IdentificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_UpdateUserId",
                table: "Citizens",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeVerification_IdElection",
                table: "CodeVerification",
                column: "IdElection");

            migrationBuilder.CreateIndex(
                name: "IX_Elections_CreateUserId",
                table: "Elections",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Elections_Name",
                table: "Elections",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Elections_UpdateUserId",
                table: "Elections",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectivePosictions_CreateUserId",
                table: "ElectivePosictions",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectivePosictions_ElectionId",
                table: "ElectivePosictions",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectivePosictions_Name",
                table: "ElectivePosictions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectivePosictions_UpdateUserId",
                table: "ElectivePosictions",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalAlliances_ReceivingPartyId",
                table: "PoliticalAlliances",
                column: "ReceivingPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalAlliances_RequestingPartyId",
                table: "PoliticalAlliances",
                column: "RequestingPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalAssignments_CreateUserId",
                table: "PoliticalAssignments",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalAssignments_PoliticalLeaderId",
                table: "PoliticalAssignments",
                column: "PoliticalLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalAssignments_PoliticalPartyId",
                table: "PoliticalAssignments",
                column: "PoliticalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalAssignments_UpdateUserId",
                table: "PoliticalAssignments",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalParties_CreateUserId",
                table: "PoliticalParties",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalParties_Name",
                table: "PoliticalParties",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalParties_PoliticalPartyAcronym",
                table: "PoliticalParties",
                column: "PoliticalPartyAcronym",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalParties_UpdateUserId",
                table: "PoliticalParties",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

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
                name: "IX_Votes_IdCandidate",
                table: "Votes",
                column: "IdCandidate");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_IdElection",
                table: "Votes",
                column: "IdElection");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_IdElectivePosiction",
                table: "Votes",
                column: "IdElectivePosiction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditVotes");

            migrationBuilder.DropTable(
                name: "CandidateAssignments");

            migrationBuilder.DropTable(
                name: "CodeVerification");

            migrationBuilder.DropTable(
                name: "PoliticalAlliances");

            migrationBuilder.DropTable(
                name: "PoliticalAssignments");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Citizens");

            migrationBuilder.DropTable(
                name: "ElectivePosictions");

            migrationBuilder.DropTable(
                name: "PoliticalParties");

            migrationBuilder.DropTable(
                name: "Elections");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
