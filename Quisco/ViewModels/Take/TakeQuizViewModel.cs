using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views.Take;

namespace Quisco.ViewModels.Take
{
    public class TakeQuizViewModel : Observable
    {
        /// <summary>Parameters used to store information relevant to the quiz solving</summary>
        private QuizCompletionParams quizCompletionParams;
        /// <summary>The quiz</summary>
        private Quiz quiz;
        /// <summary>The question</summary>
        private Question question;
        /// <summary>The answers</summary>
        private IList<Answer> answers;
        /// <summary>The answer number chosen by the user</summary>
        private int chosenAnswer;
        /// <summary>The correct answer number</summary>
        private int correctAnswer;

        /// <summary>Path for the blue button background</summary>
        BitmapImage blueButtonSource = new BitmapImage(new Uri("ms-appx:///Assets/button_blue_bg.png"));
        /// <summary>Path for the red button background</summary>
        BitmapImage redButtonSource = new BitmapImage(new Uri("ms-appx:///Assets/button_red_bg.png"));
        /// <summary>Path for the grey button background</summary>
        BitmapImage greyButtonSource = new BitmapImage(new Uri("ms-appx:///Assets/button_grey_bg.png"));
        /// <summary>Path for the green button background</summary>
        BitmapImage greenButtonSource = new BitmapImage(new Uri("ms-appx:///Assets/button_green_bg.png"));



        //Bindings:


        private string quizNameTextBlock;
        /// <summary>Binding value: Gets or sets the quiz name text block.</summary>
        /// <value>Quiz name</value>
        public string QuizNameTextBlock
        {
            get => quizNameTextBlock;
            set => Set(ref quizNameTextBlock, value); 
        }

        private string questionTextBlock;
        /// <summary>Binding value: Gets or sets the question text block.</summary>
        /// <value>Question text</value>
        public string QuestionTextBlock
        {
            get => questionTextBlock;
            set => Set(ref questionTextBlock, value); 
        }

        private string correctResultTextBlock;
        /// <summary>Binding value: Gets or sets the correct result text block.</summary>
        /// <value>"Correct" or "Wrong" message </value>
        public string CorrectResultTextBlock
        {
            get => correctResultTextBlock;
            set => Set(ref correctResultTextBlock, value); 
        }

        private string answer1Text;
        /// <summary>Binding value: Gets or sets the answer1 text.</summary>
        /// <value>Answer 1 button text. </value>
        public string Answer1Text
        {
            get => answer1Text;
            set => Set(ref answer1Text, value); 
        }

        private string answer2Text;
        /// <summary>Binding value: Gets or sets the answer2 text.</summary>
        /// <value>Answer 2 button text.</value>
        public string Answer2Text
        {
            get => answer2Text;
            set => Set(ref answer2Text, value); 
        }

        private string answer3Text;
        /// <summary>Binding value: Gets or sets the answer3 text.</summary>
        /// <value>Answer 3 button text.</value>
        public string Answer3Text
        {
            get => answer3Text;
            set => Set(ref answer3Text, value); 
        }

        private string answer4Text;
        /// <summary>Binding value: Gets or sets the answer4 text.</summary>
        /// <value>Answer 4 button text</value>
        public string Answer4Text
        {
            get => answer4Text;
            set => Set(ref answer4Text, value); 
        }

        private bool showAnswerIsVisible;
        /// <summary>Binding value: Gets or sets a value indicating whether the ShowAnswersButton is visible.</summary>
        /// <value>
        /// <c>true</c> if [ShowAnswerButton is visible]; otherwise, <c>false</c>.</value>
        public bool ShowAnswerIsVisible
        {
            get => showAnswerIsVisible; 
            set => Set(ref showAnswerIsVisible, value); 
        }

        private bool nextQuestionIsVisible;
        /// <summary>Binding value: Gets or sets a value indicating whether the nextQuestionButton is visible.</summary>
        /// <value><c>true</c> if [nextQuestionButton is visible]; otherwise, <c>false</c>.</value>
        public bool NextQuestionIsVisible
        {
            get => nextQuestionIsVisible;
            set => Set(ref nextQuestionIsVisible, value);
        }

