using Quisco.Model;

namespace Quisco.Helpers
{
    public class QuizCompletionParams
    {
        public Quiz Quiz{ get; set; }
        public int correctAnswers { get; set; }
        public int incorrectAnswers { get; set; }
        public int QuestionToHandle { get; set; }

        public QuizCompletionParams()
        {
            correctAnswers = 0;
            incorrectAnswers = 0;
            QuestionToHandle = 1;
        }
    }
}
