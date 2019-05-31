using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Popups;
using Quisco.DataAccess;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views.Take;

namespace Quisco.ViewModels.Take
{
    public class TakeRobotSelectParametersViewModel : Observable 
    {

        public string[] QuizCategories =
        {
            "All categories",
            "General Knowledge",
            "Books",
            "Film",
            "Music",
            "Theatres",
            "Television",
            "Video Games",
            "BoardGames",
            "Science & Nature",
            "Computers",
            "Math",
            "Mythology",
            "Sports",
            "Geography",
            "History",
            "Politics",
            "Art",
            "Celebrities",
            "Animals",
            "Vehicles",
            "Comics",
            "Gadgets",
            "Anime And Manga",
            "Cartoons And Animations"
        };

        public string[] QuizDifficulty =
        {
            "All difficulties",
            "Easy",
            "Medium",
            "Hard"

        };

        public int[] QuizAmounts =
        {
            3,5,10,15,20
        };

        private int selectedCategoryIndex;
        public int SelectedCategoryIndex
        {
            get { return selectedCategoryIndex; }
            set { Set(ref selectedCategoryIndex, value); }
        }

        private string selectedCategoryItem;
        public string SelectedCategoryItem
        {
            get { return selectedCategoryItem; }
            set { Set(ref selectedCategoryItem, value); }
        }
        private string selectedDifficulty;
        public string SelectedDifficulty
        {
            get { return selectedDifficulty; }
            set { Set(ref selectedDifficulty, value); }
        }

        private int selectedAmount;
        public int SelectedAmount
        {
            get { return selectedAmount; }
            set { Set(ref selectedAmount, value); }
        }


        public async void ClickedNext()
        {
            OpentdbParams opentdbParams = new OpentdbParams();
            if (SelectedCategoryIndex > 0)
                opentdbParams.Category = "&category=" + (SelectedCategoryIndex + 8);
            else opentdbParams.Category = "";

            if (SelectedDifficulty == null || SelectedDifficulty.Equals("All difficulties", StringComparison.Ordinal))
                opentdbParams.Difficulty = "";
            else  opentdbParams.Difficulty =  "&difficulty=" + SelectedDifficulty.ToLower(new CultureInfo("en-Us",false));

            if (SelectedAmount == 0) opentdbParams.Amount = "&amount=3";
            opentdbParams.Amount = "&amount=" + selectedAmount;

            RootObject rootObject = await ExternalRequest.GetQuizzesFromExternal(opentdbParams).ConfigureAwait(true);
            

            if (rootObject == null)
            {
                DisplayErrorMessageAsync("Didn't find enough quizzes to match your request!");
                return;
            }

            Quiz quiz = new Quiz();
            quiz.QuizName = SelectedCategoryItem + " Quiz";
            quiz.QuizCategory = SelectedCategoryItem;
            foreach (var opentdbQuestion in rootObject.results)
            {
                Question question = new Question();
                question.QuestionText = System.Web.HttpUtility.HtmlDecode(opentdbQuestion.question);
                question.CorrectAnswerNumber = 1;
                question.AnswersList.Add(new Answer(System.Web.HttpUtility.HtmlDecode(opentdbQuestion.correct_answer), question,1,0));
                int i = 2;
                foreach (var answer in opentdbQuestion.incorrect_answers)
                {
                    question.AnswersList.Add(new Answer(System.Web.HttpUtility.HtmlDecode(answer), question, i++, 0));
                }
                quiz.QuestionList.Add(question);
            }
            QuizCompletionParams q = new QuizCompletionParams();
            q.Quiz = quiz;
            q.QuestionToHandle = 1;

            if (quiz.QuestionList.Count == 0)
            {
                DisplayErrorMessageAsync("Didn't find enough quizzes to match your request!");
                return;
            }
            foreach(var question in quiz.QuestionList)
                ScrambleAnswers(question);

            NavigationService.Navigate(typeof(TakeQuiz), q);

        }

        private void ScrambleAnswers(Question question)
        {
            IList<Answer> tempList = new List<Answer>();
            foreach (var a in question.AnswersList) tempList.Add(a);

            int randomNumber = RandomNumber();

            question.CorrectAnswerNumber = randomNumber+1;

            tempList[0].AnswerNumber = randomNumber +1;
            question.AnswersList[randomNumber] = tempList[0];

            tempList[randomNumber].AnswerNumber = 1;
            question.AnswersList[0] = tempList[randomNumber];
        }

        private int RandomNumber()
        {
            Random r = new Random();
            return r.Next(0, 4);
            
        }

        public void ClickedBack()
        {
            NavigationService.Navigate(typeof(TakeBotOrHuman));
        }

        private static async void DisplayErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }
    }
}
