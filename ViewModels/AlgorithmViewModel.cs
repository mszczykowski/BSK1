using BSK1.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.ViewModels
{
    internal class AlgorithmViewModel : ViewModelBase
    {


        public string Name { get; }
        public Algorithm Algorithm { get; }
        public string KeyErrorMessage { get; }

        public string KeyName { get; }


        public AlgorithmViewModel(string name, Algorithm algorithm, string keyErrorMessage, string keyName)
        {
            Name = name;
            Algorithm = algorithm;
            KeyErrorMessage = keyErrorMessage;
            KeyName = keyName;
        }

        public bool IsKeyValid(string key)
        {
            return Algorithm.IsKeyValid(key);
        }

    }
}
