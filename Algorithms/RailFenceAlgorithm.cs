using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK1.ViewModels;

namespace BSK1.Algorithms
{
    internal class RailFenceAlgorithm : IAlgorithm
    {
        private readonly RailFenceViewModel _viewModel;
        public RailFenceAlgorithm(RailFenceViewModel viewModel)
        {
            _viewModel = viewModel;
        }



        public string Decrypt(string input)
        {
            return input + "chuj" + _viewModel.N;
        }

        public string Encrypt(string input)
        {
            return input + "dupa" + _viewModel.N;
        }
    }
}
