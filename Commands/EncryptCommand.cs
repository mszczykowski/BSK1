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
        public EncryptCommand(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            base.Execute(parameter);
        }

        public override async Task Translate()
        {
            outShouldBeBinary = true;
            await Task.Run(() =>
            {
                output = _viewModel.AlgorithmViewModel.Algorithm.Encrypt(input);
            });
        }
    }
}
