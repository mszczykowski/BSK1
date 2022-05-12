using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Commands
{
    internal class RandomiseKeyCommand : CommandBase
    {
        private AlgorithmsFormViewModel _viewModel;
        public RandomiseKeyCommand(AlgorithmsFormViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.KeyInput = _viewModel.AlgorithmViewModel.KeyRandomiser.GenerateRandomKey();
        }
    }
}
