using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Quisco.Core.Helpers;
using Quisco.Core.Models;
using Quisco.Core.Services;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views.Create;
using Quisco.Views.Take;

namespace Quisco.ViewModels
{
    public class MainViewModel : Observable
    {
        private Quiz quiz;

        static Uri quizzesBaseUri = new Uri("http://localhost:55418/api/Quizzes");

        HttpClient _httpClient = new HttpClient();

        private IdentityService IdentityService => Singleton<IdentityService>.Instance;

        private ObservableCollection<Quiz> quizzesObservableCollection = new ObservableCollection<Quiz>();
        public ObservableCollection<Quiz> QuizzesObservableCollection
        {
            get { return quizzesObservableCollection; }
            set { Set(ref quizzesObservableCollection, value); }
        }

        public MainViewModel()
        {
        }

        public async Task InitializeAsync()
        {
            bool IsLoggedIn = IdentityService.IsLoggedIn();

            FillQuizList();
        }



        public void CreateQuiz(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Quiz quiz = new Quiz();
            QuizParams quizParams = new QuizParams(quiz, 1);

            NavigationService.Navigate(typeof(CreateQuizName), quizParams);
        }

        public void TakeQuiz(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(TakeBotOrHuman));
        }


        public async Task<Quiz[]> GetQuizList()
        {
            var result = await _httpClient.GetAsync(quizzesBaseUri);
            var json = await result.Content.ReadAsStringAsync();
            var quizList = JsonConvert.DeserializeObject<Quiz[]>(json);
            return quizList;
        }

        public async void FillQuizList()
        {
            //TODO: errorhandling no internet
            var quizList = await GetQuizList().ConfigureAwait(true);
            // inserts quizzes to the listView with the newest first
            foreach (var quiz in quizList)
            {
                QuizzesObservableCollection.Add(quiz);
            }
        }

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            var clickedQuestion = (Quiz)e.ClickedItem;

            //            Frame.Navigate(typeof(CreateQuestion), quizParams);
        }

        private async void DisplayErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }
        

    }
}
