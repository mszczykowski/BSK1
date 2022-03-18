using BSK1.Algorithms;
using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Commands
{
    internal class EncryptCommand : EncryptDecryptCommandBase
    {
        public EncryptCommand(IAlgorithm algorithm, ModuleBaseViewModel viewModel) : base(algorithm, viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            base.Execute(parameter);

            SetOutput(_algorithm.Encrypt(input));
        }
    }
}
