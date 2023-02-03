using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTest.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExamTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    createdBy = table.Column<long>(type: "bigint", nullable: false),
                    updatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    createdBy = table.Column<long>(type: "bigint", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    examTypeId = table.Column<long>(type: "bigint", nullable: false),
                    orgId = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isPublic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    startDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    endDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    createdBy = table.Column<long>(type: "bigint", nullable: false),
                    updatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_ExamTypes_examTypeId",
                        column: x => x.examTypeId,
                        principalTable: "ExamTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    questionTypeId = table.Column<long>(type: "bigint", nullable: false),
                    examId = table.Column<long>(type: "bigint", nullable: false),
                    question = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    createdBy = table.Column<long>(type: "bigint", nullable: false),
                    updatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Exams_examId",
                        column: x => x.examId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionTypes_questionTypeId",
                        column: x => x.questionTypeId,
                        principalTable: "QuestionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    option = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isAnswer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    questionId = table.Column<long>(type: "bigint", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    createdBy = table.Column<long>(type: "bigint", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Questions_questionId",
                        column: x => x.questionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    questionId = table.Column<long>(type: "bigint", nullable: false),
                    optionId = table.Column<long>(type: "bigint", nullable: false),
                    answer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    createdBy = table.Column<long>(type: "bigint", nullable: false),
                    updatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Options_optionId",
                        column: x => x.optionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_questionId",
                        column: x => x.questionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ExamTypes",
                columns: new[] { "Id", "createdBy", "createdDate", "type", "updatedBy", "updatedDate" },
                values: new object[,]
                {
                    { 1L, 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified), "Developer", 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified), "Freshers", 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified), "Demo", 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "QuestionTypes",
                columns: new[] { "Id", "createdBy", "createdDate", "type", "updatedBy", "updatedDate" },
                values: new object[,]
                {
                    { 1L, 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified), "Coding", 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified), "MCQs", 0L, new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_optionId",
                table: "Answers",
                column: "optionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_questionId",
                table: "Answers",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_examTypeId",
                table: "Exams",
                column: "examTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_questionId",
                table: "Options",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_examId",
                table: "Questions",
                column: "examId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_questionTypeId",
                table: "Questions",
                column: "questionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropTable(
                name: "ExamTypes");
        }
    }
}
