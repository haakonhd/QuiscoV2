using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Quisco.Core.Helpers;
using Quisco.Core.Services;
using Quisco.DataAccess;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views;
using Quisco.Views.Create;

namespace Quisco.ViewModels.Create
{
    public class CreateQuestionViewModel : BindableBase, INotifyPropertyChanged
    {
        private Quiz quiz;
        private Question question;
        private QuizParams quizParams;
        private int questionToHandle;
        private string selectedAnswerNumber;

        public ICommand AddQuizCommand { get; set; }
        private QuizRequest quizzesDataAccess = new QuizRequest();
        public ObservableCollection<Quiz> Quizzes { get; set; } = new ObservableCollection<Quiz>();

        private IdentityService IdentityService => Singleton<IdentityService>.Instance;

        public CreateQuestionViewModel()
        {
        }

        public async void AddQuiz()
        {
            if (QuizIsValid(quiz))
            {
                bool userConfirmed = await DisplayAreYouSureDialog().ConfigureAwait(true);
                if (userConfirmed)
                {
                    //updating quiz and question's ICollections and deleting their ILists.
                    quiz.Questions.Clear();
                    foreach (Question q in quiz.QuestionList) quiz.Questions.Add(q);

                    quiz.QuestionList = null;
                    foreach (var question in quiz.Questions)
                    {
                        question.Answers.Clear();
                        foreach (var answer in question.AnswersList) question.Answers.Add(answer);
                        question.AnswersList = null;
                    }

                    if (IdentityService.IsLoggedIn())
                        quiz.UserIdHash = HashGenerator.ComputeSha256Hash(IdentityService.GetAccountIdentifier());

                    if (await quizzesDataAccess.AddQuizToDbAsync(quiz).ConfigureAwait(true))
                    {
                        Quizzes.Add(quiz);
                        NavigationService.Navigate(typeof(MainPage));
                    }
                    else
                    {
                        DisplayErrorMessageAsync("There was an unknown error adding your quiz to the database.");
                    }
                }
            }
        }

        public void Initialize(QuizParams quizParams)
        {
            this.quizParams = quizParams;
            this.quiz = quizParams.Quiz;
            this.questionToHandle = quizParams.QuestionToHandle;
            question = quiz.QuestionList.ElementAtOrDefault(questionToHandle - 1);
            BuildViews();
        }

