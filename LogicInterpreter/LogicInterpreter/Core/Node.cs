namespace LogicInterpreter.Core
{

    class Node
    {
        public NodeType Type;
        public Node Left;
        public Node Right;

        public char Name;
        public int Value;

        public bool IsCalculated = false;
    }
}
