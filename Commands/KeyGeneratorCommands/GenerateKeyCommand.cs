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
            return base.CanExecute(parameter) && !_algorithmsFormViewModel.AlgorithmViewModel.KeyGenerator.IsRunning;
        }

        public override void Execute(object? parameter)
        {
            _algorithmsFormViewModel.ValidateKey();
            if (!_algorithmsFormViewModel.AlgorithmViewModel.IsKeyValid(_algorithmsFormViewModel.KeyInput)) return;


            _algorithmsFormViewModel.AlgorithmViewModel.KeyGenerator.ClearKey();
            _algorithmsFormViewModel.AlgorithmViewModel.KeyGenerator.StartGeneratingKey();
        }
    }
}
