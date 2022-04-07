using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms.KeyGenerators
{
    internal interface IKeyGenerator
    {
        public string Key { get; }
        void StartGeneratingKey();

        void StopGeneratingKey();

        void ClearKey();

        void GenerateKeyElement();
    }
}
