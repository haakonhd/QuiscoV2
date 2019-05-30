using Windows.UI.Xaml.Controls;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views.Take;

namespace Quisco.ViewModels.Take
{
    public class TakeQuizViewModel : Observable
    {
        private QuizParams quizParams;
        private Quiz quiz;
        private int questionToHandle;

        private string quizNameTextBlock;
        public string QuizNameTextBlock
        {
            get => quizNameTextBlock;
            set => Set(ref quizNameTextBlock, value); 
        }

        private bool showAnswerIsVisible;
        public bool ShowAnswerIsVisible
        {
            get => showAnswerIsVisible; 
            set => Set(ref showAnswerIsVisible, value); 
        }

        private bool nextQuestionIsVisible;
        public bool NextQuestionIsVisible
        {
            get => nextQuestionIsVisible;
            set => Set(ref nextQuestionIsVisible, value);
        }

        private bool previousQuestionIsVisible;
        public bool PreviousQuestionIsVisible
        {
            get => previousQuestionIsVisible;
            set => Set(ref previousQuestionIsVisible, value);
        }

        public void ShowAnswerButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        public void NextQuestionButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }
        public void PreviousQuestionButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        public void Initialize(QuizParams quizParams)
        {
            this.quizParams = quizParams;
            this.quiz = quizParams.Quiz;
            this.questionToHandle = quizParams.QuestionToHandle;
            NextQuestionIsVisible = false;

        }

    }
}
