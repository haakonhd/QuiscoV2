using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Quisco.DataAccess;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views.Create;
using Quisco.Views.Take;

namespace Quisco.ViewModels
{
    public class MainViewModel : Observable
    {
		public ObservableCollection<Quiz> QuizzesObservableCollection { get; } = new ObservableCollection<Quiz>();

        public void Initialize()
        {
            FillQuizList();
        }


        public void CreateQuiz()
        {
            Quiz quiz = new Quiz();
            QuizParams quizParams = new QuizParams(quiz, 1);

            NavigationService.Navigate(typeof(CreateQuizName), quizParams);
        }

        public void TakeQuiz()
        {
            NavigationService.Navigate(typeof(TakeBotOrHuman));
        }

        public async void FillQuizList()
        {
            Quiz[] quizList = await QuizRequest.GetQuizListAsync().ConfigureAwait(true);
            if (quizList == null)
            {
                DisplayErrorMessageAsync("There was an error loading the quizzes");
                return;
            }
            foreach (Quiz q in quizList)
                QuizzesObservableCollection.Add(q);
        }

        private static async void DisplayErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }



    }
}
