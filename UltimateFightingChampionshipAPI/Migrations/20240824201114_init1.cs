using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltimateFightingChampionshipAPI.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fighters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fighters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeightClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FighterWeightClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FighterId = table.Column<int>(type: "int", nullable: false),
                    WeightClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FighterWeightClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FighterWeightClasses_Fighters_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FighterWeightClasses_WeightClasses_WeightClassId",
                        column: x => x.WeightClassId,
                        principalTable: "WeightClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_FighterWeightClasses_FighterId",
                table: "FighterWeightClasses",
                column: "FighterId");

            migrationBuilder.CreateIndex(
                name: "IX_FighterWeightClasses_WeightClassId",
                table: "FighterWeightClasses",
                column: "WeightClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FighterWeightClass");

            migrationBuilder.DropTable(
                name: "FighterWeightClasses");

            migrationBuilder.DropTable(
                name: "Fighters");
        }
    }
}
