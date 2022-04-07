using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Commands.KeyGeneratorCommands
{
    internal class ClearKeyCommand : KeyGeneratorCommandBase
    {
        public ClearKeyCommand(AlgorithmsFormViewModel algorithmsFormViewModel) : base(algorithmsFormViewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            _algorithmsFormViewModel.AlgorithmViewModel.KeyGenerator.ClearKey();
        }
    }
}
