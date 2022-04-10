using BSK1.Algorithms;
using BSK1.Algorithms.BinaryAlgorithms;
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
            if(_viewModel.AlgorithmViewModel.Algorithm is StreamCipher)
            {
                var steramChiper = _viewModel.AlgorithmViewModel.Algorithm as StreamCipher;

                _viewModel.ValidateSeed();
                _viewModel.ValidateKey();

                if (!steramChiper.IsSeedValid(_viewModel.Seed)) return;
            }
            
            base.Execute(parameter);
        }

        public override async Task Translate()
        {
            outShouldBeBinary = false;
            await Task.Run(() =>
            {
                output = _viewModel.AlgorithmViewModel.Algorithm.Decrypt(input);
            });
        }
    }
}
