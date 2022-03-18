using BSK1.Commands;
using BSK1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BSK1.ViewModels
{
    internal class MainMenuViewModel : ViewModelBase
    {
        public ICommand ExitCommand { get; }

        public ICommand NavigateToRailFenceCommand { get; }

        public MainMenuViewModel(NavigationService railFenceViewNavigationService)
        {
            NavigateToRailFenceCommand = new NavigateCommand(railFenceViewNavigationService);


            ExitCommand = new ExitCommand();
        }
    }
}
