using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanNumerals
{
    class Program
    {
        private static IDictionary<char, int> _dict = new Dictionary<char, int>();
        private static IDictionary<char, List<char>> _sub = new Dictionary<char, List<char>>();

        static void Main(string[] args)
        {            
            _dict.Add('I', 1);
            _dict.Add('V', 5);
            _dict.Add('X', 10);
            _dict.Add('L', 50);
            _dict.Add('C', 100);
            _dict.Add('D', 500);
            _dict.Add('M', 1000);

            _sub.Add('I', new List<char> { 'V', 'X'});
            _sub.Add('V', new List<char> { });
            _sub.Add('X', new List<char> { 'L', 'C' });
            _sub.Add('L', new List<char> { });
            _sub.Add('C', new List<char> { 'D', 'M' });
            _sub.Add('D', new List<char> { });
            _sub.Add('M', new List<char> { });

            ConsoleKeyInfo instruction;

            do
            {
                Console.Write("Input: ");
                var input = Console.ReadLine();

                var inputArray = input.ToCharArray();

                if
                    (
                        RepetitionValidation(input) == true
                        &&
                        SusbstractionValidation(input) == true
                        &&
                        FormationValidation(input) == true
                        &&
                        BasicValidation(input) == true
                    )     //-- Validation
                {

                    //-- Interpretation
                    int newValue = _dict[inputArray[0]];

                    for (int i = 1; i < inputArray.Length; i++)
                    {
                        if (_dict[inputArray[i]] > _dict[inputArray[i - 1]])
                        {
                            newValue -= _dict[inputArray[i - 1]];
                            newValue += _dict[inputArray[i]] - _dict[inputArray[i - 1]];
                        }
                        else
                        {
                            newValue += _dict[inputArray[i]];
                        }
                    }

                    Console.WriteLine("Output: {0}\n", newValue.ToString());
                    //--
                }                   
          
                Console.Write("Press 'e' or 'E' to exit. Any other key to continue.");
                instruction = Console.ReadKey();

                Console.Write("\n\n");

            } while (!(instruction.KeyChar == 'e' || instruction.KeyChar == 'E'));
        }

        public static bool BasicValidation(string input)
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

        public static bool FormationValidation(string input)
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

        private static bool SusbstractionValidation(string input)
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
                                ,inputArray[i]
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

        private static bool IsSubstractible(char to, char from)
        {
            return _sub[to].Contains(from);
        }

        private static bool RepetitionValidation(string input)
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

        private static bool NoRepetitionValidation
                        (
                             char symbol
                            ,string input
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

        private static bool AllowedRepetitionValidation
                        (
                             char symbol
                            ,string input
                        )
        {
            var inputList = input.ToList();
            var inputArray = input.ToArray();

            var firstIndex = inputList.FindIndex((ch) => ch.Equals(symbol));
            var lastIndex = inputList.FindLastIndex((ch) => ch.Equals(symbol));

            var indexDiff = lastIndex - firstIndex;

            if (indexDiff > 4)
            {
                Console.Write("[Rule {0}, clause {1}]. ", 1,1);
                Console.WriteLine("Invalid input\n");
                return false;
            }            
            else if (
                        indexDiff == 4
                        &&
                        !(_dict[inputArray[lastIndex]] > _dict[inputArray[lastIndex - 1]])
                    )
            {
                Console.Write("[Rule {0}, clause {1}]. ", 1, 2);
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
                                ,2
                                ,firstIndex
                            )
                            &&
                            (_dict[inputArray[lastIndex]] > _dict[inputArray[lastIndex - 1]])
                        )

                    )
            {
                Console.Write("[Rule {0}, clause {1}]. ", 1, 3);
                Console.WriteLine("Invalid input\n");
                return false;
            }
            else if (
                        indexDiff == 2
                        &&                        
                        !SameConsecutiveCharacters
                            (
                                 inputArray
                                ,3
                                ,firstIndex
                            )
                        &&
                        !(_dict[inputArray[lastIndex]] > _dict[inputArray[lastIndex - 1]])
                    )
            {
                Console.Write("[Rule {0}, clause {1}]. ", 1, 4);
                Console.WriteLine("Invalid input\n");
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool SameConsecutiveCharacters
                        (
                             char[] inputArray                            
                            ,int howMany
                            ,int startingIndex
                        )
        {
            int count = 1;

            for (int i = startingIndex + 1; i < (startingIndex + howMany); i++)
            {
                if(inputArray[i-1].Equals(inputArray[i]))
                {
                    count++;
                }
            }

            return count == howMany;
        }
    }     
}
