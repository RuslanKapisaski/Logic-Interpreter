using LogicInterpreter.Core;
using System;

namespace LogicInterpreter.Commands
{
    class All
    {
        public static void Execute(LogicalFunction function)
        {
            // Header: a , b , c : FuncName
            for (int i = 0; i < function.Parameters.Length; i++)
            {
                Console.Write(function.Parameters[i]);

                if (i < function.Parameters.Length - 1)
                {
                    Console.Write(" , ");

                }
            }
            Console.Write(" : ");
            Console.WriteLine(function.Name);

            int n = function.Parameters.Length;
            int[] values = new int[n];

            Generate(function, values, 0);

        }
        private static void Generate(LogicalFunction function, int[] values, int index)
        {
            if (index == values.Length)
            {
                // print current combination
                for (int i = 0; i < values.Length; i++)
                {
                    Console.Write(values[i]);

                    if (i < values.Length - 1)
                    {
                        Console.Write(" , ");
                    }
                }


                for (int i = 0; i < function.Parameters.Length; i++)
                {
                    Solver.UpdateVariable(function.Root, function.Parameters[i], values[i]);
                }

                int result = Solver.Evaluate(function.Root);

                Console.Write(" : ");
                Console.WriteLine(result);

                return;
            }

            values[index] = 0;
            Generate(function, values, index + 1);

            values[index] = 1;
            Generate(function, values, index + 1);
        }
    }
}