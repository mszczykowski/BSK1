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

        private object _targetProperty;

        private string KeyPropertyName { get; }
        public AlgorithmViewModel(string name, Algorithm algorithm, string keyErrorMessage, string keyName, string keyPropertyName)
        {
            Name = name;
            Algorithm = algorithm;
            KeyErrorMessage = keyErrorMessage;
            KeyName = keyName;

            KeyPropertyName = keyPropertyName;
        }

        public bool IsKeyValid(string key)
        {
            return Algorithm.IsKeyValid(key);
        }

    }
}
