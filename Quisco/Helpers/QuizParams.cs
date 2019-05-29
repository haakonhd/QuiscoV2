using Windows.UI.Xaml.Controls;
using Quisco.Model;

namespace Quisco.Helpers
{
    public class QuizParams
    {
        public Quiz Quiz { get; set; }
        public int QuestionToHandle { get; set; }
        public Frame CurrentFrame { get; set; }

        public QuizParams(Quiz quiz, int questionToHandle)
        {
            this.Quiz = quiz;
            this.QuestionToHandle = questionToHandle;
        }
    }
}
