using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Core.Helpers;
using Quisco.Core.Services;
using Quisco.DataAccess;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views;
using Quisco.Views.Create;

namespace Quisco.ViewModels 
{
    public class UserPageViewModel : Observable
    {

        private QuizParams quizParams;
        

        private IdentityService IdentityService => Singleton<IdentityService>.Instance;

        private ObservableCollection<Quiz> quizzesObservableCollection = new ObservableCollection<Quiz>();
        public ObservableCollection<Quiz> QuizzesObservableCollection
        {
            get { return quizzesObservableCollection; }
            set { Set(ref quizzesObservableCollection, value); }
        }
        public void Initialize()
        {
            quizParams = new QuizParams(null,1);

            FillQuizList();
        }

        public async void FillQuizList()
        {
            QuizRequest quizRequest = new QuizRequest();
            
            //TODO: errorhandling no internet
            var thisHashId = HashGenerator.ComputeSha256Hash(IdentityService.GetAccountIdentifier());

            var quizList = await quizRequest.GetQuizzesFromIdHashAsync(thisHashId).ConfigureAwait(true);
            foreach(Quiz q in quizList)
                QuizzesObservableCollection.Add(q);
        }

        

        public async void EditButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            QuizRequest quizRequest = new QuizRequest();
            if (quizParams.Quiz == null)
            {
                DisplayErrorMessageAsync("Please select a quiz from the list");
                return;
            }
            var completeQuiz = await quizRequest.GetCompleteQuizAsync(quizParams.Quiz).ConfigureAwait(true);
            quizParams.Quiz = completeQuiz;

            //TODO: error no selected quiz
                        // adds collections to list so they are easier to operate on
            int questionCounter = 1;
            foreach (Question q in completeQuiz.Questions)
            {
                q.QuestionNumber = questionCounter++;
                completeQuiz.QuestionList.Add(q);
                foreach (Answer a in q.Answers)
                    q.AnswersList.Add(a);
            }
            if (quizParams != null)
                NavigationService.Navigate(typeof(EditQuiz), quizParams);
            
        }

        public async void DeleteQuiz(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (quizParams.Quiz == null)
            {
                DisplayErrorMessageAsync("Please select a quiz from the list");
                return;
            }
            Quiz quiz = quizParams.Quiz;
            QuizRequest quizzesDataAccess = new QuizRequest();
            var myHashId = HashGenerator.ComputeSha256Hash(IdentityService.GetAccountIdentifier());
            if (myHashId != quizParams.Quiz.UserIdHash)
            {
                DisplayErrorMessageAsync("Error. UserId hash did not match quiz's UserId hash.");
                return;
            }

            bool userConfirmed = await DisplayAreYouSureDialog().ConfigureAwait(true);
            if (userConfirmed)
            {
            if (IdentityService.IsLoggedIn())
                if (await quizzesDataAccess.DeleteQuizAsync(quiz).ConfigureAwait(true))
                {
                    NavigationService.Navigate(typeof(MainPage));
                }
                else
                {
                    DisplayErrorMessageAsync("There was an error deleting the quiz.");
                }
            }
        }
        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            var selectedItem = (Quiz) e.ClickedItem;

            quizParams.Quiz = selectedItem;
        }
        private async Task<bool> DisplayAreYouSureDialog()
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "Deleting quiz.",
                Content = "Are you sure you want to delete the quiz \"" + quizParams.Quiz.QuizName + "'?" ,
                PrimaryButtonText = "Yes",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            return result == ContentDialogResult.Primary; // true if clicked "yes", false if closed
        }

        private async void DisplayErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }
    }

}
