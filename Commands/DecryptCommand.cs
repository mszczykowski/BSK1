using BSK1.Algorithms;
using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Commands
{
    internal class DecryptCommand : EncryptDecryptCommandBase
    {
        public DecryptCommand(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            base.Execute(parameter);

            SetOutput(_viewModel.AlgorithmViewModel.Algorithm.Decrypt(input));
        }
    }
}
