using LogicInterpreter.Core;
using System;

namespace LogicInterpreter.Commands
{
    class Find
    {
        public static void Execute(int[,] table, char[] vars)
        {
            int n = vars.Length;

            //Генерираме базови кандидати: a, b, c и !a, !b, !c
            Node[] pool = new Node[n * 2];

            for (int i = 0; i < n; i++)
            {
                pool[i] = Var(vars[i]);
                pool[i + n] = Not(Var(vars[i]));
            }

            //Пробваме най-простите: a / !a
            for (int i = 0; i < pool.Length; i++)
            {
                Node cand = Clone(pool[i]); 

                if (Matches(cand, table, vars)) 
                {
                    Console.WriteLine("Result: " + ToText(cand)); 
                    return;
                }
            }

            // 3) Пробваме всички двойки с AND/OR: x&y, x|y
            for (int i = 0; i < pool.Length; i++)
            {
                for (int j = 0; j < pool.Length; j++)
                {
                    Node cand1 = And(Clone(pool[i]), Clone(pool[j]));
                    if (Matches(cand1, table, vars))
                    {
                        Console.WriteLine("Result: " + ToText(cand1));
                        return;
                    }

                    Node cand2 = Or(Clone(pool[i]), Clone(pool[j]));
                    if (Matches(cand2, table, vars))
                    {
                        Console.WriteLine("Result: " + ToText(cand2));
                        return;
                    }
                }
            }

            // 4) Пробваме 3 операнда (ограничена дълбочина): (x op y) op z
            for (int i = 0; i < pool.Length; i++)
            {
                for (int j = 0; j < pool.Length; j++)
                {
                    for (int k = 0; k < pool.Length; k++)
                    {
                        Node leftAnd = And(Clone(pool[i]), Clone(pool[j]));
                        Node candA = And(leftAnd, Clone(pool[k]));
                        if (Matches(candA, table, vars))
                        {
                            Console.WriteLine("Result: " + ToText(candA));
                            return;
                        }

                        Node leftOr = Or(Clone(pool[i]), Clone(pool[j]));
                        Node candB = Or(leftOr, Clone(pool[k]));
                        if (Matches(candB, table, vars))
                        {
                            Console.WriteLine("Result: " + ToText(candB));
                            return;
                        }

                        Node candC = Or(And(Clone(pool[i]), Clone(pool[j])), Clone(pool[k]));
                        if (Matches(candC, table, vars))
                        {
                            Console.WriteLine("Result: " + ToText(candC));
                            return;
                        }

                        Node candD = And(Or(Clone(pool[i]), Clone(pool[j])), Clone(pool[k]));
                        if (Matches(candD, table, vars))
                        {
                            Console.WriteLine("Result: " + ToText(candD));
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("No match found (within limited search depth).");
        }

        static void ResetCalculated(Node node)
        {
            if (node == null) return;

            node.IsCalculated = false;
            ResetCalculated(node.Left);
            ResetCalculated(node.Right);
        }

        static bool Matches(Node expr, int[,] table, char[] vars)
        {
            for (int row = 0; row < table.GetLength(0); row++)
            {
                
                for (int v = 0; v < vars.Length; v++)
                {
                    Set(expr, vars[v], table[row, v]);
                }

                int expected = table[row, vars.Length];

                ResetCalculated(expr);
                int actual = Solver.Evaluate(expr);

                if (actual != expected)
                    return false;
            }
            return true;
        }

        static void Set(Node node, char name, int value)
        {
            if (node == null) return;

            if (node.Type == NodeType.Variable && node.Name == name)
            {
                node.Value = value;
            }

            Set(node.Left, name, value);
            Set(node.Right, name, value);
        }

        static Node Var(char c) => new Node { Type = NodeType.Variable, Name = c };
        static Node Not(Node a) => new Node { Type = NodeType.Not, Left = a };
        static Node And(Node a, Node b) => new Node { Type = NodeType.And, Left = a, Right = b };
        static Node Or(Node a, Node b) => new Node { Type = NodeType.Or, Left = a, Right = b };

        static Node Clone(Node n)
        {
            if (n == null) return null;

            Node copy = new Node
            {
                Type = n.Type,
                Name = n.Name,
                Value = n.Value,
                IsCalculated = false
            };

            copy.Left = Clone(n.Left);
            copy.Right = Clone(n.Right);

            return copy;
        }

        static string ToText(Node n)
        {
            if (n == null) return "";

            if (n.Type == NodeType.Variable)
                return "" + n.Name;

            if (n.Type == NodeType.Not)
                return "!" + ToText(n.Left);

            string operand = n.Type == NodeType.And ? "&" : "|";
            return "(" + ToText(n.Left) + " " + operand + " " + ToText(n.Right) + ")";
        }
    }
}