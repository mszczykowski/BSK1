using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms
{
    internal class CaesarCipherAlgorithm : Algorithm
    {
        public CaesarCipherAlgorithm(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Decrypt(string input)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string input)
        {
            throw new NotImplementedException();
        }
    }
}
