using System;
using LogicInterpreter.Core;
using LogicInterpreter.Parsing;

namespace LogicInterpreter.Commands
{
    class Define
    {
        public static LogicalFunction Execute(string input)
        {
            input = input.Trim();

            int i = 6;
            string name = null;

            //Function name
            while (i < input.Length && input[i] != '(')
            {
                if (input[i] != ' ')
                {
                    name += input[i];
                }

                i++;
            }

            i++;

            //Parameters
            char[] parameters = new char[10];
            int parametersCount = 0;

            while (input[i] != ')')
            {
                char parameter = input[i];

                if (parameter >= 'a' && parameter <= 'z')
                {
                    parameters[parametersCount] = parameter;
                    parametersCount++;
                }
                i++;
            }

            while (input[i] != ':') i++;
            i++;

            //Reading expression
            string expression = " ";

            while (i < input.Length)
            {
                if (input[i] != ' ')
                {
                    expression += input[i];
                }
                i++;
            }

            //Parsing
            ExpressionParser parser = new ExpressionParser();
            Node root = parser.Parse(expression);

            //Function creation
            LogicalFunction function = new LogicalFunction
            {
                Name = name,
                Parameters = Trim(parameters, parametersCount),
                Root = root
            };

            Console.WriteLine("Function defined:" + name);
            return function;

        }

        private static char[] Trim(char[] arr, int size)
        {
            char[] result = new char[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = arr[i];
            }
            return result;
        }
    }
}
