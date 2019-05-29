using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quisco.Model
{
	[Table("Questions")]
	public class Question
	{
		[Key]
		public int QuestionId { get; set; }
		[Required]
		[StringLength(500)]
		public string QuestionText { get; set; }
		[NotMapped]
		public int QuestionNumber { get; set; }
		public int CorrectAnswerNumber { get; set; }
		public Quiz BelongingQuiz { get; set; }
		public int BelongingQuizId { get; set; }
		[NotMapped]
		public IList<Answer> AnswersList { get; set; } = new List<Answer>();
		public ICollection<Answer> Answers { get; set; } = new List<Answer>();

		public Question() { }

		public Question(string questionText)
		{
			this.QuestionText = questionText;
		}

		public Question(int questionId, string questionText, ICollection<Answer> answers, int belongingQuizId)
		{
			this.QuestionId = questionId;
			this.QuestionText = questionText;
			this.Answers = answers;
			this.BelongingQuizId = belongingQuizId;
		}

		public Question(int questionId, string questionText, int questionNumber, int belongingQuizId)
		{
			this.QuestionId = questionId;
			this.QuestionText = questionText;
			this.QuestionNumber = questionNumber;
			this.BelongingQuizId = belongingQuizId;
		}

		public Question(string questionText, ICollection<Answer> answers, int belongingQuizId)
		{
			this.QuestionText = questionText;
			this.Answers = answers;
			this.BelongingQuizId = belongingQuizId;
		}

		public override string ToString()
		{
			return this.QuestionText;
		}
	}
}