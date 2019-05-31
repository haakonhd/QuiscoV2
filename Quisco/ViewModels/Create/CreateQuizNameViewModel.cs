using System;
using System.ComponentModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views;
using Quisco.Views.Create;

namespace Quisco.ViewModels
{
    public class CreateQuizNameViewModel : Observable
    {
        private QuizParams quizParams;
        private Quiz quiz;

        public CreateQuizNameViewModel(QuizParams quizParams)
        {
            this.quizParams = quizParams;
            quiz = quizParams.Quiz;
            if (quiz.QuizName != null)
                QuestionInputText = quiz.QuizName;
        }



        private string questionInputText;
        public string QuestionInputText
        {
            get => questionInputText;
            set => this.Set(ref questionInputText, value);
        }

        public void ClickedBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        public void ClickedNext(object sender, RoutedEventArgs e)
        {
            bool quizNameIsValid = true;
            if (QuestionInputText == null) quizNameIsValid = false;
            else if (QuestionInputText.Length < 3) quizNameIsValid = false;

            if (!quizNameIsValid)
            {
                DisplayErrorMessageAsync("Please enter a quiz name with 3 or more characters");
                return;
            }
            string quizName = QuestionInputText;
            quizParams.Quiz.QuizName = quizName;
            NavigationService.Navigate(typeof(CreateQuizCategory), quizParams);

        }

        private static async void DisplayErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }
    }
}
