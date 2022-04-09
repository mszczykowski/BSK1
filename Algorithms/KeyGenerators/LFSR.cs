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
        private string Bits;
        private int[] Powers;

        public LFSR(EventHandler KeyUpdatedEventHandler, AlgorithmsFormViewModel viewModel) : base(KeyUpdatedEventHandler, viewModel)
        {
        }

        public override string GenerateKey(int keyLenght)
        {
            Initialize(_viewModel.KeyInput);
            for (int i = 0; i < keyLenght; i++)
                GenerateKeyElement();

            ClearData();
            return key;
        }

        public override void GenerateKeyElement()
        {
            if (Bits == null || Powers == null)
                Initialize(_viewModel.KeyInput);

            char[] powersBits = new char[Powers.Length];
            for (int i = 0; i < Powers.Length; i++) // Get bits of given powers
                powersBits[i] = Bits[Powers[i] - 1];

            string newBit = XORArray(powersBits);
            Bits = Bits.Insert(0, newBit); // Add new bit at the beginning
            char lastBit = Bits[Bits.Length - 1]; // Get last bit
            Bits = Bits.Remove(Bits.Length - 1); // Remove last bit from string

            key += lastBit; // Return last string to key
        }

        public override void ClearKey()
        {
            base.ClearKey();

            ClearData();
        }

        private void ClearData()
        {
            Bits = null;
            Powers = null;
        }

        // Seting Powers and random Bits
        private void Initialize(string powers)
        {
            Powers = powers.Replace(" ", string.Empty).Split(",").Select(int.Parse).Distinct().OrderBy(x => x).ToArray(); // Clear spaces, divide by every comma, convert to int[] array
            Bits = GenerateRandomBits(Powers.Max()); // Generate random bits of max power length
        }

        // Generating random string of bits of specific length
        private string GenerateRandomBits(int length)
        {
            string bits = "";
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                if (rand.Next(0, 2) == 0)
                    bits += "0";
                else
                    bits += "1";
            }
            return bits;
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
            int current = 0;
            foreach (int value in intElements)
            {
                current ^= value;
            }

            return current.ToString();
        }
    }
}
