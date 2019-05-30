using Quisco.Services;
using Quisco.Views.Take;

namespace Quisco.ViewModels.Take
{
    public class TakeBotOrHumanViewModel
    {
        public void Initialize()
        {

        }

        public async void TakeHumanQuizButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(TakeSelectQuiz));
        }

        public async void TakeRobotQuizButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //TODO: change this
            NavigationService.Navigate(typeof(TakeSelectQuiz));
        }
    }
}
