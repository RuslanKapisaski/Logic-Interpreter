using LogicInterpreter.Commands;
using LogicInterpreter.Core;
using LogicInterpreter.Utils;
using System;

class Program
{
    static void Main()
    {

        Console.WriteLine("Welcome! Possible commands: DEFINE, SOLVE, ALL, FIND, SAVE, LOAD & EXIT");

        FunctionCollection functions = new FunctionCollection();

        string[] saveDefines = new string[20];
        int savedCount = 0;

        while (true)
        {
            Console.Write("> ");

            string input = Console.ReadLine();

            if (input == null) continue;

            string[] tokens = Splitter.Split(input, new char[] { ' ' });

            if (tokens[0] == null) continue;

            switch (tokens[0])
            {
                case "DEFINE":
                    try
                    {
                        Define.Execute(input, functions);
                        saveDefines[savedCount++] = input;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    break;

                case "SOLVE":
                    try
                    {
                        Solve(tokens, functions);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    break;

                case "ALL":
                    try
                    {
                        LogicalFunction f = functions.Get(tokens[1]);
                        if (f == null) throw new Exception("Unknown function");
                        All.Execute(f);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    break;

                case "FIND":
                    FinderDemo();
                    break;

                case "SAVE":
                    SaveLoad.Save(tokens[1], saveDefines, savedCount);
                    Console.WriteLine("Saved..");
                    break;

                case "LOAD":
                    SaveLoad.Load(tokens[1], functions);
                    Console.WriteLine("Loaded..");
                    break;

                case "EXIT":
                    return;

                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }

    static void Solve(string[] parts, FunctionCollection functions)
    {
        string expression = parts[1];

        //SOLVE Func(1,0)

        string[] tokens = Splitter.Split(expression, new char[] { '(', ')', ',' });

        string name = tokens[0];

        LogicalFunction func = functions.Get(name);

        if (func == null)
        {
            throw new Exception("Unknown function");
        }

        int[] values = new int[func.Parameters.Length];

        for (int i = 0; i < func.Parameters.Length; i++)
        {
            values[i] = tokens[i + 1][0] - '0';
        }

        LogicInterpreter.Commands.Solve.Execute(func, values);
    }

    static void FinderDemo()
    {
        //a,b,c,result
        int[,] table =
        {
            {0,0,0,0},
            {0,0,1,0},
            {0,1,0,0},
            {0,1,1,0},
            {1,0,0,0},
            {1,0,1,0},
            {1,1,0,0},
            {1,1,1,1}
        };

        char[] vars = { 'a', 'b', 'c' };
        Find.Execute(table, vars);
    }
}