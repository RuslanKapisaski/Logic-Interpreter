using LogicInterpreter.Core;

namespace LogicInterpreter.Core
{
    class FunctionCollection
    {
        private const int size = 31;
        private LogicalFunction[][] table = new LogicalFunction[size][];


        public void Add(string name, LogicalFunction function)
        {
            int index = Hash(name) % size;

            if (table[index] == null)
            {
                table[index] = new LogicalFunction[10];
            }

            int i = 0;

            while (table[index][i] != null)
                i++;

            table[index][i] = function;
        }

        public LogicalFunction Get(string name)
        {
            int index = Hash(name) % size;

            if (table[index] == null) return null;

            int i = 0;
            while (table[index][i] != null)
            {
                if (table[index][i].Name == name)
                {
                    return table[index][i];
                }
                i++;
            }

            return null;
        }

        private int Hash(string key)
        {
            int h = 0;
            for (int i = 0; i < key.Length; i++)
            {
                h = h * 31 + key[i];
            }

            return h;
        }
    }
}
