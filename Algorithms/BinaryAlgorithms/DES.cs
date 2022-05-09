using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms.BinaryAlgorithms
{
    internal class DES : Algorithm
    {
        private int DataSize { get; } = 56;

        public DES(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Decrypt(string input)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string input)
        {
            List<string> inputParts = DivideInput(input); // Divide input to 64 bit parts
            List<string> encryptedParts = new List<string>();

            foreach (string part in inputParts)
            {
                encryptedParts.Add(ExecuteAlgorithm(part)); // Execute DES algorithm on every part
            }

            string result = string.Join("", encryptedParts.ToArray()); // Connect every encrypted part and return as a result

            return result;
            //throw new NotImplementedException();
        }

        public override bool IsKeyValid(string key)
        {
            return true;
            //throw new NotImplementedException();
        }

        // Methods
        private string ExecuteAlgorithm(string input)
        {
            return input;
        }

        private List<string> DivideInput(string input)
        {
            // one ascii char = 16 bits
            List<string> parts = Split(input, DataSize).ToList(); // cut to 56 bits strings

            for (int i = 0; i < parts.Count; i++)
            {
                parts[i] = AddMissingBytes(parts[i]);
            }

            return parts;
        }

        private IEnumerable<string> Split(string str, int chunkSize)
        {
            if (String.IsNullOrEmpty(str) || chunkSize < 1)
                throw new ArgumentException();

            for (int i = 0; i < str.Length; i += chunkSize)
                yield return str.Substring(i, Math.Min(chunkSize, str.Length - i));
        }

        private string AddMissingBytes(string input)
        {
            int stringLength = input.Length;
            int difference = DataSize - stringLength;
            int bytesNumberToAdd = difference / 8;

            for (int i = 0; i < bytesNumberToAdd; i++)
                input += "00000000";

            input += Convert.ToString(bytesNumberToAdd, 2).PadLeft(8, '0');

            return input;
        }
    }
}
