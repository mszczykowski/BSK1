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

        public override void Execute(object? parameter)
        {
            _algorithmsFormViewModel.ValidateKey();
            if (_algorithmsFormViewModel.AlgorithmViewModel.IsKeyValid(_algorithmsFormViewModel.KeyInput))
                _algorithmsFormViewModel.AlgorithmViewModel.KeyGenerator.StartGeneratingKey();
        }
    }
}
