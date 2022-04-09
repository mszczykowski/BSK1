using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms.KeyGenerators
{
    internal class LFSR : KeyGenerator
    {
        public LFSR(EventHandler KeyUpdatedEventHandler, AlgorithmsFormViewModel viewModel) : base(KeyUpdatedEventHandler, viewModel)
        {
        }

        public override string GenerateKey(int keyLenght)
        {
            throw new NotImplementedException();
        }

        public override void GenerateKeyElement()
        {
            key += 1;
        }
    }
}
