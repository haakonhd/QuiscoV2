using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

            var quizList = await quizRequest.GetQuizzesFromIdHash(thisHashId).ConfigureAwait(true);
            foreach(Quiz q in quizList)
                QuizzesObservableCollection.Add(q);
        }

        

        public async void EditButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            QuizRequest quizRequest = new QuizRequest();
            var completeQuiz = await quizRequest.GetCompleteQuiz(quizParams.Quiz).ConfigureAwait(true);
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
        public void DeleteQuiz(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            var selectedItem = (Quiz) e.ClickedItem;

            quizParams.Quiz = selectedItem;
        }

    }

}
