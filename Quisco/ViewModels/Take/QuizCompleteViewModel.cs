using Quisco.Helpers;
using Quisco.Services;
using Quisco.Views;

namespace Quisco.ViewModels.Take
{
    public class QuizCompleteViewModel : Observable
    {
        private string quizNameText;
        public string QuizNameText
        {
            get => quizNameText;
            set => Set(ref quizNameText, value);
        }

        private string answerResultText;
        public string AnswerResultText
        {
            get => answerResultText;
            set => Set(ref answerResultText, value);
        }

        public void Initialize(QuizCompletionParams quizCompletionParams)
        {

            quizNameText = quizCompletionParams.Quiz.QuizName;
            answerResultText = quizCompletionParams.correctAnswers + " correct answers out of " + quizCompletionParams.Quiz.QuestionList.Count;
        }

        public void GoBackButton()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

    }
}
