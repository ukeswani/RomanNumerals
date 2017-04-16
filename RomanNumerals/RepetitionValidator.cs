using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals
{
    public class RepetitionValidator : IValidator
    {
        private IDictionary<char, int> _dict;

        public RepetitionValidator(Dictionary<char, int> dict)
        {
            _dict = dict;
        }

        public bool IsValid(string input)
        {
            return
               AllowedRepetitionValidation('I', input)
               &&
               AllowedRepetitionValidation('X', input)
               &&
               AllowedRepetitionValidation('C', input)
               &&
               AllowedRepetitionValidation('M', input)
               &&
               NoRepetitionValidation('D', input)
               &&
               NoRepetitionValidation('L', input)
               &&
               NoRepetitionValidation('V', input);
        }

        private bool NoRepetitionValidation
                        (
                             char symbol
                            , string input
                        )
        {
            var inputList = input.ToList();
            var inputArray = input.ToArray();

            var firstIndex = inputList.FindIndex((ch) => ch.Equals(symbol));
            var lastIndex = inputList.FindLastIndex((ch) => ch.Equals(symbol));

            var indexDiff = lastIndex - firstIndex;

            if (indexDiff != 0)
            {
                Console.WriteLine("Invalid input\n");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool AllowedRepetitionValidation
                        (
                             char symbol
                            , string input
                        )
        {
            var inputList = input.ToList();
            var inputArray = input.ToArray();

            var firstIndex = inputList.FindIndex((ch) => ch.Equals(symbol));
            var lastIndex = inputList.FindLastIndex((ch) => ch.Equals(symbol));

            var indexDiff = lastIndex - firstIndex;

            if (indexDiff > 4)
            {
                Console.WriteLine("Invalid input\n");
                return false;
            }
            else if (
                        indexDiff == 4
                        &&
                        !(_dict[inputArray[lastIndex]] > _dict[inputArray[lastIndex - 1]])
                    )
            {
                Console.WriteLine("Invalid input\n");
                return false;
            }
            else if (
                        indexDiff == 3
                        &&
                        !(
                            SameConsecutiveCharacters
                            (
                                 inputArray
                                , 2
                                , firstIndex
                            )
                            &&
                            (_dict[inputArray[lastIndex]] > _dict[inputArray[lastIndex - 1]])
                        )

                    )
            {
                Console.WriteLine("Invalid input\n");
                return false;
            }
            else if (
                        indexDiff == 2
                        &&
                        !SameConsecutiveCharacters
                            (
                                 inputArray
                                , 3
                                , firstIndex
                            )
                        &&
                        !(_dict[inputArray[lastIndex]] > _dict[inputArray[lastIndex - 1]])
                    )
            {
                Console.WriteLine("Invalid input\n");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool SameConsecutiveCharacters
                        (
                             char[] inputArray
                            , int howMany
                            , int startingIndex
                        )
        {
            int count = 1;

            for (int i = startingIndex + 1; i < (startingIndex + howMany); i++)
            {
                if (inputArray[i - 1].Equals(inputArray[i]))
                {
                    count++;
                }
            }

            return count == howMany;
        }
    }
}
