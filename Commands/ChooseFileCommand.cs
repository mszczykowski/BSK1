using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using BSK1.Enums;

namespace BSK1.Commands
{
    internal class ChooseFileCommand : CommandBase
    {
        private AlgorithmsFormViewModel _viewModel;

        public ChooseFileCommand(AlgorithmsFormViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            string filter = "";

            switch(_viewModel.AlgorithmViewModel.AlgorithmType)
            {
                case AlgorithmType.Text:
                    filter = "Pliki tekstowe .txt|*.txt";
                    break;
                case AlgorithmType.Binary:
                    filter = "Wszystkie pliki|*.*";
                    break;
            }
            
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = filter,
                RestoreDirectory = true,
                Title = "Wybierz plik",
                DefaultExt = "txt",
                CheckFileExists = true,
                CheckPathExists = true,
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _viewModel.FilePath = openFileDialog.FileName;

                _viewModel.IsInputFile = true;
            }
        }
    }
}
