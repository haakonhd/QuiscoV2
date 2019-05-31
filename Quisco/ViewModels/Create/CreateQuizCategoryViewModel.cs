using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views.Create;
using CreateQuizNamePage = Quisco.Views.Create.CreateQuizName;

namespace Quisco.ViewModels.Create
{
    public class CreateQuizCategoryViewModel : Observable

    {
        private QuizParams quizParams;
        private Quiz quiz;

        public CreateQuizCategoryViewModel(QuizParams quizParams)
        {
            this.quizParams = quizParams;
            this.quiz = quizParams.Quiz;

            if (quiz.QuizCategory != null)
                SelectedItem = quiz.QuizCategory;
        }

//        public string[] QuizCategories = CategoryHelper

        private Object selectedItem;
        public Object SelectedItem
        {
            get { return selectedItem; }
            set { Set(ref selectedItem, value); }
        }

        private TextBoxHelper headerText = new TextBoxHelper();

        public string[] QuizCategories =
        {
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

        public TextBoxHelper HeaderText
        {
            get { return headerText; }
            set { Set(ref headerText, value); }
        }

        public void ClickedNext(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
                quiz.QuizCategory = SelectedItem.ToString();

            NavigationService.Navigate(typeof(CreateQuestion), quizParams);
        }

        public void ClickedBack()
        {
            if (SelectedItem != null)
                quiz.QuizCategory = SelectedItem.ToString();

            NavigationService.Navigate(typeof(CreateQuizNamePage), quizParams);
        }
    }
}
