using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms.KeyGenerators
{
    internal class LFSR : KeyGenerator
    {
        public LFSR(EventHandler KeyUpdatedEventHandler) : base(KeyUpdatedEventHandler)
        {
        }

        public override void GenerateKeyElement()
        {
            key += 1;
        }
    }
}
