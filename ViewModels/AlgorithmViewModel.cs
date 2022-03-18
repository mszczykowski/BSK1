using BSK1.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK1.Enums;

namespace BSK1.ViewModels
{
    internal class AlgorithmViewModel : ViewModelBase
    {
        

        public string Name { get; }
        public Algorithm Algorithm { get; }

        public RequiredForm RequiredForm { get; }
        public AlgorithmViewModel(string name, Algorithm algorithm, RequiredForm requiredForm)
        {
            Name = name;
            Algorithm = algorithm;
            RequiredForm = requiredForm;
        }


    }
}
