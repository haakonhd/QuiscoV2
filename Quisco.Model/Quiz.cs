using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quisco.Model
{
	[Table("Quizzes")]
	public class Quiz
	{
		[Key]
		public int QuizId { get; set; }
		[Required]
		[StringLength(100)]
		public string QuizName { get; set; }
		public string QuizCategory { get; set; }
		public string UserIdHash { get; set; }
		public ICollection<Question> Questions { get; set; } = new List<Question>();
		[NotMapped]
		public IList<Question> QuestionList { get; set; } = new List<Question>();


		public Quiz() { }

		public Quiz(string quizName)
		{
			QuizName = quizName;
		}

		public Quiz(int quizId, string quizName, string quizCategory, ICollection<Question> questions)
		{
			this.QuizId = quizId;
			this.QuizName = quizName;
			this.QuizCategory = quizCategory;
			this.Questions = questions;
		}
		public Quiz(string quizName, string quizCategory, ICollection<Question> questions)
		{
			this.QuizName = quizName;
			this.QuizCategory = quizCategory;
			this.Questions = questions;
		}
		public override string ToString()
		{
			return this.QuizName;
		}
	}
}
