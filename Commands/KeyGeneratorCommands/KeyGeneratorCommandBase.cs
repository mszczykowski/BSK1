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
        protected readonly AlgorithmsFormViewModel _viewModel;
        public KeyGeneratorCommandBase(AlgorithmsFormViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AlgorithmViewModel) || e.PropertyName == nameof(AlgorithmsFormViewModel.BinaryKey)
                || e.PropertyName == nameof(AlgorithmsFormViewModel.IsLoading)) OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return !_viewModel.IsLoading && _viewModel.AlgorithmViewModel?.KeyGenerator != null && base.CanExecute(parameter);
        }
    }
}
