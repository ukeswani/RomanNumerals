using System;
using System.Collections.Generic;

namespace RomanNumerals
{
    public class SusbstractionValidator : IValidator
    {
        private IDictionary<char, int> _dict;
        private IDictionary<char, List<char>> _sub;

        public SusbstractionValidator(Dictionary<char, int> dict, Dictionary<char, List<char>> sub)
        {
            _dict = dict;
            _sub = sub;
        }

        public bool IsValid(string input)
        {
            var inputArray = input.ToCharArray();

            for (int i = 1; i < inputArray.Length; i++)
            {
                if (
                        _dict[inputArray[i]] > _dict[inputArray[i - 1]]
                        &&
                        !IsSubstractible
                            (
                                 inputArray[i - 1]
                                , inputArray[i]
                            )
                    )
                {
                    Console.Write("[Rule {0}, clause {1}]. ", 2, 1);
                    Console.WriteLine("Invalid input\n");
                    return false;
                }
            }

            return true;
        }

        private bool IsSubstractible(char to, char from)
        {
            return _sub[to].Contains(from);
        }
    }
}
