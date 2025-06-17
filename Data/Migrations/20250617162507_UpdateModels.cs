using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPositionLanguage");

            migrationBuilder.AddColumn<int>(
                name: "JobPositionId",
                table: "Language",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecruiterId",
                table: "JobPosition",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Language_JobPositionId",
                table: "Language",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPosition_RecruiterId",
                table: "JobPosition",
                column: "RecruiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosition_AspNetUsers_RecruiterId",
                table: "JobPosition",
                column: "RecruiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_JobPosition_JobPositionId",
                table: "Language",
                column: "JobPositionId",
                principalTable: "JobPosition",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosition_AspNetUsers_RecruiterId",
                table: "JobPosition");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_JobPosition_JobPositionId",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Language_JobPositionId",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_JobPosition_RecruiterId",
                table: "JobPosition");

            migrationBuilder.DropColumn(
                name: "JobPositionId",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "RecruiterId",
                table: "JobPosition");

            migrationBuilder.CreateTable(
                name: "JobPositionLanguage",
                columns: table => new
                {
                    JobPositionsId = table.Column<int>(type: "int", nullable: false),
                    RequiredLanguagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositionLanguage", x => new { x.JobPositionsId, x.RequiredLanguagesId });
                    table.ForeignKey(
                        name: "FK_JobPositionLanguage_JobPosition_JobPositionsId",
                        column: x => x.JobPositionsId,
                        principalTable: "JobPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPositionLanguage_Language_RequiredLanguagesId",
                        column: x => x.RequiredLanguagesId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPositionLanguage_RequiredLanguagesId",
                table: "JobPositionLanguage",
                column: "RequiredLanguagesId");
        }
    }
}
