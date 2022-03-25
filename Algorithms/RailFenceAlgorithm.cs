using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK1.ViewModels;

namespace BSK1.Algorithms
{
    internal class RailFenceAlgorithm : Algorithm
    {
        public RailFenceAlgorithm(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Decrypt(string input)
        {
            return input + "chuj" + _viewModel.Key;
        }

        public override string Encrypt(string input)
        {
            return input + "dupa" + _viewModel.Key;
        }

        public override bool IsKeyValid(string key)
        {
            int x;
            return (Int32.TryParse(key, out x) && x > 0);
        }
    }
}
