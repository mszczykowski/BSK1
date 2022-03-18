using BSK1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK1.Commands;

namespace BSK1.ViewModels
{
    internal class RailFenceViewModel : ModuleBaseViewModel
    {
        private int _n;

        public int N
        {
            get => _n;
            set
            {
                _n = value;
                OnPropertyChanged(nameof(N));
            }
        }

        public RailFenceViewModel(NavigationService mainMenuNavigationService) : base(mainMenuNavigationService)
        {
            //EncryptCommand = new EncryptCommand(this);
            //DecryptCommand = new DecryptCommand(this);
        }
    }
}
