using System;
using System.Collections.Generic;

namespace RomanNumerals
{
    public class FormationValidator : IValidator
    {
        private IDictionary<char, int> _dict;

        public FormationValidator(Dictionary<char, int> dict)
        {
            _dict = dict;
        }

        public bool IsValid(string input)
        {
            if (input.Length > 2)
            {
                var inputArray = input.ToCharArray();

                for (int i = 2; i < inputArray.Length; i++)
                {
                    if (
                            _dict[inputArray[i]] > _dict[inputArray[i - 1]]
                            &&
                            !(_dict[inputArray[i - 2]] >= _dict[inputArray[i]])
                        )
                    {
                        Console.Write("[Rule {0}, clause {1}]. ", 3, 1);
                        Console.WriteLine("Invalid input\n");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