        private bool answer3ButtonVisibility;
        /// <summary>Gets or sets a value indicating answer3 button visibility.</summary>
        /// <value>
        ///   <c>true</c> if [answer3 button is visible]; otherwise, <c>false</c>.</value>
        public bool Answer3ButtonVisibility
        {
            get => answer3ButtonVisibility;
            set => Set(ref answer3ButtonVisibility, value);
        }

        private bool answer4ButtonVisibility;
        /// <summary>Gets or sets a value indicating answer4 button visibility.</summary>
        /// <value>
        ///   <c>true</c> if [answer4 button is visible]; otherwise, <c>false</c>.</value>
        public bool Answer4ButtonVisibility
        {
            get => answer4ButtonVisibility;
            set => Set(ref answer4ButtonVisibility, value);
        }

        private bool answersAreEnabled;
        /// <summary>Gets or sets a value indicating whether [answers are enabled].</summary>
        /// <value>
        ///   <c>true</c> if [answers are enabled]; otherwise, <c>false</c>.</value>
        public bool AnswersAreEnabled
        {
            get => answersAreEnabled;
            set => Set(ref answersAreEnabled, value);
        }

        private BitmapImage answer1ImageSource;
        /// <summary>Binding value: Gets or sets the answer1 background image source.</summary>
        /// <value>The answer1 image source.</value>
        public BitmapImage Answer1ImageSource
        {
            get => answer1ImageSource;
            set => Set(ref answer1ImageSource, value);
        }

        private BitmapImage answer2ImageSource;
        /// <summary>Binding value: Gets or sets the answer2 background image source.</summary>
        /// <value>The answer2 image source.</value>
        public BitmapImage Answer2ImageSource
        {
            get => answer2ImageSource;
            set => Set(ref answer2ImageSource, value);
        }

        private BitmapImage answer3ImageSource;
        /// <summary>Binding value: Gets or sets the answer3 background image source.</summary>
        /// <value>The answer3 image source.</value>
        public BitmapImage Answer3ImageSource
        {
            get => answer3ImageSource;
            set => Set(ref answer3ImageSource, value);
        }

        private BitmapImage answer4ImageSource;
        /// <summary>Binding value: Gets or sets the answer4 background image source.</summary>
        /// <value>The answer4 image source.</value>
        public BitmapImage Answer4ImageSource
        {
            get => answer4ImageSource;
            set => Set(ref answer4ImageSource, value);
        }

        /// <summary>Initializes a new instance of the <see cref="TakeQuizViewModel"/> class.</summary>
        public TakeQuizViewModel()
        {
            
        }



        /// <summary>Initiates a new question round based on the quiz completion parameters</summary>
        /// <param name="quizCompletionParams">Parameters used to store information relevant to the quiz solving</param>
        public void Initialize(QuizCompletionParams quizCompletionParams)
        {
            this.quizCompletionParams = quizCompletionParams;
            quiz = quizCompletionParams.Quiz;
            question = quiz.QuestionList[quizCompletionParams.QuestionToHandle - 1];
            answers = question.AnswersList;

            chosenAnswer = 0;
            correctAnswer = question.CorrectAnswerNumber;

            ResetLayout();
            InitializeLayout();

        }

        /// <summary>Resets the layout.</summary>
        private void ResetLayout()
        {
            ShowAnswerIsVisible = true;
            NextQuestionIsVisible = false;
            Answer3ButtonVisibility = false;
            Answer4ButtonVisibility = false;
            ResetButtonColors();
            AnswersAreEnabled = true;
            CorrectResultTextBlock = "";
        }

        /// <summary>Updates binding values based on the current question</summary>
        private void InitializeLayout()
        {
            QuizNameTextBlock = quiz.QuizName;
            QuestionTextBlock = question.QuestionText;

            // there isnt a guaranteed order of the answers but we want them placed ordered in the grid

            foreach (var a in answers)  if (a.AnswerNumber == 1)  Answer1Text = a.AnswerText;

            foreach (var a in answers)  if (a.AnswerNumber == 2)  Answer2Text = a.AnswerText;

            if (answers.Count > 2)
            {
                Answer3ButtonVisibility = true;
                foreach (var a in answers)  if (a.AnswerNumber == 3)  Answer3Text = a.AnswerText;
            }
            if (answers.Count > 3)
            {
                Answer4ButtonVisibility = true;
                foreach (var a in answers)  if (a.AnswerNumber == 4)  Answer4Text = a.AnswerText;
            }
        }

