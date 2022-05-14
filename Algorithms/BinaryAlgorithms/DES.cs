using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            List<string> inputParts = Split(input, 64).ToList(); // Divide input to 64 bit parts
            List<string> decryptedParts = new List<string>();

            string[] keys = CalculateKeys(_viewModel.KeyInput);
            keys.Reverse();

            foreach (string part in inputParts)
                decryptedParts.Add(ExecuteAlgorithm(part, keys)); // Execute DES algorithm on every part

            string result = "";

            foreach (string part in decryptedParts)
                result += RemoveMissingBytes(part);

            return result;
        }

        public override string Encrypt(string input)
        {
            List<string> inputParts = DivideInput(input); // Divide input to 64 bit parts
            List<string> encryptedParts = new List<string>();

            string[] keys = CalculateKeys(_viewModel.KeyInput);

            foreach (string part in inputParts)
                encryptedParts.Add(ExecuteAlgorithm(part, keys)); // Execute DES algorithm on every part

            string result = string.Join("", encryptedParts.ToArray()); // Connect every encrypted part and return as a result

            return result;
        }

        public override bool IsKeyValid(string key)
        {
            if (String.IsNullOrEmpty(key) || !Regex.IsMatch(key, "^[01]+$") || key.Length != 64)
                return false;

            return true;
        }

        // Methods
        private string ExecuteAlgorithm(string input, string[] keys)
        {
            string inputAfterInitial = ExecutePermutation(input, InitialPermutation);
            string leftHalf;
            string rightHalf;

            SplitBitsString(inputAfterInitial, out leftHalf, out rightHalf);

            for (int i = 0; i < 16; i++)
            {
                string calculationResult = CalculateRightHalfWithKey(rightHalf, keys[i]);
                string xorResult = XORStrings(leftHalf, calculationResult);

                leftHalf = rightHalf;
                rightHalf = xorResult;
            }

            string temp = leftHalf;
            leftHalf = rightHalf;
            rightHalf = temp;

            string inputAfterKeys = leftHalf + rightHalf;
            string result = ExecutePermutation(inputAfterKeys, InitialPermutationInverted);

            return result;
        }

        private string[] CalculateKeys(string key)
        {
            string keyPermuted = ExecutePermutation(key, PermutedChoice1);

            var C = keyPermuted.Substring(0, 28);
            var D = keyPermuted.Substring(28);
            
            int x = C.Length + D.Length;

            string[] keys = new string[16];
            for (int i = 0; i < 16; i++)
            {
                C = LeftShift(C, ShiftArray[i]);
                D = LeftShift(D, ShiftArray[i]);

                keys[i] = ExecutePermutation(C + D, PermutedChoice2);
            }

            return keys;
        }

        private string LeftShift(string stringToShift, int shiftValue)
        {
            return stringToShift[shiftValue..] + stringToShift[..shiftValue];
        }

        private int BinaryToDecimal(char[] binary)
        {
            int decimalNumber = 0;
            for (int i = binary.Length - 1, j = 1; i >= 0; i--, j *= 2) 
            {
                decimalNumber += binary[i] == '1' ? j : 0;
            }
            return decimalNumber;
        }

        private string DecimalToBinary(int decimalNumber)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; decimalNumber > 0; i++)
            {
                builder.Append(decimalNumber % 2);
                decimalNumber /= 2;
            }
            return builder.ToString();
        }

        private string CalculateRightHalfWithKey(string rightHalf, string key)
        {
            /* 
             * TO DO
             * Dla osoby, która będzie robić łączenie prawej połowy wejścia z kluczem
             * na wejściu jest 32 bity prawej połowy i klucz 48 bitowy
             * metoda powinna zwracać 32 bit wartość
             */
            StringBuilder builder = new StringBuilder(); // 110001
            rightHalf = ExecutePermutation(rightHalf, ExtensionArray);
            string keyAndRightXored = XORStrings(key, rightHalf);
            

            for (int i = 0; i < 8; i++) 
            {
                char[] row = { keyAndRightXored[i * 6], keyAndRightXored[(i * 6) + 5] };
                char[] col = keyAndRightXored.Substring(i * 6 + 1, 4).ToCharArray();
                string transformedInto4Bits;
                int[] chosenPermutation;
                switch (i)
                {
                    case 0:
                        chosenPermutation = S1;
                        break;
                    case 1:
                        chosenPermutation = S2;
                        break;
                    case 2:
                        chosenPermutation = S3;
                        break;
                    case 3:
                        chosenPermutation = S4;
                        break;
                    case 4:
                        chosenPermutation = S5;
                        break;
                    case 5:
                        chosenPermutation = S6;
                        break;
                    case 6:
                        chosenPermutation = S7;
                        break;
                    case 7:
                        chosenPermutation = S8;
                        break;
                    default:
                        chosenPermutation = S1;
                        break;
                }
                transformedInto4Bits = Convert.ToString(chosenPermutation[BinaryToDecimal(row) * BinaryToDecimal(col)], 2).PadLeft(4, '0');
                builder.Append(transformedInto4Bits);
            }
            
            // temporary
            return builder.ToString();
        }

        private string XORStrings(string leftHalf, string calcResult)
        {
            /* 
             * TO DO
             * Dla kogoś kto będzie xor robił
             * oba wejścia 32 bit
             * xorujemy jedno z drugim
             * ma zwracać 32 bit
             */
            if (leftHalf.Length != calcResult.Length)
            {
                throw new InvalidOperationException("Not equal");
            }
            char[] temp = leftHalf.ToCharArray();

            for (int i = 0; i < leftHalf.Length; i++) 
            {
                string xor = (leftHalf[i] ^ calcResult[i]).ToString();
                temp[i] = xor[0];
            }

            return new string (temp);
        }

        // Executes permutation on given string
        private string ExecutePermutation(string inputBits, int[] permutationArray)
        {
            string newString = "";
            for (int i = 0; i < permutationArray.Length; i++)
            {
                int position = permutationArray[i];
                int index = position - 1;

                newString += inputBits[index];
            }

            return newString;
        }

        // Splits input string into 2 equal halves
        private void SplitBitsString(string input, out string leftOutput, out string rightOutput)
        {
            int mid = input.Length / 2;
            leftOutput = input.Substring(0, mid);
            rightOutput = input.Substring(mid, mid);
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

        private string RemoveMissingBytes(string input)
        {
            string lastByte = input.Substring(input.Length - 8);
            int numberOfMissingBytes = Convert.ToInt32(lastByte, 2) + 1;
            int numberBitsToDelete = numberOfMissingBytes * 8;
            string result = input.Remove(input.Length - numberBitsToDelete);

            return result;
        }

        /* 
         * Permutation arrays
         * "X bits -> Y bits"
         * 1. Get X bits string.
         * 2. Permute it using given array (e.g. using PermutedChoice1).
         * 3. Store it into second array of Y bits.
         */

        // 64 bits
        private int[] InitialPermutation =
            { 58, 50, 42, 34, 26, 18, 10, 2,
              60, 52, 44, 36, 28, 20, 12, 4,
              62, 54, 46, 38, 30, 22, 14, 6,
              64, 56, 48, 40, 32, 24, 16, 8,
              57, 49, 41, 33, 25, 17, 9, 1,
              59, 51, 43, 35, 27, 19, 11, 3,
              61, 53, 45, 37, 29, 21, 13, 5,
              63, 55, 47, 39, 31, 23, 15, 7 };

        // 64 bits -> 56 bits
        private int[] PermutedChoice1 =
            { 57, 49, 41, 33, 25, 17, 9,
              1, 58, 50, 42, 34, 26, 18,
              10, 2, 59, 51, 43, 35, 27,
              19, 11, 3, 60, 52, 44, 36,
              63, 55, 47, 39, 31, 23, 15,
              7, 62, 54, 46, 38, 30, 22,
              14, 6, 61, 53, 45, 37, 29,
              21, 13, 5, 28, 20, 12, 4 };

        private int[] ShiftArray = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        // 56 bits -> 48 bits
        private int[] PermutedChoice2 =
            { 14, 17, 11, 24, 1, 5,
              3, 28, 15, 6, 21, 10,
              23, 19, 12, 4, 26, 8,
              16, 7, 27, 20, 13, 2,
              41, 52, 31, 37, 47, 55,
              30, 40, 51, 45, 33, 48,
              44, 49, 39, 56, 34, 53,
              46, 42, 50, 36, 29, 32 };

        // 32 bits -> 48 bits
        private int[] ExtensionArray =
            { 32, 1, 2, 3, 4, 5,
              4, 5, 6, 7, 8, 9,
              8, 9, 10, 11, 12, 13,
              12, 13, 14, 15, 16, 17,
              16, 17, 18, 19, 20, 21,
              20, 21, 22, 23, 24, 25,
              24, 25, 26, 27, 28, 29,
              28, 29, 30, 31, 32, 1 };

        private int[] S1 =
            { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7,
              0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8,
              4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0,
              15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 };

        private int[] S2 =
            { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10,
              3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5,
              0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15,
              13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 };

        private int[] S3 =
            { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8,
              13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1,
              13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7,
              1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 };

        private int[] S4 =
            { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15,
              13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9,
              10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4,
              3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 };

        private int[] S5 =
            { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9,
              14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6,
              4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14,
              11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 };

        private int[] S6 =
            { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11,
              10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8,
              9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6,
              4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 };

        private int[] S7 = 
            { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1,
              13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6,
              1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2,
              6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 };

        private int[] S8 = 
            { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7,
              1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2,
              7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8,
              2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 };

        // 32 bits
        private int[] Permutation = 
            { 16, 7, 20, 21,
              29, 12, 28, 17,
              1, 15, 23, 26,
              5, 18, 31, 10,
              2, 8, 24, 14,
              32, 27, 3, 9,
              19, 13, 30, 6,
              22, 11, 4, 25 };

        // 64 bits
        private int[] InitialPermutationInverted = 
            { 40, 8, 48, 16, 56, 24, 64, 32,
              39, 7, 47, 15, 55, 23, 63, 31,
              38, 6, 46, 14, 54, 22, 62, 30,
              37, 5, 45, 13, 53, 21, 61, 29,
              36, 4, 44, 12, 52, 20, 60, 28,
              35, 3, 43, 11, 51, 19, 59, 27,
              34, 2, 42, 10, 50, 18, 58, 26,
              33, 1, 41, 9, 49, 17, 57, 25 };
    }
}
