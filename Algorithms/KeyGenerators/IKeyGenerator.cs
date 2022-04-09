using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms.KeyGenerators
{
    internal interface IKeyGenerator
    {
        string Key { get; }
        bool IsRunning { get; }
        void StartGeneratingKey();

        void StopGeneratingKey();

        void ClearKey();

        void GenerateKeyElement();

        string GenerateKey(int keyLenght);
    }
}
