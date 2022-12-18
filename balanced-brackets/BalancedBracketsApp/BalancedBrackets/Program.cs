using System;
using System.Collections.Generic;
using System.Linq;

namespace BalancedBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Informe a expressão balanceada: ");
                string expression = Console.ReadLine();

                bool balancedExpression = ValidateExpression(expression);

                Console.Write("\nResultado: ");
                if (balancedExpression)
                {
                    Console.WriteLine($"{expression} é válida");
                }
                else
                {
                    Console.WriteLine($"{expression} não é válida");
                }

                Console.WriteLine("\nPressione qualquer tecla para informar um nova expressão...");
                Console.ReadKey();

                Console.Clear();
            }
        }

        private static bool ValidateExpression(string expression)
        {
            if (expression.Length == 0)
                return true;

            char[] openingBrackets = { '{', '(', '[' };
            char[] closingBrackets = { '}', ')', ']' };

            Stack<char> brackets = new Stack<char>();

            foreach (char bracket in expression)
            {
                if (openingBrackets.Contains(bracket))
                {
                    brackets.Push(bracket);
                }
                else if (closingBrackets.Contains(bracket))
                {
                    if (brackets.Count == 0)
                        return false;

                    char openingBracket = brackets.Pop();

                    if (!ValidateBracketPair(openingBracket, bracket))
                        return false;
                }
            }

            return brackets.Count == 0;
        }

        private static bool ValidateBracketPair(char openingBracket, char closingBracket)
        {
            return (openingBracket == '{' && closingBracket == '}') 
                || (openingBracket == '(' && closingBracket == ')')
                || (openingBracket == '[' && closingBracket == ']');
        }
    }
}
