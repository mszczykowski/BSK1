using BSK1.Algorithms;
using BSK1.Algorithms.KeyGenerators;
using BSK1.Algorithms.KeyRandomisers;
using BSK1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.ViewModels
{
    internal class AlgorithmViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public Algorithm Algorithm { get; set; }
        public string KeyErrorMessage { get; set; }
        public string KeyName { get; set; }
        public AlgorithmType AlgorithmType { get; set; }
        public IKeyGenerator KeyGenerator { get; set; }
        public IKeyRandomiser KeyRandomiser { get; set; }

        public bool IsKeyValid(string key)
        {
            return Algorithm.IsKeyValid(key);
        }

    }
}
