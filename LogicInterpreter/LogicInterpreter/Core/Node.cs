namespace LogicInterpreter.Core
{

    class Node
    {
        public NodeType Type;
        public Node Left;
        public Node Right;

        public char Name;
        public int Value;

        public int Evaluate()
        {

            if (Type == NodeType.Variable)
            {
                return Value;
            }

            if (Type == NodeType.Not)
            {
                Value = Left.Evaluate() == 1 ? 0 : 1;
                return Value;
            }

            if (Type == NodeType.And)
            {
                Value = Left.Evaluate() & Right.Evaluate();
                return Value;
            }

            if (Type == NodeType.Or)
            {
                Value = Left.Evaluate() | Right.Evaluate();
                return Value;
            }

            return 0;
        }
    }

    
}
