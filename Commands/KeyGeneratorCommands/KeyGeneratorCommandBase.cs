using BSK1.Algorithms.KeyGenerators;
using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Commands.KeyGeneratorCommands
{
    internal abstract class KeyGeneratorCommandBase : CommandBase
    {
        protected readonly AlgorithmsFormViewModel _algorithmsFormViewModel;
        public KeyGeneratorCommandBase(AlgorithmsFormViewModel algorithmsFormViewModel)
        {
            _algorithmsFormViewModel = algorithmsFormViewModel;

            _algorithmsFormViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AlgorithmViewModel)) OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _algorithmsFormViewModel.AlgorithmViewModel?.KeyGenerator != null && base.CanExecute(parameter);
        }
    }
}
