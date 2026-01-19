using LogicInterpreter.Core;
using LogicInterpreter.Structures;

namespace LogicInterpreter.Parsing
{
    class ExpressionParser
    {
        public Node Parse(string expression)
        {
            NodeStack stack = new NodeStack();

            for (int i = 0; i < expression.Length; i++)
            {
                char token = expression[i];

                if (token == ' ') continue;

                if (token >= 'a' && token <= 'z')
                {
                    stack.Push(new Node
                    {
                        Type = NodeType.Variable,
                        Name = token
                    });
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
            }

            return stack.Pop();
        }
    }
}
