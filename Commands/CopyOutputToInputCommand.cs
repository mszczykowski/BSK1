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
        private ModuleBaseViewModel _moduleBaseViewModel;

        public CopyOutputToInputCommand(ModuleBaseViewModel moduleBaseViewModel)
        {
            _moduleBaseViewModel = moduleBaseViewModel;

            _moduleBaseViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return (!String.IsNullOrEmpty(_moduleBaseViewModel.OutputText) || _moduleBaseViewModel.OutputFileLinkVisible)
                && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if(!String.IsNullOrEmpty(_moduleBaseViewModel.OutputText)) 
                _moduleBaseViewModel.InputText = _moduleBaseViewModel.OutputText;
            if (_moduleBaseViewModel.OutputFileLinkVisible)
                _moduleBaseViewModel.FilePath = FileService.outputFilePath;
        }
    }
}
