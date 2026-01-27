using LogicInterpreter.Core;
using LogicInterpreter.Structures;
using System;

namespace LogicInterpreter.Parsing
{
    class ExpressionParser
    {
        private FunctionCollection table;

        public ExpressionParser(FunctionCollection t)
        {
            table = t;
        }

        public Node Parse(string expression)
        {
            NodeStack stack = new NodeStack();

            for (int i = 0; i < expression.Length; i++)
            {
                char token = expression[i];

                if (token == ' ')
                    continue;

                // 1) Variable: single lowercase letter
                if (IsLowerLetter(token))
                {
                    stack.Push(new Node
                    {
                        Type = NodeType.Variable,
                        Name = token
                    });
                }

                // DEFINE Fun2(a,b,c): Func1(a,b)c|
                else if (IsUpperLetter(token))
                {
                    string name = "";

                    while (i < expression.Length && (IsLetter(expression[i]) || IsDigit(expression[i])))
                    {
                        if (expression[i] == '(') break;

                        name += expression[i];
                        i++;
                    }

                    if (i < expression.Length && expression[i] == '(')
                    {
                        Node call = ParseFunctionCall(name, expression, ref i);
                        stack.Push(call);

                        i--;
                    }
                    else
                    {
                        throw new Exception("Invalid function call: " + name + " (missing '(')");
                    }
                }

                else if (token == '!')
                {
                    Node child = stack.Pop();
                    stack.Push(new Node
                    {
                        Type = NodeType.Not,
                        Left = child
                    });
                }

                else if (token == '&' || token == '|')
                {
                    Node right = stack.Pop();
                    Node left = stack.Pop();

                    stack.Push(new Node
                    {
                        Type = token == '&' ? NodeType.And : NodeType.Or,
                        Left = left,
                        Right = right
                    });
                }
                else
                {
                    throw new Exception("Invalid symbol: " + token);
                }
            }

            return stack.Pop();
        }

        private Node ParseFunctionCall(string funcName, string expr, ref int i)
        {
            if (table == null)
                throw new Exception("Function table is null");

            LogicalFunction f = table.Get(funcName);
            if (f == null)
                throw new Exception("Unknown function: " + funcName);

            i++;

            Node[] args = new Node[f.Parameters.Length];
            int argCount = 0;

            while (i < expr.Length && expr[i] != ')')
            {
                char c = expr[i];

                if (c == ' ' || c == ',')
                {
                    i++;
                    continue;
                }

                if (IsLowerLetter(c))
                {
                    if (argCount >= args.Length)
                        throw new Exception("Too many arguments for " + funcName);

                    args[argCount++] = new Node
                    {
                        Type = NodeType.Variable,
                        Name = c
                    };

                    i++;
                    continue;
                }

                throw new Exception("Invalid argument in call to " + funcName);
            }

            if (i >= expr.Length || expr[i] != ')')
                throw new Exception("Missing ')' in call to " + funcName);

            if (argCount != f.Parameters.Length)
                throw new Exception("Function " + funcName + " expects " + f.Parameters.Length + " args");


            i++;


            return Clone(f.Root, f.Parameters, args);
        }

        private Node Clone(Node n, char[] paramNames, Node[] args)
        {
            if (n == null) return null;

            if (n.Type == NodeType.Variable)
            {
                for (int k = 0; k < paramNames.Length; k++)
                {
                    if (n.Name == paramNames[k])
                        return args[k];
                }

                return new Node
                {
                    Type = NodeType.Variable,
                    Name = n.Name
                };
            }

            return new Node
            {
                Type = n.Type,
                Left = Clone(n.Left, paramNames, args),
                Right = Clone(n.Right, paramNames, args)
            };
        }


        private bool IsLowerLetter(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        private bool IsUpperLetter(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        private bool IsLetter(char c)
        {
            return IsLowerLetter(c) || IsUpperLetter(c);
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }
    }
}