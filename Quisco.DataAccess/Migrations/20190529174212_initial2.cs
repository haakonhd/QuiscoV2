using Microsoft.EntityFrameworkCore.Migrations;

namespace Quisco.DataAccess.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionList_BelongingQuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionList_Quizzes_BelongingQuizId",
                table: "QuestionList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionList",
                table: "QuestionList");

            migrationBuilder.RenameTable(
                name: "QuestionList",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionList_BelongingQuizId",
                table: "Questions",
                newName: "IX_Questions_BelongingQuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_BelongingQuestionId",
                table: "Answers",
                column: "BelongingQuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_BelongingQuizId",
                table: "Questions",
                column: "BelongingQuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_BelongingQuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_BelongingQuizId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "QuestionList");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_BelongingQuizId",
                table: "QuestionList",
                newName: "IX_QuestionList_BelongingQuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionList",
                table: "QuestionList",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionList_BelongingQuestionId",
                table: "Answers",
                column: "BelongingQuestionId",
                principalTable: "QuestionList",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionList_Quizzes_BelongingQuizId",
                table: "QuestionList",
                column: "BelongingQuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
