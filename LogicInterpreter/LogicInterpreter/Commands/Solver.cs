using LogicInterpreter.Core;
using System;

namespace LogicInterpreter.Commands
{
    class Solver
    {
        public static void Execute(LogicalFunction function, int[] values)
        {
            for (int i = 0; i < function.Parameters.Length; i++)
            {
                UpdateVariable(function.Root, function.Parameters[i], values[i]);
            }

            int result = Evaluate(function.Root);
            Console.WriteLine("Result: " + result);
        }

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