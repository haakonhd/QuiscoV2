using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Quisco.Helpers;
using Quisco.ViewModels.Create;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quisco.Views.Create
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateQuestion : Page
    {

        public CreateQuestionViewModel ViewModel { get; set; }
        private QuizParams quizParams;

        public CreateQuestion()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            quizParams = (QuizParams)e.Parameter; // get parameter
            Frame rootFrame = Window.Current.Content as Frame;
            if (quizParams != null)
            {
                quizParams.CurrentFrame = rootFrame;
                ViewModel = new CreateQuestionViewModel();
            }

            ViewModel.Initialize(quizParams);
            DataContext = ViewModel;
        }


    }
}
