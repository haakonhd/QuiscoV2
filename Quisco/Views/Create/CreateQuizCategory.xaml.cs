using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Quisco.Model;
using Quisco.ViewModels;
using Quisco.ViewModels.Create;
using Quisco.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quisco.Views.Create
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateQuizCategory : Page
    {
        public CreateQuizCategoryViewModel ViewModel { get; set; }
        private QuizParams quizParams;

        public CreateQuizCategory()
        {
            //            ListView sss = new ListView();
            //            Object w = sss.SelectedItem;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            quizParams = (QuizParams)e.Parameter; // get parameter
            ViewModel = new CreateQuizCategoryViewModel(quizParams);

            Frame rootFrame = Window.Current.Content as Frame;
        }

    }
}
