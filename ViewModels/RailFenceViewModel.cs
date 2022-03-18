using BSK1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK1.Commands;
using BSK1.Algorithms;

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
            var railFenceAlgorithm = new RailFenceAlgorithm(this);

            EncryptCommand = new EncryptCommand(railFenceAlgorithm, this);
            DecryptCommand = new DecryptCommand(railFenceAlgorithm, this);
        }
    }
}
