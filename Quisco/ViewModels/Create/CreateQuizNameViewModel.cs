using System.ComponentModel;
using Windows.UI.Xaml;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views;
using Quisco.Views.Create;

namespace Quisco.ViewModels
{
    public class CreateQuizNameViewModel : BindableBase, INotifyPropertyChanged
    {
        private QuizParams quizParams;
        private Quiz quiz;
        private int questionToHandle;

        public CreateQuizNameViewModel(QuizParams quizParams)
        {
            this.quizParams = quizParams;
            quiz = quizParams.Quiz;
            questionToHandle = quizParams.QuestionToHandle;
            if (quiz.QuizName != null)
                QuestionInputText = quiz.QuizName;
        }



        private string questionInputText;
        public string QuestionInputText
        {
            get => questionInputText;
            set => this.SetProperty(ref questionInputText, value);
        }

        public void ClickedBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        public void ClickedNext(object sender, RoutedEventArgs e)
        {
            string quizName = QuestionInputText;
            quizParams.Quiz.QuizName = quizName;
            NavigationService.Navigate(typeof(CreateQuizCategory), quizParams);

        }
    }
}
