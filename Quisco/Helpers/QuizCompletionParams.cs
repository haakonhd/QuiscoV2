using Quisco.Model;

namespace Quisco.Helpers
{
    public class QuizCompletionParams
    {
        public Quiz Quiz{ get; set; }
        public int correctAnswers { get; set; }
        public int incorrectAnswers { get; set; }
    }
}
