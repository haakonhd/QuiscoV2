using System.ComponentModel.DataAnnotations.Schema;

namespace Quisco.Model
{
	public class Answer
	{
		public int AnswerId { get; set; }
		public string AnswerText { get; set; }
		public int AnswerNumber { get; set; }
		public Question BelongingQuestion { get; set; }
		public int BelongingQuestionId { get; set; }
		[NotMapped]
		public bool ToBeDeleted { get; set; }

		public Answer(string answerText, Question belongingQuestion, int answerNumber, int belongingQuestionId)
		{
			this.AnswerText = answerText;
			this.BelongingQuestion = belongingQuestion;
			this.AnswerNumber = answerNumber;
			this.BelongingQuestionId = belongingQuestionId;
			this.ToBeDeleted = false;
		}

		public Answer(int answerId, string answerText, Question belongingQuestion, int answerNumber, int belongingQuestionId)
		{
			this.AnswerId = answerId;
			this.AnswerText = answerText;
			this.BelongingQuestion = belongingQuestion;
			this.AnswerNumber = answerNumber;
			this.BelongingQuestionId = belongingQuestionId;
			this.ToBeDeleted = false;
		}

		public Answer(int answerId, string answerText, int answerNumber, int belongingQuestionId)
		{
			this.AnswerId = answerId;
			this.AnswerText = answerText;
			this.AnswerNumber = answerNumber;
			this.BelongingQuestionId = belongingQuestionId;
			this.ToBeDeleted = false;
		}

		public Answer()
		{
			this.ToBeDeleted = false;
		}

		public override string ToString()
		{
			return this.AnswerText;
		}
	}
}