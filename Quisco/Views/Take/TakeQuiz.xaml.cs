﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Quisco.Helpers;
using Quisco.ViewModels.Take;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quisco.Views.Take
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TakeQuiz : Page
    {
        public TakeQuizViewModel ViewModel { get; set; }
        private QuizCompletionParams quizCompletionParams;

        public TakeQuiz()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = new TakeQuizViewModel();
            quizCompletionParams = (QuizCompletionParams)e.Parameter; // get parameter

            ViewModel.Initialize(quizCompletionParams);
            DataContext = ViewModel;
        }
    }
}
