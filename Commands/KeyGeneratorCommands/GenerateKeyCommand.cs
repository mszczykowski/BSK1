using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Commands.KeyGeneratorCommands
{
    internal class GenerateKeyCommand : KeyGeneratorCommandBase
    {
        public GenerateKeyCommand(AlgorithmsFormViewModel algorithmsFormViewModel) : base(algorithmsFormViewModel)
        {
        }
        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter) && !_viewModel.AlgorithmViewModel.KeyGenerator.IsRunning;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.ValidateKey();
            if (!_viewModel.AlgorithmViewModel.IsKeyValid(_viewModel.KeyInput)) return;

            _viewModel.AlgorithmViewModel.KeyGenerator.StartGeneratingKey();
        }
    }
}