        /// <summary>Resets the button colors.</summary>
        private void ResetButtonColors()
        {
            Answer1ImageSource = blueButtonSource;
            Answer2ImageSource = blueButtonSource;
            Answer3ImageSource = blueButtonSource;
            Answer4ImageSource = blueButtonSource;
        }

        /// <summary>Adjusts layout according to the correct answer and answer selected by user.</summary>
        private void SetCorrectAnswerInLayout()
        {
            if (correctAnswer == 1)
            {
                Answer1ImageSource = greenButtonSource;
                if (chosenAnswer != 1) SetUserPickedWrongAnswer();
                else CorrectResultTextBlock = "Correct!";
            }

            if (correctAnswer == 2)
            {
                Answer2ImageSource = greenButtonSource;
                if(chosenAnswer != 2) SetUserPickedWrongAnswer();
                else CorrectResultTextBlock = "Correct!";
            }

            if (correctAnswer == 3)
            {
                Answer3ImageSource = greenButtonSource;
                if(chosenAnswer != 3) SetUserPickedWrongAnswer();
                else CorrectResultTextBlock = "Correct!";
            }

            if (correctAnswer == 4)
            {
                Answer4ImageSource = greenButtonSource;
                if(chosenAnswer != 4) SetUserPickedWrongAnswer();
                else CorrectResultTextBlock = "Correct!";
            }
        }

        /// <summary>The user picked the wrong answer. Sets color to red</summary>
        private void SetUserPickedWrongAnswer()
        {
            CorrectResultTextBlock = "Wrong!";
            if (chosenAnswer == 1) Answer1ImageSource = redButtonSource;
            if (chosenAnswer == 2) Answer2ImageSource = redButtonSource;
            if (chosenAnswer == 3) Answer3ImageSource = redButtonSource;
            if (chosenAnswer == 4) Answer4ImageSource = redButtonSource;
        }

        /// <summary>Displays an error message asynchronous.</summary>
        /// <param name="errorMessage">The error message.</param>
        private async void DisplayErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }

        //Button events

        /// <summary>  Button event: Shows correct and wrong answers. Displays error if no answer is selected.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
        public void ShowAnswerButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (chosenAnswer == 0)
            {
                DisplayErrorMessageAsync("Please click an answer");
                return;
            }

            AnswersAreEnabled = false;
            SetCorrectAnswerInLayout();
            if (chosenAnswer == correctAnswer)
                quizCompletionParams.correctAnswers++;
            else
                quizCompletionParams.incorrectAnswers++;
            ShowAnswerIsVisible = false;
            NextQuestionIsVisible = true;
        }

        /// <summary>Button event: Initiates a new question round.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
        public void NextQuestionButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (quizCompletionParams.QuestionToHandle == quiz.QuestionList.Count)
            {
                NavigationService.Navigate(typeof(QuizComplete), quizCompletionParams);
                return;
            }
            quizCompletionParams.Quiz = quiz;
            quizCompletionParams.QuestionToHandle++;
            Initialize(quizCompletionParams);
        }

        /// <summary>Button event: Answer1Button is pressed. Updates button layout accordingly</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
        public void Answer1ButtonPressed(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            chosenAnswer = 1;
            ResetButtonColors();
            Answer1ImageSource = greyButtonSource;
        }

        /// <summary>Button event: Answer2Button is pressed. Updates button layout accordingly</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
        public void Answer2ButtonPressed(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            chosenAnswer = 2;
            ResetButtonColors();
            Answer2ImageSource = greyButtonSource;
        }

        /// <summary>Button event: Answer3Button is pressed. Updates button layout accordingly</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
        public void Answer3ButtonPressed(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            chosenAnswer = 3;
            ResetButtonColors();
            Answer3ImageSource = greyButtonSource;
        }

        /// <summary>Button event: Answer4Button is pressed. Updates button layout accordingly</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
        public void Answer4ButtonPressed(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            chosenAnswer = 4;
            ResetButtonColors();
            Answer4ImageSource = greyButtonSource;
        }
    }
}
