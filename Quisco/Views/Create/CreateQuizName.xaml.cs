using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Quisco.Helpers;
using Quisco.ViewModels;
using Quisco.ViewModels.Create;

namespace Quisco.Views.Create
{
    public sealed partial class CreateQuizName : Page
    {
        public CreateQuizNameViewModel ViewModel { get; set; }
        private QuizParams quizParams;

        public CreateQuizName()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            quizParams = (QuizParams)e.Parameter; // get parameter
            ViewModel = new CreateQuizNameViewModel(quizParams);

            Frame rootFrame = Window.Current.Content as Frame;
        }
    }
}
