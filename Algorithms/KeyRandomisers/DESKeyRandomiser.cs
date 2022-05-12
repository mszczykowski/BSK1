using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms.KeyRandomisers
{
    internal class DESKeyRandomiser : IKeyRandomiser
    {
        private readonly int keyLenght = 64;
        private readonly Random random;

        public DESKeyRandomiser()
        {
            random = new Random();
        }

        public string GenerateRandomKey()
        {
            string key = "";

            for(int i = 0; i < keyLenght; i++)
            {
                key += random.Next(2).ToString();
            }

            return key;
        }
    }
}
