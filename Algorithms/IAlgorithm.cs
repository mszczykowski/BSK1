using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms
{
    internal interface IAlgorithm
    {
        public string Encrypt(string input);

        public string Decrypt(string input);
    }
}
