using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK1.Enums;
using BSK1.Services;
using BSK1.ViewModels;

namespace BSK1.Commands
{
    internal class OpenOutputFileCommand : CommandBase
    {
        private readonly AlgorithmsFormViewModel _viewModel;
        public OpenOutputFileCommand(AlgorithmsFormViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        public override void Execute(object? parameter)
        {
            switch(_viewModel.AlgorithmViewModel.AlgorithmType)
            {
                case AlgorithmType.Text:
                    Process.Start("notepad.exe", FileService.outputFilePath);
                    break;
                case AlgorithmType.Binary:
                    Process.Start("explorer.exe", FileService.outputFolderPath);
                    break;
            }
            
            
        }
    }
}
