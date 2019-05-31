using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Quisco.Helpers;
using Quisco.ViewModels.Take;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quisco.Views.Take
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuizComplete : Page
    {
        public QuizCompleteViewModel ViewModel = new QuizCompleteViewModel();
        private QuizCompletionParams quizCompletionParams;

        public QuizComplete()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            quizCompletionParams = (QuizCompletionParams)e.Parameter; // get parameter

            ViewModel.Initialize(quizCompletionParams);
            DataContext = ViewModel;
        }
    }
}
