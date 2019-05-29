using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quisco.DataAccess.Migrations
{
    public partial class quiz_id_hash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuizName = table.Column<string>(maxLength: 100, nullable: false),
                    QuizCategory = table.Column<string>(nullable: true),
                    UserIdHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionText = table.Column<string>(maxLength: 500, nullable: false),
                    CorrectAnswerAnswerId = table.Column<int>(nullable: true),
                    BelongingQuizQuizId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_Quizzes_BelongingQuizQuizId",
                        column: x => x.BelongingQuizQuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswersList",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerText = table.Column<string>(nullable: true),
                    AnswerNumber = table.Column<int>(nullable: false),
                    BelongingQuestionQuestionId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Question_BelongingQuestionQuestionId",
                        column: x => x.BelongingQuestionQuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_BelongingQuestionQuestionId",
                table: "AnswersList",
                column: "BelongingQuestionQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "AnswersList",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_BelongingQuizQuizId",
                table: "Question",
                column: "BelongingQuizQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CorrectAnswerAnswerId",
                table: "Question",
                column: "CorrectAnswerAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Answers_CorrectAnswerAnswerId",
                table: "Question",
                column: "CorrectAnswerAnswerId",
                principalTable: "AnswersList",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Question_BelongingQuestionQuestionId",
                table: "AnswersList");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Question_QuestionId",
                table: "AnswersList");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "AnswersList");
        }
    }
}
