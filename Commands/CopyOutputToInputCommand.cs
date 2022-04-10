using BSK1.Services;
using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Commands
{
    internal class CopyOutputToInputCommand : CommandBase
    {
        private AlgorithmsFormViewModel _viewModel;

        public CopyOutputToInputCommand(AlgorithmsFormViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return (!String.IsNullOrEmpty(_viewModel.OutputText) || _viewModel.OutputFileLinkVisible)
                && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if(!String.IsNullOrEmpty(_viewModel.OutputText)) 
                _viewModel.InputText = _viewModel.OutputText;

            if (_viewModel.OutputFileLinkVisible)
                _viewModel.FilePath = _viewModel.OutputFilePath;

            if(_viewModel.GeneratedKey != null) _viewModel.KeyInput = _viewModel.GeneratedKey;
        }
    }
}
