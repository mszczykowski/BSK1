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
        private int[] powers;
        private string seed;

        public LFSR(EventHandler KeyUpdatedEventHandler, AlgorithmsFormViewModel viewModel) : base(KeyUpdatedEventHandler, viewModel)
        {
        }

        public override string GenerateKey(int keyLenght)
        {
            Initialize(_viewModel.KeyInput);
            for (int i = 0; i < keyLenght; i++)
                GenerateKeyElement();

            OnKeyUpdate();
            return _key;
        }

        public override void GenerateKeyElement()
        {
            if (_seed == null || powers == null)
                Initialize(_viewModel.KeyInput);


            char[] powersBits = new char[powers.Length];
            for (int i = 0; i < powers.Length; i++) // Get bits of given powers
                powersBits[i] = seed[powers[i] - 1];

            string newBit = XORArray(powersBits);
            seed = seed.Insert(0, newBit); // Add new bit at the beginning
            char lastBit = seed[seed.Length - 1]; // Get last bit
            seed = seed.Remove(seed.Length - 1); // Remove last bit from string

            _key += lastBit; // Return last string to key
        }


        // Seting Powers and random Bits
        private void Initialize(string powersText)
        {
            _key = "";

            string[] stringPowers = powersText.Replace(" ", string.Empty).Split(","); // Clear spaces, divide by every comma
            
            powers = Array.ConvertAll(stringPowers, s => int.TryParse(s, out var x) ? x : -1).OrderBy(x => x).ToArray(); // convert to int[] array

            if (String.IsNullOrEmpty(_viewModel.Seed))
            {
                _seed = GenerateSeed(powers.Max());
            }

            else _seed = _viewModel.Seed;

            SeedExtend(powers.Max());

            seed = new string(_seed);
        }

        // Generating random string of bits of specific length
        private string GenerateSeed(int length)
        {
            string seed = "";
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                if (rand.Next(0, 2) == 0)
                    seed += "0";
                else
                    seed += "1";
            }
            return seed;
        }


        private void SeedExtend(int length)
        {
            int originalSeeedLenght = _seed.Length;
            for (int i = _seed.Length, j = 0; i < length; i++, j++)
            {
                if(j >= originalSeeedLenght)
                {
                    j = 0;
                }

                _seed += _seed[j];
                
            }
        }

        /// <summary>
        /// Executes XOR operation on every element of array.
        /// </summary>
        /// <param name="elements">Array of '0 or '1' chars.</param>
        /// <returns>Returns result of XOR operation on array in string.</returns>
        private string XORArray(char[] elements)
        {
            // 0, 1, 1, 0, 0
            // 1, 1, 0
            // 0, 0
            // 0
            int[] intElements = Array.ConvertAll(elements, c => (int)char.GetNumericValue(c));
            int result = intElements.Aggregate((x, y) => x ^ y);

            return result.ToString();
        }
    }
}