        public void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(CreateQuizCategory), quizParams);
        }

        //One way bindings:

        private string quizNameHeader;
        public string QuizNameHeader
        {
            get => quizNameHeader;
            set => SetProperty(ref quizNameHeader, value);
        }

        private string questionNumberText;
        public string QuestionNumberText
        {
            get => questionNumberText;
            set => SetProperty(ref questionNumberText, value);
        }

        public ObservableCollection<Question> questionsObservableCollection = new ObservableCollection<Question>();
        public ObservableCollection<Question> QuestionsObservableCollection
        {
            get => questionsObservableCollection;
            set => SetProperty(ref questionsObservableCollection, value);
        }

        private SolidColorBrush answer1BorderBrush = new SolidColorBrush();
        public SolidColorBrush Answer1BorderBrush
        {
            get => answer1BorderBrush;
            set => SetProperty(ref answer1BorderBrush, value);
        }

        private SolidColorBrush answer2BorderBrush = new SolidColorBrush();
        public SolidColorBrush Answer2BorderBrush
        {
            get => answer2BorderBrush;
            set => SetProperty(ref answer2BorderBrush, value);
        }

        private SolidColorBrush answer3BorderBrush = new SolidColorBrush();
        public SolidColorBrush Answer3BorderBrush
        {
            get => answer3BorderBrush;
            set => SetProperty(ref answer3BorderBrush, value);
        }

        private SolidColorBrush answer4BorderBrush = new SolidColorBrush();
        public SolidColorBrush Answer4BorderBrush
        {
            get => answer4BorderBrush;
            set => SetProperty(ref answer4BorderBrush, value);
        }

        //Two way bindings:

        //input texts

        private string questionInputText;
        public string QuestionInputText
        {
            get => questionInputText;
            set => SetProperty(ref questionInputText, value);
        }

        private TextBoxHelper inputText1 = new TextBoxHelper();

        public TextBoxHelper Answer1InputText
        {
            get => inputText1;
            set => SetProperty(ref inputText1, value);
        }

        private TextBoxHelper inputText2 = new TextBoxHelper();
        public TextBoxHelper Answer2InputText
        {
            get => inputText2;
            set => SetProperty(ref inputText2, value);
        }

        private TextBoxHelper inputText3 = new TextBoxHelper();
        public TextBoxHelper Answer3InputText
        {
            get => inputText3;
            set => SetProperty(ref inputText3, value);
        }

        private TextBoxHelper inputText4 = new TextBoxHelper();
        public TextBoxHelper Answer4InputText
        {
            get => inputText4;
            set => SetProperty(ref inputText4, value);
        }

        // radio buttons

        private RadioButtonHelper radioButton1 = new RadioButtonHelper();
        public RadioButtonHelper RadioButton1
        {
            get => radioButton1;
            set => SetProperty(ref radioButton1, value);
        }

        private RadioButtonHelper radioButton2 = new RadioButtonHelper();
        public RadioButtonHelper RadioButton2
        {
            get => radioButton2;
            set => SetProperty(ref radioButton2, value);
        }

        private RadioButtonHelper radioButton3 = new RadioButtonHelper();
        public RadioButtonHelper RadioButton3
        {
            get => radioButton3;
            set => SetProperty(ref radioButton3, value);
        }

        private RadioButtonHelper radioButton4 = new RadioButtonHelper();
        public RadioButtonHelper RadioButton4
        {
            get => radioButton4;
            set => SetProperty(ref radioButton4, value);
        }



        public void ClickedItem(object sender, ItemClickEventArgs e)
        {
            var clickedQuestion = (Question)e.ClickedItem;
            quizParams.QuestionToHandle = clickedQuestion.QuestionNumber;

            Initialize(quizParams);
        }


        private void FillInputs()
        {
            QuestionInputText = "";
            question = quiz.QuestionList.ElementAtOrDefault(questionToHandle - 1);

            // question to be handled has already been created (editing)
            if (question != null)
            {
                QuestionInputText = question.QuestionText;

                Answer1InputText.Text = question.AnswersList.ElementAtOrDefault(0)?.AnswerText;
                Answer2InputText.Text = question.AnswersList.ElementAtOrDefault(1)?.AnswerText;
                Answer3InputText.Text = question.AnswersList.ElementAtOrDefault(2)?.AnswerText;
                Answer4InputText.Text = question.AnswersList.ElementAtOrDefault(3)?.AnswerText;

                OnAnswer1Changed(null, null);
                SetCorrectAnswer();
            }
        }

        private void SetCorrectAnswer()
        {
            if (question.AnswersList.Count < 1) return;
            int correctAnswerNumber = question.CorrectAnswerNumber;
            if (correctAnswerNumber == 1 && RadioButton1.IsEnabled) RadioButton1.IsChecked = true;
            if (correctAnswerNumber == 2 && RadioButton1.IsEnabled) RadioButton2.IsChecked = true;
            if (correctAnswerNumber == 3 && RadioButton1.IsEnabled) RadioButton3.IsChecked = true;
            if (correctAnswerNumber == 4 && RadioButton1.IsEnabled) RadioButton4.IsChecked = true;

        }



        public void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RefreshTextBoxColors();
        }

        private void SetAnswersToQuestion()
        {
            //answer1
            string a1 = Answer1InputText.Text;
            if (RadioButton1.IsChecked) question.CorrectAnswerNumber = 1;
            if (question.AnswersList.Count > 0) //answer exists in list
                question.AnswersList[0].AnswerText = a1; //edit this index in answer list
            else question.AnswersList.Add(new Answer(a1, question, 1, question.QuestionId)); //adding new answer

            //answer2
            string a2 = Answer2InputText.Text;
            if (RadioButton2.IsChecked) question.CorrectAnswerNumber = 2;
            if (question.AnswersList.Count > 1)
                question.AnswersList[1].AnswerText = a2;
            else question.AnswersList.Add(new Answer(a2, question, 2, question.QuestionId));

            //answer3.
            string a3 = Answer3InputText.Text;
            if (RadioButton3.IsChecked) question.CorrectAnswerNumber = 3;
            if (!string.IsNullOrEmpty(a3))
            {
                if (question.AnswersList.Count > 2)
                    question.AnswersList[2].AnswerText = a3;
                else question.AnswersList.Add(new Answer(a3, question, 3, question.QuestionId));
            }
            else if (question.AnswersList.Count > 2)//annswer3input is empty.
            {
                question.AnswersList[2].ToBeDeleted = true; //removes answer if there exists one
                question.AnswersList[2].AnswerText = ""; //resets answer text

            }

            //answer4
            string a4 = Answer4InputText.Text;
            if (RadioButton4.IsChecked) question.CorrectAnswerNumber = 4;
            if (!string.IsNullOrEmpty(a4))
            {
                if (question.AnswersList.Count > 3)
                    question.AnswersList[3].AnswerText = a4;
                else question.AnswersList.Add(new Answer(a4, question, 4, question.QuestionId));
            }
            else if (question.AnswersList.Count > 3)
            {
                question.AnswersList[3].ToBeDeleted = true;
                question.AnswersList[3].AnswerText = "";
            }
        }

        public void AddQuestion(object sender, RoutedEventArgs e)
        {
            bool newQuestion;
            // if creating a new question, the value will be null
            question = quiz.QuestionList.ElementAtOrDefault(questionToHandle - 1);

            if (question == null)
            {
                question = new Question();
                question.BelongingQuizId = quiz.QuizId;
                newQuestion = true;
            }
            else newQuestion = false;

            if (InputsAreValid())
            {
                question.QuestionText = QuestionInputText;
                question.QuestionNumber = questionToHandle;
                SetAnswersToQuestion();
            }
            else
            {
                return; // InputsAreValid() will handle errors
            }

            if (newQuestion) quiz.QuestionList.Add(question);
            else quiz.QuestionList[questionToHandle - 1] = question;

            quizParams.QuestionToHandle = quiz.QuestionList.Count + 1;
            quiz.QuizName = QuizNameHeader;
            quizParams.Quiz = quiz;
            Initialize(quizParams);
        }


        private bool InputsAreValid()
        {
            bool valid;
            valid = QuestionTextIsValid() && AnswersAreValid() && AnswerIsSelected();
            if (!valid)
            {
                if (!QuestionTextIsValid()) DisplayErrorMessageAsync("Error: Question text needs at least 5 characters.");
                else if (!AnswersAreValid()) DisplayErrorMessageAsync("Error: At least 2 answers must be entered.");
                else DisplayErrorMessageAsync("Error: Please select a correct answer.");
            }

            return valid;
        }

        private bool QuestionTextIsValid()
        {
            return QuestionInputText.Length > 5;
        }

        private bool AnswersAreValid()
        {
            if (string.IsNullOrEmpty(Answer1InputText.Text)) return false;
            if (string.IsNullOrEmpty(Answer2InputText.Text)) return false;
            return true;
        }

        private bool AnswerIsSelected()
        {
            if (RadioButton1.IsChecked == true) return true;
            if (RadioButton2.IsChecked == true) return true;
            if (RadioButton3.IsChecked == true) return true;
            if (RadioButton4.IsChecked == true) return true;
            return false;
        }

        private void BuildViews()
        {
            RefreshAnswerInputs();
            FillInputs();

            QuizNameHeader = quiz.QuizName;
            QuestionNumberText = "Edit question " + questionToHandle;
            //for some weird reason the listview will only update half the time I do changes, unless I do this
            QuestionsObservableCollection = new ObservableCollection<Question>();
            foreach (Question q in quiz.QuestionList) QuestionsObservableCollection.Add(q);

            RefreshTextBoxColors();
        }


        private int GetSelectedAnswer()
        {
            if (RadioButton1.IsChecked) return 1;
            if (RadioButton2.IsChecked) return 2;
            if (RadioButton3.IsChecked) return 3;
            if (RadioButton4.IsChecked) return 4;
            return 0;
        }

        private void RefreshTextBoxColors()
        {
            Answer1BorderBrush = default(SolidColorBrush);
            Answer2BorderBrush = default(SolidColorBrush);
            Answer3BorderBrush = default(SolidColorBrush);
            Answer4BorderBrush = default(SolidColorBrush);


            if (!string.IsNullOrEmpty(Answer1InputText.Text))
                Answer1BorderBrush = new SolidColorBrush(Colors.Red);
            if (!string.IsNullOrEmpty(Answer2InputText.Text))
                Answer2BorderBrush = new SolidColorBrush(Colors.Red);
            if (!string.IsNullOrEmpty(Answer3InputText.Text))
                Answer3BorderBrush = new SolidColorBrush(Colors.Red);
            if (!string.IsNullOrEmpty(Answer4InputText.Text))
                Answer4BorderBrush = new SolidColorBrush(Colors.Red);

            int selectedRadioButton = GetSelectedAnswer();
            switch (selectedRadioButton)
            {
                case 1:
                    Answer1BorderBrush = new SolidColorBrush(Colors.PaleGreen);
                    break;
                case 2:
                    Answer2BorderBrush = new SolidColorBrush(Colors.PaleGreen);
                    break;
                case 3:
                    Answer3BorderBrush = new SolidColorBrush(Colors.PaleGreen);
                    break;
                case 4:
                    Answer4BorderBrush = new SolidColorBrush(Colors.PaleGreen);
                    break;
            }
        }

        public void RefreshAnswerInputs()
        {
            Answer1InputText.Text = "";
            Answer2InputText.Text = "";
            Answer3InputText.Text = "";
            Answer4InputText.Text = "";

            Answer1InputText.IsEnabled = true;
            RadioButton1.IsEnabled = false;
            OnAnswer1Changed(null, null);
        }

        public void OnAnswer1Changed(object sender, TextChangedEventArgs e)
        {
            // ANSWER 1
            if (!string.IsNullOrEmpty(Answer1InputText.Text))
            {
                Answer2InputText.IsEnabled = true;
                RadioButton1.IsEnabled = true;
            }
            else
            {
                RadioButton1.IsChecked = false;
                Answer2InputText.IsEnabled = false;
            }
            OnAnswer2Changed(null, null);
        }

        public void OnAnswer2Changed(object sender, TextChangedEventArgs e)
        {
            // ANSWER 2
            if (!string.IsNullOrEmpty(Answer2InputText.Text) && Answer2InputText.IsEnabled)
            {
                Answer3InputText.IsEnabled = true;
                RadioButton2.IsEnabled = true;
            }
            else
            {
                RadioButton2.IsChecked = false;
                RadioButton2.IsEnabled = false;
                Answer3InputText.IsEnabled = false;
            }
            OnAnswer3Changed(null, null);
        }

        public void OnAnswer3Changed(object sender, TextChangedEventArgs e)
        {
            // ANSWER 3
            if (!string.IsNullOrEmpty(Answer3InputText.Text) && Answer3InputText.IsEnabled)
            {
                Answer4InputText.IsEnabled = true;
                RadioButton3.IsEnabled = true;
            }
            else
            {
                RadioButton3.IsChecked = false;
                RadioButton3.IsEnabled = false;
                Answer4InputText.IsEnabled = false;
            }
            OnAnswer4Changed(null, null);
        }

        public void OnAnswer4Changed(object sender, TextChangedEventArgs e)
        {
            // ANSWER 4
            if (!string.IsNullOrEmpty(Answer4InputText.Text) && Answer4InputText.IsEnabled)
            {
                RadioButton4.IsEnabled = true;
            }
            else
            {
                RadioButton4.IsChecked = false;
                RadioButton4.IsEnabled = false;
            }
            RefreshTextBoxColors();
        }

        private bool QuizIsValid(Quiz quiz)
        {
            bool isValid = true;
            string errorMessage = "";
            if (quiz == null) return false;
            if (quiz.QuestionList.Count < 3)
            {
                isValid = false;
                errorMessage = "Error: You need at least 3 questions.";
            }

            if (quiz.QuestionList.Count > 20)
            {
                isValid = false;
                errorMessage = "You can have maximum 20 questions";
            }
            if (!isValid) DisplayErrorMessageAsync(errorMessage);
            return isValid;
        }

        private async void DisplayErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }

        private async Task<bool> DisplayAreYouSureDialog()
        {
            ContentDialog fileDialog = new ContentDialog
            {
                Title = "All ready to go.",
                Content = "Are you sure you're done editing the quiz?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await fileDialog.ShowAsync();

            return result == ContentDialogResult.Primary; // true if clicked "yes", false if closed
        }

    }
}
