using LogicInterpreter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterpreter.Structures
{
    class NodeStack
    {
        private Node[] data = new Node[100];
        private int top = -1;

        public void Push(Node node)
        {
            data[++top] = node;
        }

        public Node Pop()
        {
            return data[top--];
        }
    }
}
