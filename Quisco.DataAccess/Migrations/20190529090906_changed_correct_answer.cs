using Microsoft.EntityFrameworkCore.Migrations;

namespace Quisco.DataAccess.Migrations
{
    public partial class changed_correct_answer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Answers_CorrectAnswerAnswerId",
                table: "QuestionList");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CorrectAnswerAnswerId",
                table: "QuestionList");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerAnswerId",
                table: "QuestionList");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerNumber",
                table: "QuestionList",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswerNumber",
                table: "QuestionList");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerAnswerId",
                table: "QuestionList",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CorrectAnswerAnswerId",
                table: "QuestionList",
                column: "CorrectAnswerAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Answers_CorrectAnswerAnswerId",
                table: "QuestionList",
                column: "CorrectAnswerAnswerId",
                principalTable: "AnswersList",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
