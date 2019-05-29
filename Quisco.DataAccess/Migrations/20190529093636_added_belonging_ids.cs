using Microsoft.EntityFrameworkCore.Migrations;

namespace Quisco.DataAccess.Migrations
{
    public partial class added_belonging_ids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_BelongingQuestionQuestionId",
                table: "AnswersList");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_BelongingQuizQuizId",
                table: "QuestionList");

            migrationBuilder.DropIndex(
                name: "IX_Questions_BelongingQuizQuizId",
                table: "QuestionList");

            migrationBuilder.DropIndex(
                name: "IX_Answers_BelongingQuestionQuestionId",
                table: "AnswersList");

            migrationBuilder.DropColumn(
                name: "BelongingQuizQuizId",
                table: "QuestionList");

            migrationBuilder.DropColumn(
                name: "BelongingQuestionQuestionId",
                table: "AnswersList");

            migrationBuilder.AddColumn<int>(
                name: "BelongingQuizId",
                table: "QuestionList",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BelongingQuestionId",
                table: "AnswersList",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_BelongingQuizId",
                table: "QuestionList",
                column: "BelongingQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_BelongingQuestionId",
                table: "AnswersList",
                column: "BelongingQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_BelongingQuestionId",
                table: "AnswersList",
                column: "BelongingQuestionId",
                principalTable: "QuestionList",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_BelongingQuizId",
                table: "QuestionList",
                column: "BelongingQuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_BelongingQuestionId",
                table: "AnswersList");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_BelongingQuizId",
                table: "QuestionList");

            migrationBuilder.DropIndex(
                name: "IX_Questions_BelongingQuizId",
                table: "QuestionList");

            migrationBuilder.DropIndex(
                name: "IX_Answers_BelongingQuestionId",
                table: "AnswersList");

            migrationBuilder.DropColumn(
                name: "BelongingQuizId",
                table: "QuestionList");

            migrationBuilder.DropColumn(
                name: "BelongingQuestionId",
                table: "AnswersList");

            migrationBuilder.AddColumn<int>(
                name: "BelongingQuizQuizId",
                table: "QuestionList",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BelongingQuestionQuestionId",
                table: "AnswersList",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_BelongingQuizQuizId",
                table: "QuestionList",
                column: "BelongingQuizQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_BelongingQuestionQuestionId",
                table: "AnswersList",
                column: "BelongingQuestionQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_BelongingQuestionQuestionId",
                table: "AnswersList",
                column: "BelongingQuestionQuestionId",
                principalTable: "QuestionList",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_BelongingQuizQuizId",
                table: "QuestionList",
                column: "BelongingQuizQuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
