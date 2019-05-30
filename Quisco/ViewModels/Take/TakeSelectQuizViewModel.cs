using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Quisco.DataAccess;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views;
using Quisco.Views.Take;

namespace Quisco.ViewModels.Take
{
    public class TakeSelectQuizViewModel : Observable
    {
        private Quiz quiz;

        private string quizNameTextBlock;
        public string QuizNameTextBlock
        {
            get { return quizNameTextBlock; }
            set { Set(ref quizNameTextBlock, value); }
        }

        private string quizCategoryTextBlock;
        public string QuizCategoryTextBlock
        {
            get { return quizCategoryTextBlock; }
            set { Set(ref quizCategoryTextBlock, value); }
        }

        private string quizQuestionsAmountTextBlock;
        public string QuizQuestionsAmountTextBlock
        {
            get { return quizQuestionsAmountTextBlock; }
            set { Set(ref quizQuestionsAmountTextBlock, value); }
        }

        private ObservableCollection<Quiz> quizzesObservableCollection = new ObservableCollection<Quiz>();

        public ObservableCollection<Quiz> QuizzesObservableCollection
        {
            get { return quizzesObservableCollection; }
            set { Set(ref quizzesObservableCollection, value); }
        }

        public void Initialize()
        {

            FillQuizList();
        }

        public async void FillQuizList()
        {
            QuizRequest quizRequest = new QuizRequest();

            //TODO: errorhandling no internet

            var quizList = await quizRequest.GetQuizListAsync().ConfigureAwait(true);
            foreach (Quiz q in quizList)
                QuizzesObservableCollection.Add(q);
        }

        public void TakeQuizButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (quiz == null)
            {
                DisplayErrorMessageAsync("Please select a quiz from the list");
                return;
            }

            // adds collections to list so they are easier to operate on
            int questionCounter = 1;
            foreach (Question q in quiz.Questions)
            {
                q.QuestionNumber = questionCounter++;
                quiz.QuestionList.Add(q);
                foreach (Answer a in q.Answers)
                    q.AnswersList.Add(a);
            }

            if (quiz != null)
            {
                QuizParams quizParams = new QuizParams(quiz, 1);
                NavigationService.Navigate(typeof(TakeQuiz), quizParams);
            }
        }

        public async void ClickItemList(object sender, ItemClickEventArgs e)
        {
            QuizRequest quizRequest = new QuizRequest();
            var selectedItem = (Quiz)e.ClickedItem;
            quiz = await quizRequest.GetCompleteQuizAsync(selectedItem).ConfigureAwait(true);
            QuizNameTextBlock = quiz.QuizName;
            QuizCategoryTextBlock = quiz.QuizCategory;
            QuizQuestionsAmountTextBlock = quiz.Questions.Count + " questions.";
        }

        private async void DisplayErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }
    }
}
