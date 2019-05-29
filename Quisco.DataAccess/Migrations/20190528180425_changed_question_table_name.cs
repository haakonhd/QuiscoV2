using Microsoft.EntityFrameworkCore.Migrations;

namespace Quisco.DataAccess.Migrations
{
    public partial class changed_question_table_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Question_BelongingQuestionQuestionId",
                table: "AnswersList");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Question_QuestionId",
                table: "AnswersList");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quizzes_BelongingQuizQuizId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Answers_CorrectAnswerAnswerId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "QuestionList");

            migrationBuilder.RenameIndex(
                name: "IX_Question_CorrectAnswerAnswerId",
                table: "QuestionList",
                newName: "IX_Questions_CorrectAnswerAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_BelongingQuizQuizId",
                table: "QuestionList",
                newName: "IX_Questions_BelongingQuizQuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "QuestionList",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_BelongingQuestionQuestionId",
                table: "AnswersList",
                column: "BelongingQuestionQuestionId",
                principalTable: "QuestionList",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "AnswersList",
                column: "QuestionId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Answers_CorrectAnswerAnswerId",
                table: "QuestionList",
                column: "CorrectAnswerAnswerId",
                principalTable: "AnswersList",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_BelongingQuestionQuestionId",
                table: "AnswersList");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "AnswersList");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_BelongingQuizQuizId",
                table: "QuestionList");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Answers_CorrectAnswerAnswerId",
                table: "QuestionList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "QuestionList");

            migrationBuilder.RenameTable(
                name: "QuestionList",
                newName: "Question");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_CorrectAnswerAnswerId",
                table: "Question",
                newName: "IX_Question_CorrectAnswerAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_BelongingQuizQuizId",
                table: "Question",
                newName: "IX_Question_BelongingQuizQuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Question_BelongingQuestionQuestionId",
                table: "AnswersList",
                column: "BelongingQuestionQuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Question_QuestionId",
                table: "AnswersList",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quizzes_BelongingQuizQuizId",
                table: "Question",
                column: "BelongingQuizQuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Answers_CorrectAnswerAnswerId",
                table: "Question",
                column: "CorrectAnswerAnswerId",
                principalTable: "AnswersList",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
