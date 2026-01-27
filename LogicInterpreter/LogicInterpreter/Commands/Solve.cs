using LogicInterpreter.Core;
using System;

namespace LogicInterpreter.Commands
{
    class Solve
    {

        //Задаване на стойност за всеки параметър
        public static void Execute(LogicalFunction function, int[] values)
        {
            for (int i = 0; i < function.Parameters.Length; i++)
            {
                UpdateVariable(function.Root, function.Parameters[i], values[i]);
            }

            int result = Evaluate(function.Root);
            Console.WriteLine("Result: " + result);
        }

        // Рекурсивно решаване и кеширане
        internal static int Evaluate(Node node)
        {
            if (node.IsCalculated)
            {
                return node.Value;
            }

            if (node.Type == NodeType.Variable)
            {
                node.IsCalculated = true;
                return node.Value;
            }

            if (node.Type == NodeType.Not)
            {
                node.Value = Evaluate(node.Left) == 1 ? 0 : 1;
            }
            else if (node.Type == NodeType.And)
            {
                node.Value = Evaluate(node.Left) & Evaluate(node.Right);
            }

            else if (node.Type == NodeType.Or)
            {
                node.Value = Evaluate(node.Left) | Evaluate(node.Right);
            }

            node.IsCalculated = true;
            return node.Value;
        }

        // Маркира само засегнатите части за преизчисляване
        internal static bool UpdateVariable(Node node, char varName, int newValue)
        {
            if (node == null) return false;

            if (node.Type == NodeType.Variable && node.Name == varName)
            {
                if (node.Value != newValue)
                {
                    node.Value = newValue;
                    node.IsCalculated = false;
                    return true;
                }
                return false;
            }
            //Търсим промяна в ляво и дясно държо
            bool leftChanged = UpdateVariable(node.Left, varName, newValue);
            bool rightChanged = UpdateVariable(node.Right, varName, newValue);

            if (leftChanged || rightChanged)
            {
                node.IsCalculated = false;
                return true;
            }

            return false;
        }
    }
}