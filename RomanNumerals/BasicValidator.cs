using System;
using System.Collections.Generic;

namespace RomanNumerals
{
    public class BasicValidator : IValidator
    {
        private IDictionary<char, int> _dict;

        public BasicValidator(Dictionary<char, int> dict)
        {
            _dict = dict;
        }

        public bool IsValid(string input)
        {
            var inputArray = input.ToCharArray();

            for (int i = 0; i < inputArray.Length; i++)
            {
                if (!_dict.Keys.Contains(inputArray[i]))
                {
                    Console.Write("[Rule {0}, clause {1}]. ", 4, 1);
                    Console.WriteLine("Invalid input\n");
                    return false;
                }
            }

            return true;
        }
    }
}
