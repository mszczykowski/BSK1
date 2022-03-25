using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms
{
    internal abstract class Algorithm
    {
        protected AlgorithmsFormViewModel _viewModel;

        public Algorithm(AlgorithmsFormViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public abstract string Encrypt(string input);

        public abstract string Decrypt(string input);

        public abstract bool IsKeyValid(string key);
    }
}
