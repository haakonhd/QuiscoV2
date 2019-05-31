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
            get => quizNameTextBlock; 
            set => Set(ref quizNameTextBlock, value); 
        }

        private string quizCategoryTextBlock;
        public string QuizCategoryTextBlock
        {
            get => quizCategoryTextBlock;
            set => Set(ref quizCategoryTextBlock, value);
        }

        private string quizQuestionsAmountTextBlock;
        public string QuizQuestionsAmountTextBlock
        {
            get { return quizQuestionsAmountTextBlock; }
            set { Set(ref quizQuestionsAmountTextBlock, value); }
        }


        public ObservableCollection<Quiz> QuizzesObservableCollection { get; } = new ObservableCollection<Quiz>();

        public void Initialize()
        {

            FillQuizList();
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

        public void TakeQuizButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (quiz == null)
            {
                DisplayErrorMessageAsync("Please select a quiz from the list");
                return;
            }

            // adds collections to list so they are easier to operate on
            var questionCounter = 1;
            foreach (Question q in quiz.Questions)
            {
                q.QuestionNumber = questionCounter++;
                quiz.QuestionList.Add(q);
                foreach (Answer a in q.Answers)
                    q.AnswersList.Add(a);
            }

            if (quiz != null)
            {
                var quizCompletionParams = new QuizCompletionParams {Quiz = quiz};
                NavigationService.Navigate(typeof(TakeQuiz), quizCompletionParams);
            }
        }

        public async void ClickItemList(object sender, ItemClickEventArgs e)
        {
            var selectedItem = (Quiz)e.ClickedItem;
            quiz = await QuizRequest.GetCompleteQuizAsync(selectedItem).ConfigureAwait(true);
            QuizNameTextBlock = quiz.QuizName;
            QuizCategoryTextBlock = quiz.QuizCategory;
            QuizQuestionsAmountTextBlock = quiz.Questions.Count + " questions.";
        }

        private static async void DisplayErrorMessageAsync(string errorMessage)
        {
            var dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }
    }
}
