using Quisco.Services;
using Quisco.Views.Take;

namespace Quisco.ViewModels.Take
{
    public class TakeBotOrHumanViewModel
    {
        public void TakeHumanQuizButton()
        {
            NavigationService.Navigate(typeof(TakeSelectQuiz));
        }

        public void TakeRobotQuizButton()
        {
            NavigationService.Navigate(typeof(TakeRobotSelectParameters));
        }
    }
}
